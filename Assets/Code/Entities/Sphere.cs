using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Sphere
{
    public Guid Guid { get; set; }
    private Rigidbody Rigidbody { get; set; }

    public Sphere(Rigidbody rigidbody) 
    {
        Rigidbody = rigidbody;
        Guid = Guid.NewGuid();
    }

    public void MoveToPosition(Vector3 position)
    {
        Rigidbody.AddForce((position - (Rigidbody.transform.position + Rigidbody.centerOfMass)) * SettingManager.SphereMagnetForceMod 
            - Rigidbody.velocity * SettingManager.SphereMagnetVelocityMod);
    }
}
