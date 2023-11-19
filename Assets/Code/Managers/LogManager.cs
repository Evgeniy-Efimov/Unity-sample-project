using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LogManager
{
    public static void LogException(Exception exception)
    {
        try
        {
            Debug.LogException(exception);
        }
        catch { }
    }
}
