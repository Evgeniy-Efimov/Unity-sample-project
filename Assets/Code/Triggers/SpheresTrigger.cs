using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpheresTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SphereManager.GetManagerInstance().EnableTimeDisplay();
    }

    private void OnTriggerExit(Collider other)
    {
        SphereManager.GetManagerInstance().DisableTimeDisplay();
    }
}
