using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update

    public float mouseSensitivity = 1000f;

    public Transform playerBody;
    public GameObject flashLight;

    bool flashLightOn;

    float xRotation = 0f;

    void Start()
    {
        flashLight.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown("f") && !flashLightOn)
        {
            flashLight.SetActive(true);
            Debug.Log("on");
            flashLightOn = true;

        }
        else if (Input.GetKeyDown("f") && flashLightOn)
        {
            flashLight.SetActive(false);
            Debug.Log("off");
            flashLightOn = false;
        }
    }
}

