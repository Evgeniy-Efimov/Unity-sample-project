using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    private Sphere Sphere { get; set; }

    void Start()
    {
        try
        {            
            Sphere = new Sphere(GetComponent<Rigidbody>());
            SphereManager.GetManagerInstance().SubscribeSphere(Sphere);
        }
        catch (Exception exception)
        {
            LogManager.LogException(exception);
        }
    }
}
