using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SettingManager
{
    public static float MovementSpeed = 3f;
    public static float RotationSpeed = 1.5f;
    public static float JumpForce = 2f;
    public static int MaxTextBitmapWidth = 1000;
    public static int MaxTextBitmapHeight = 1000;
    public static string FontName = "Tahoma";
    public static int FontSize = 11;
    public static int BlackColorSplitter = 200;
    public static int UpdateTimeMilliseconds = 1000;
    public static int UpdateSpherePositionMilliseconds = 50;
    public static float SphereMagnetForceMod = 3.5f;
    public static float SphereMagnetVelocityMod = 0.9f;
    public static string SphereTag = "Sphere";
    public static string MainCameraTag = "MainCamera";
    public static string DisplayTag = "Display";
}
