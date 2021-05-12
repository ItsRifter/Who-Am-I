using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{

    public float mouseSens;

    public Transform playerBody;

    float xRotation = 0f;

    //Lock the mouse
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    //Mouse input
    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        //Clamp the rotation so the player can't look over or under
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
