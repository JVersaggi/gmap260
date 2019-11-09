using UnityEngine;
using System.Collections;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class Movement : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    int nrOfAlowedDJumps = 1;
    int dJumpCounter = 0;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        {
            moveDirection.x = Input.GetAxis("Horizontal") * speed;
            moveDirection.z = Input.GetAxis("Vertical") * speed;

            if (Input.GetButtonDown("Jump"))
            {
                if (characterController.isGrounded)
                {
                    moveDirection.y = jumpSpeed;
                    dJumpCounter = 0;
                }
                if (!characterController.isGrounded && dJumpCounter < nrOfAlowedDJumps)
                {
                    moveDirection.y = jumpSpeed;
                    dJumpCounter++;
                }
            }
            moveDirection.y -= gravity * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }
}