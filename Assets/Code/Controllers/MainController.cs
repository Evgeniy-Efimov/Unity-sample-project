using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    void Update()
    {
        try
        {
            SphereManager.GetManagerInstance().UpdateSpheresPosition();
        }
        catch (Exception exception)
        {
            LogManager.LogException(exception);
        }
    }
}
