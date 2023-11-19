using System;
using System.Collections.Generic;
using UnityEngine;
using Graphics = System.Drawing.Graphics;
using Bitmap = DisposableBitmap;
using Font = System.Drawing.Font;
using Color = System.Drawing.Color;
using System.Linq;

public class SphereManager
{
    private static SphereManager Instance { get; set; } = new SphereManager();

    private List<List<SpherePosition>> SpheresPosition = new List<List<SpherePosition>>();
    private List<Sphere> FreeSpheres = new List<Sphere>();
    private List<Sphere> SubscribedSpheres = new List<Sphere>();
    private GameObject TimeDisplay { get; set; }
    private float displayX0, displayY0, displayZ, displayWidth, displayHeight;

    private int textBitmapWidth;
    private int textBitmapHeight;

    private object IsEnabledLocker = new object();
    private bool IsEnabled = false;

    private TimeChecker SpheresUpdateTimeChecker { get; set; }
    private TimeChecker TimeUpdateTimeChecker { get; set; }

    private SphereManager()
    {
        try
        {
            SpheresUpdateTimeChecker = new TimeChecker();
            TimeUpdateTimeChecker = new TimeChecker();
            TimeDisplay = GameObject.FindGameObjectWithTag(SettingManager.DisplayTag);

            var extents = TimeDisplay.GetComponent<MeshFilter>().mesh.bounds.extents;
            var size = TimeDisplay.GetComponent<MeshFilter>().mesh.bounds.size;
            var cornerLocalPosition = new Vector3(extents.x, 0, -extents.z);
            var cornerPosition = TimeDisplay.transform.TransformPoint(cornerLocalPosition);
            displayX0 = cornerPosition.x;
            displayY0 = cornerPosition.y;
            displayZ = cornerPosition.z;
            displayWidth = size.x * TimeDisplay.transform.localScale.x;
            displayHeight = size.z * TimeDisplay.transform.localScale.z;

            UpdateTextBitmapSize(DrawHelper.DrawText("00:00:00"));
        }
        catch (Exception exception)
        {
            LogManager.LogException(exception);
        }
    }

    public static SphereManager GetManagerInstance()
    {      
        return Instance;
    }

    public void UpdateSpheresPosition()
    {
        try
        {
            if (SpheresUpdateTimeChecker.IsTimePass(SettingManager.UpdateSpherePositionMilliseconds))
            {
                foreach (var sphere in SubscribedSpheres)
                {
                    var newPosition = GetSpherePosition(sphere);
                    if (newPosition != null)
                    {
                        sphere.MoveToPosition(newPosition.Value);
                    }
                }                
            }
        }
        catch (Exception exception)
        {
            LogManager.LogException(exception);
        }
    }

    public void EnableTimeDisplay()
    {
        lock (IsEnabledLocker)
        {
            IsEnabled = true;
        }
    }

    public void DisableTimeDisplay()
    {
        lock (IsEnabledLocker)
        {
            IsEnabled = false;
        }
    }

    public void SubscribeSphere(Sphere sphere)
    {
        if (Instance.SubscribedSpheres.FirstOrDefault(s => s.Guid == sphere.Guid) == null)
        {
            lock (Instance.SubscribedSpheres) lock (Instance.FreeSpheres)
            {
                Instance.SubscribedSpheres.Add(sphere);
                Instance.FreeSpheres.Add(sphere);                
            }
        }
    }

    public Vector3? GetSpherePosition(Sphere sphere)
    {
        if (!IsEnabled) return null;
        lock (SpheresPosition)
        {
            var position = SpheresPosition?.SelectMany(s => s)?.FirstOrDefault(s => s?.Sphere?.Guid == sphere.Guid)?.Position;

            if (position == null && !FreeSpheres.Any(s => s.Guid == sphere.Guid))
            {
                lock (FreeSpheres)
                {
                    FreeSpheres.Add(sphere);
                }
            }
            return position;
        }
    }

    public void UpdateTime()
    {
        if (TimeUpdateTimeChecker.IsTimePass(SettingManager.UpdateTimeMilliseconds))
        {
            var bitmap = DrawHelper.DrawText(GetTimeString());

            for (int i = 0; i < textBitmapWidth; i++)
            {
                for (int j = 0; j < textBitmapHeight; j++)
                {
                    if (DrawHelper.IsBlack(bitmap.GetPixel(i, j)))
                    {
                        UseSphere(i, j);
                    }
                    else
                    {
                        FreeSphere(i, j);
                    }
                }
            }
            bitmap.Dispose();
        }       
    }  

    private void UseSphere(int i, int j)
    {
        if (SpheresPosition[i][j]?.Sphere == null)
        {
            lock (FreeSpheres)
            {
                var freeSphere = FreeSpheres.LastOrDefault();
                if (freeSphere != null)
                {
                    FreeSpheres.Remove(freeSphere);
                    lock (SpheresPosition)
                    {
                        SpheresPosition[i][j] = new SpherePosition()
                        {
                            Sphere = freeSphere,
                            Position = GetPositionByBitmapCoordinates(i, j, textBitmapWidth, textBitmapHeight)
                        };
                    }
                }
            }
        }
    }

    private void FreeSphere(int i, int j)
    {
        var sphere = SpheresPosition[i][j]?.Sphere;
        if (sphere != null)
        {
            lock (FreeSpheres)
            {                
                if (!FreeSpheres.Any(s => s.Guid == sphere.Guid))
                {
                    FreeSpheres.Add(sphere);
                }
            }
            lock (SpheresPosition)
            {
                SpheresPosition[i][j] = null;
            }  
        }
    }

    private void UpdateTextBitmapSize(Bitmap bitmap)
    {
        int currentTextBitmapWidth = 0;
        int currentTextBitmapHeight = 0;

        for (int i = 0; i < SettingManager.MaxTextBitmapWidth; i++)
        {
            for (int j = 0; j < SettingManager.MaxTextBitmapHeight; j++)
            {
                if (DrawHelper.IsBlack(bitmap.GetPixel(i, j)))
                {
                    if (currentTextBitmapWidth < i) currentTextBitmapWidth = i;
                    if (currentTextBitmapHeight < j) currentTextBitmapHeight = j;
                }
            }
        }

        textBitmapWidth = currentTextBitmapWidth + 1;
        textBitmapHeight = currentTextBitmapHeight + 1;

        for (int i = 0; i < textBitmapWidth; i++)
        {
            SpheresPosition.Add(new List<SpherePosition>());
            for (int j = 0; j < textBitmapHeight; j++)
            {
                SpheresPosition[i].Add(new SpherePosition());
            }
        }
    }

    private Vector3 GetPositionByBitmapCoordinates(int i, int j, int width, int height)
    {
        var positionModifierX = displayWidth / width;
        var positionModifierY = displayHeight / height;
        return new Vector3(displayX0 + i * positionModifierX, displayY0 - j * positionModifierY, displayZ - 1);
    }

    private string GetTimeString()
    {
        return DateTime.Now.ToString(@"HH\:mm\:ss");
    }
}
