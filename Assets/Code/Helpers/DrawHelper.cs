using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Graphics = System.Drawing.Graphics;
using Bitmap = DisposableBitmap;
using Font = System.Drawing.Font;
using Color = System.Drawing.Color;

public static class DrawHelper
{
    public static Bitmap DrawText(string text)
    {
        var bitmap = new Bitmap(SettingManager.MaxTextBitmapWidth, SettingManager.MaxTextBitmapHeight);
        using (Graphics graphics = Graphics.FromImage(bitmap.GetBitmap()))
        {
            var imageSize = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            graphics.FillRectangle(Brushes.White, imageSize);
            graphics.DrawString(text, new Font(SettingManager.FontName, SettingManager.FontSize), Brushes.Black, new RectangleF(0, 0, bitmap.Width, bitmap.Height));
            graphics.Flush();
        }
        return bitmap;
    }

    public static bool IsBlack(Color color)
    {
        return color.R <= SettingManager.BlackColorSplitter && color.G <= SettingManager.BlackColorSplitter && color.B <= SettingManager.BlackColorSplitter;
    }
}
