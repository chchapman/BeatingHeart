using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    [Header("sensitivity")]
    public float MouseSensitivityX; //5
    public float MouseSensitivityY; //5

    [Header("Desync")]
    public float syncSpeed; //5
    public float YsyncSpeed; //8

    public bool desync;

    public float smooth; //40
    public Transform tempTarget;
    Transform tempTar;

    public bool tempDesync;

    public float vaultSmooth; //5
    public float crouchSmooth; //20

    public float fovSyncSpeed;

    [Header("Tilt")]
    public float tiltAngle; //5
    public float tiltRate; //5
    public float inAirTiltRate; //.5
    private float tilt;

    [Header("Misc")]
    public Transform cam;

    private float xRot = 0;

    public Transform camPos;

    public float FOV;
    private float armFOV;

    [SerializeField] Camera armCam;

    [Header("Terminal")]

    public bool terminal;
    private bool seeTerm;
    [SerializeField] float termFOV;
    [SerializeField] float termFovSpeed;
    //public GameObject menu;

    public bool unlockNextLevel;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //FOV = cam.GetComponent<Camera>().fieldOfView;
        //armFOV = armCam.GetComponent<Camera>().fieldOfView;
        //menu.SetActive(false);
        //Invoke("CloseMenu", 5f);
        //tempTar = tempTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if (!terminal)
        {
            //tilt
            /*if (!gameObject.GetComponent<PlayerCharacterController>().isGrounded)
            {
                tilt = Mathf.Lerp(tilt, -tiltAngle * Input.GetAxisRaw("Horizontal"), inAirTiltRate * Time.deltaTime);
            }
            else
            {
                tilt = Mathf.Lerp(tilt, -tiltAngle * Input.GetAxisRaw("Horizontal") * .5f, tiltRate * Time.deltaTime);
            }*/

            //getting mouuse input and multiplyiung by sensitivity and making it frame rate independant
            float mouseX = Input.GetAxis("Mouse X") * MouseSensitivityX * 10 * Time.fixedDeltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivityY * -10 * Time.fixedDeltaTime;

            //rotating the character by teh mouseX around the Y axis
            transform.Rotate(Vector3.up, mouseX);

            xRot += mouseY;

            xRot = Mathf.Clamp(xRot, -90f, 90f);

            //rotating camera around the x axis by the mouseY
            cam.localRotation = Quaternion.Euler(xRot, 0f, tilt);

            //desync stuff
            if (desync)
            {
                cam.position = Vector3.Lerp(cam.position, camPos.position, Time.deltaTime * syncSpeed);
                //cam.position = Vector3.Lerp(cam.position, new Vector3(camPos.position.x, cam.position.y, cam.position.z), Time.deltaTime * YsyncSpeed);

                if (cam.GetComponent<Camera>().fieldOfView > FOV + .1f)
                {
                    cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(cam.GetComponent<Camera>().fieldOfView, FOV, fovSyncSpeed);
                }
                else
                {
                    cam.GetComponent<Camera>().fieldOfView = FOV;
                }

                if (armCam.GetComponent<Camera>().fieldOfView > armFOV + .1f)
                {
                    armCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(cam.GetComponent<Camera>().fieldOfView, armFOV, fovSyncSpeed);
                }
                else
                {
                    armCam.GetComponent<Camera>().fieldOfView = armFOV;
                }
                /*if (Mathf.Abs(camPos.position.magnitude - cam.position.magnitude) <= .5f)
                {
                    cam.position = camPos.position;
                }*/
            }

            if (tempDesync)
            {
                cam.position = Vector3.Lerp(cam.position, tempTar.position, Time.deltaTime * smooth);
                if (Mathf.Abs(tempTar.position.magnitude - cam.position.magnitude) <= .01f)
                {
                    tempDesync = false;
                    desync = true;
                    //cam.position = camPos.position;
                }
            }
        }
        else
        {
            cam.position = Vector3.Lerp(cam.position, tempTar.position, Time.deltaTime * smooth);
            cam.rotation = Quaternion.Lerp(cam.rotation, tempTar.rotation, Time.deltaTime * smooth);
            if (Mathf.Abs(tempTar.position.magnitude - cam.position.magnitude) <= .01f)
            {
                cam.position = tempTar.position;
            }
            /*
            if (seeTerm)
            {
                cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(cam.GetComponent<Camera>().fieldOfView, termFOV, termFovSpeed);
                armCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(cam.GetComponent<Camera>().fieldOfView, termFOV, termFovSpeed);

                if(Mathf.Abs(cam.GetComponent<Camera>().fieldOfView - termFOV) < .1f && !menu.activeSelf)
                {
                    OpenMenu();
                }
            }*/

            if (!seeTerm)
            {
                cam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(cam.GetComponent<Camera>().fieldOfView, FOV, termFovSpeed);
                armCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(cam.GetComponent<Camera>().fieldOfView, FOV, termFovSpeed);
            }
        }
    }

    public void desyncCam(int des)
    {
        //cam.localPosition = new Vector3(cam.position.x + des.x, cam.position.y + des.y, cam.position.z + des.z);
        if(des == 1)
        {
            cam.localPosition = new Vector3(0f, -1f, -0.75f);
            syncSpeed = vaultSmooth;

        }else if(des == 2)
        {
            cam.localPosition = new Vector3(0f, +.5f, 0f);
            syncSpeed = crouchSmooth;

        }
        else if(des == 3)
        {
            cam.localPosition = new Vector3(0f, -.5f, 0f);
            syncSpeed = crouchSmooth;
        }
        else if (des == 4)
        {
            cam.localPosition = new Vector3(0f, -.5f, -.75f);
            syncSpeed = vaultSmooth;
        }
    }

    public void impactCam(Vector3 pos)
    {
        tempDesync = true;
        desync = false;
        tempTar.localPosition = pos;
    }

    public void Terminal(Transform tar)
    {
        terminal = true;
        tempTar = tar;
        //GetComponent<PlayerCharacterController>().move = false;
    }

    public void TermFOV()
    {
        seeTerm = true;
    }

    /*
    void OpenMenu()
    {
        menu.SetActive(true);

        if (unlockNextLevel)
        {
            //menu.GetComponent<InGameMenu>().unlockNextLevel();
        }

        //menu.GetComponent<TextEffects>().retype();
    }

    public void CloseMenu()
    {
        seeTerm = false;
        menu.SetActive(false);
        //GetComponent<weaponSwitching>().anim.Play("arms_ex_terminal");
    }

    public void stopTerm()
    {
        terminal = false;
        tempTar = tempTarget;
        //GetComponent<weaponSwitching>().anim.Play("arms_intro");
        //GetComponent<PlayerCharacterController>().move = true;
    }

    public void startAtTerm(Transform tar)
    {

        terminal = true;
        tempTar = tar;
        //GetComponent<PlayerCharacterController>().move = false;

        FOV = cam.GetComponent<Camera>().fieldOfView;
        armFOV = armCam.GetComponent<Camera>().fieldOfView;

        cam.GetComponent<Camera>().fieldOfView = termFOV;
        armCam.GetComponent<Camera>().fieldOfView = termFOV;
        seeTerm = false;

        cam.position = tempTar.position;
        cam.rotation = tempTar.rotation;
        GameObject.FindWithTag("Player").transform.position = tempTar.position;

        CloseMenu();
    }*/
}
