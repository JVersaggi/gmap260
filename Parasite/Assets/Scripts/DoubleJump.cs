using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    public int maxjumps = 2;
    int jumps;
    float jumpforce = 5f;
    bool grounded;
    public float movespeed;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveL();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveR();
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }

    void MoveL()
    {

    }

    void MoveR()
    {

    }

    void Jump()
    {
        if (jumps > 0)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector2(0, jumpforce), ForceMode.Impulse);
            grounded = false;
            jumps = jumps - 1;
        }
        if (jumps == 0)
        {
            return;
        }
    }
    void OnCollisionEnter(Collision collide)
    {
        if (collide.gameObject.tag == "Ground")
        {
            jumps = maxjumps;
            grounded = true;
            movespeed = 2.0F;
        }
    }
}
