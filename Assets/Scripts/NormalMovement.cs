using UnityEngine;
using System.Collections;

public class NormalMovement : MonoBehaviour
{

    private Vector3 movementVector;
    private CharacterController controller;

    public float movementSpeed = 8;
    public float jumpPower = 15;
    public float gravity = 40;
    public float rotateSpeed = 5f;
    private float rotate = 0.0f;
    void Start()
    {


        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        
        //for keyboard ---------------------------------------------------
        if (Input.GetKey(KeyCode.W))//move forward
        {
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.S))//move back
        {
            transform.position -= transform.forward * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.Q))//move left
        {
            transform.position -= transform.right * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.E))//move right
        {
            transform.position += transform.right * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.D))//move left
        {
            transform.Rotate(0, 3, 0);
        }
        if (Input.GetKey(KeyCode.A))//move left
        {
            transform.Rotate(0, -3, 0);
        }

        if (controller.isGrounded)
        {
            movementVector.y = 0;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                movementVector.y = jumpPower;
            }
        }
        movementVector.y -= gravity * Time.deltaTime;

        controller.Move(movementVector * Time.deltaTime);

    }
}
