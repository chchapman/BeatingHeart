using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    PlayerControls playerControls;
    Vector2 MouseInput;

    [SerializeField] float xSens = 8;
    float mouseX;
    Transform Player;

    [SerializeField] float ySens = 0.5f;
    float mouseY;

    Transform mainCam;
    [SerializeField] float xClamp = 90;
    float xRotation = 0f;

    [SerializeField] Vector3 offset;
    [SerializeField] float syncSpeed;

    [Header("Effects")]

    [SerializeField] float tilt;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        playerControls = GetComponent<InputManager>().playerControls;

        playerControls.GroundMovement.LookX.performed += ctx => MouseInput.x = ctx.ReadValue<float>();
        playerControls.GroundMovement.LookY.performed += ctx => MouseInput.y = ctx.ReadValue<float>();

        Player = GameObject.FindWithTag("Player").transform;
        mainCam = Camera.main.transform;
    }

    private void Update()
    {
        mouseX = MouseInput.x * xSens;

        //transform.localRotation = (Vector3.forward * tilt * GetComponent<PlayerMovement>().horizontalInput.x);
        //transform.rotation = Quaternion.Euler(transform.rotation.x + mouseX * Time.deltaTime, transform.rotation.y, -tilt * GetComponent<PlayerMovement>().horizontalInput.x);

        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);

        //mainCam.Rotate(Vector3.up, mouseX * Time.deltaTime);

        //mainCam.rotation;

        mouseY = MouseInput.y * ySens;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);

        Vector3 targetRotation = mainCam.transform.eulerAngles;

        targetRotation.x = xRotation;

        mainCam.transform.eulerAngles = targetRotation;

        //desync

        //mainCam.position = Vector3.Lerp(mainCam.position, transform.position + offset, Time.deltaTime * syncSpeed);
    }
}
