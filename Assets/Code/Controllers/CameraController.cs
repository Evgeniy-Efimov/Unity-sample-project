using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CameraManager cameraManager;
    private bool CanJump = true;

    void Start()
    {
        try
        {
            cameraManager = new CameraManager(GetComponent<Rigidbody>());
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        catch (Exception exception)
        {
            LogManager.LogException(exception);
        }
    }

    void OnCollisionStay()
    {
        CanJump = true;
    }

    void Update()
    {
        try
        {
            cameraManager.Rotate(Input.GetAxis(InputKeys.MouseX), Input.GetAxis(InputKeys.MouseY));

            if (Input.GetKey(InputKeys.W)) cameraManager.MoveForward(Time.deltaTime);
            if (Input.GetKey(InputKeys.S)) cameraManager.MoveBackward(Time.deltaTime);
            if (Input.GetKey(InputKeys.A)) cameraManager.MoveLeft(Time.deltaTime);
            if (Input.GetKey(InputKeys.D)) cameraManager.MoveRight(Time.deltaTime);
            if (Input.GetKey(InputKeys.Space) && CanJump) 
            { 
                CanJump = false;
                cameraManager.MoveUp(); 
            }
        }
        catch (Exception exception)
        {
            LogManager.LogException(exception);
        }
    }
}