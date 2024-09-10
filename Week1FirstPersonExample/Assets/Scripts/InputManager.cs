using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    public PlayerControls playerControls;
    PlayerControls.GroundMovementActions groundMovement;

    Vector2 HorizontalInput;

    // Start is called before the first frame update
    void Awake()
    {
        playerControls = new PlayerControls();
        groundMovement = playerControls.GroundMovement;

        groundMovement.horizontal.performed += ctx => HorizontalInput = ctx.ReadValue<Vector2>();

        groundMovement.Jump.performed += _u => GetComponent<PlayerMovement>().OnJump();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<PlayerMovement>().RecieveInput(HorizontalInput);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
