using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Controls")]
    public Joystick joystick;
    public float horizontalSensitivity;
    public float verticaSensitivity;
    public float magnituteValue;


    [SerializeField] public float mouseSensitivity = 300.0f;
    [SerializeField] public Transform playerBody;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        float mouseX = joystick.Horizontal* magnituteValue;
        float mouseY = joystick.Vertical* magnituteValue;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //playerBody.Rotate(Vector3.up*mouseX);
        yRotation += mouseX;

        playerBody.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
}
