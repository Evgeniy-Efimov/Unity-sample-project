using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    private Rigidbody CameraRigidbody { get; set; }

    public CameraManager(Rigidbody cameraRigidbody)
    {
        CameraRigidbody = cameraRigidbody;
    }

    public void Rotate(float x, float y)
    {
        CameraRigidbody.transform.eulerAngles -= new Vector3(y * SettingManager.RotationSpeed, -x * SettingManager.RotationSpeed, 0);
    }

    public void MoveForward(float deltaTime)
    {
        CameraRigidbody.transform.position += new Vector3(CameraRigidbody.transform.forward.x, 0, CameraRigidbody.transform.forward.z)
             * SettingManager.MovementSpeed * deltaTime;
    }

    public void MoveBackward(float deltaTime) 
    {
        CameraRigidbody.transform.position -= new Vector3(CameraRigidbody.transform.forward.x, 0, CameraRigidbody.transform.forward.z)
             * SettingManager.MovementSpeed * deltaTime;
    }

    public void MoveLeft(float deltaTime)
    {
        CameraRigidbody.transform.position -= CameraRigidbody.transform.right * SettingManager.MovementSpeed * deltaTime;
    }

    public void MoveRight(float deltaTime) 
    {
        CameraRigidbody.transform.position += CameraRigidbody.transform.right * SettingManager.MovementSpeed * deltaTime;
    }

    public void MoveUp() 
    {
        CameraRigidbody.AddForce(new Vector3(0, SettingManager.JumpForce, 0), ForceMode.Impulse);
    }
}
