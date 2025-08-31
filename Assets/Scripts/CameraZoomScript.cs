using UnityEngine;
using System;

public class CameraZoomScript : MonoBehaviour
{
    float minFov = 15f;
    public float maxFov = 120f;
    public float zoomSensitivity = 15f;
    public float moveSensitivity = 0.035f;

    float maxCameraX = 32f;
    float maxCameraY = 40f;

    int horizontalMove = 0;
    int verticalMove = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckFovChange();
        CheckMovement();
    }

    void OnGUI()
    {
        float CamX = Camera.main.transform.position.x;
        float CamY = Camera.main.transform.position.y;
        float CamZ = Camera.main.transform.position.z;
        float finalSens = moveSensitivity * (1 + (Camera.main.fieldOfView - minFov)* 0.5f / minFov); // 1x at Min zoom, 3x at Max (6x) zoom, linear inbetween
        float updatedX = Util.maybeConstrainByAbsValue(CamX + horizontalMove * finalSens, maxCameraX);
        float updatedY = Util.maybeConstrainByAbsValue(CamY + verticalMove * finalSens, maxCameraY);

        Camera.main.transform.position = new Vector3(updatedX, updatedY, CamZ);
    }

    void CheckFovChange()
    {
        float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }

    void CheckMovement()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalMove = -1;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontalMove = 1;
        }
        else
        {
            horizontalMove = 0;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            verticalMove = 1;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            verticalMove = -1;
        }
        else
        {
            verticalMove = 0;
        }
    }
}
