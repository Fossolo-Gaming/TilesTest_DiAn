using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityStandardAssets.CrossPlatformInput;


public class PlayerMovement : MonoBehaviour
{
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

    public float speed = 100f;             //Floating point variable to store the player's movement speed.

    private Vector2 direction = new Vector2(0f, 0f);
    private bool updated = false;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    private void move(float x, float y)
    {
        direction = new Vector2(x, y) * speed;
        updated = true;
    }

    public void MoveLeft()  { move(-1f,  0f); }
    public void MoveRight() { move( 1f,  0f); }
    public void MoveUp()    { move( 0f,  1f); }
    public void MoveDown()  { move( 0f, -1f); }
    public void StopMovement() { updated = false;  }

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        if (!updated)
        {
            if (Input.GetKey(leftKey)) move(-1f, 0f);
            else if (Input.GetKey(rightKey)) move(1f, 0f);
            else if (Input.GetKey(upKey)) move(0f, 1f);
            else if (Input.GetKey(downKey)) move(0f, -1f);
            else
            {
                direction = new Vector2(0f, 0f);
            }
            updated = false;
        }

        /*
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis(InputHorizontalName);
        
        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis(InputVericalName);

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        */
        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(direction);                
    }
}