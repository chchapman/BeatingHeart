using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Moving")]

    [SerializeField] CharacterController Controller;
    [SerializeField] float speed = 10.0f;
    [SerializeField] float airSpeed = 10.0f;
    [SerializeField] float groundSmooth = 10.0f;
    [SerializeField] float airSmooth = 10.0f;
    Vector3 verticalVel;
    [SerializeField] float gravity = -20;
    public Vector2 horizontalInput;
    Vector3 vel;

    [Header("Gravity")]
    [SerializeField] LayerMask whatIsGround;
    bool isGrounded;

    [Header("Jumping")]
    [SerializeField] float jumpHeight;
    bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal movement

        //Controller.Move(HorizontalVelocity * Time.deltaTime);
        if (isGrounded)
        {
            Vector3 HorizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed * Time.deltaTime;

            Controller.Move(Vector3.SmoothDamp(new Vector3(Controller.velocity.x, 0f, Controller.velocity.z), HorizontalVelocity, ref vel, groundSmooth));
        }
        else
        {
            Vector3 HorizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * airSpeed * Time.deltaTime;

            Controller.Move(Vector3.SmoothDamp(new Vector3(Controller.velocity.x, 0f, Controller.velocity.z), HorizontalVelocity, ref vel, airSmooth));
        }

        //vertical movement

        verticalVel.y += gravity * Time.deltaTime;
        GroundCheck();

        if (isJumping)
        {
            Jump();
        }

        Controller.Move(verticalVel * Time.deltaTime);
    }

    public void RecieveInput(Vector2 horizontalInput)
    {
        this.horizontalInput = horizontalInput;
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, whatIsGround);

        if (isGrounded)
        {
            verticalVel.y = 0;
        }
    }

    public void OnJump()
    {
        isJumping = true;
    }

    void Jump()
    {
        if (isGrounded)
        {
            verticalVel.y = Mathf.Sqrt(-2 * jumpHeight * gravity);

        }

        isJumping = false;
    }
}
