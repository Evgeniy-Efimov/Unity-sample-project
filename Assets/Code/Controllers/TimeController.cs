using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    void Update()
    {
        try
        {
            SphereManager.GetManagerInstance().UpdateTime();
        }
        catch (Exception exception)
        {
            LogManager.LogException(exception);
        }
    }
}
