using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float smooth;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask whatIsGround;

    Vector2 horizontalInput;
    Vector3 vel;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desVel = new Vector3(horizontalInput.x * speed * Time.deltaTime, 0f, horizontalInput.y * speed * Time.deltaTime);
        Vector3 horizontalMovement = Vector3.SmoothDamp(rb.velocity, desVel, ref vel, smooth);

        rb.velocity = horizontalMovement;
    }

    public void RecieveInput(Vector2 horizontalInput)
    {
        this.horizontalInput = horizontalInput;
    }

    bool isGrounded()
    {
        if(Physics.Raycast(groundCheck.position, Vector3.down, .2f, whatIsGround))
        {
            return true;
        }

        return false;
    }
}
