using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityStandardAssets.CrossPlatformInput;


public class PlayerMovementJump : MonoBehaviour
{
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

    public float xSpeedOnGround = 1f;             //Floating point variable to store the player's movement speed.
    public float xSpeedInFlight = 0.5f;
    public float jumpForce = 100f;
    public float maxSpeed = 5f;

    private Vector2 direction = new Vector2(0f, 0f);
    public bool isOnGround = false;
    public bool jumped = false;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    public BoxCollider2D feetCollider;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        //Feet = GetComponent<BoxCollider2D>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        float x = 0f;
        float y = 0f;
        if (Input.GetKey(leftKey))
        {
            if (rb2d.velocity.x > -maxSpeed)
            {
                x = isOnGround ? -xSpeedOnGround : -xSpeedInFlight;
            }
        }
        if (Input.GetKey(rightKey))
        {
            if (rb2d.velocity.x < maxSpeed)
            {
                x = isOnGround ? xSpeedOnGround : xSpeedInFlight;
            }
        }

        if (isOnGround && !jumped && rb2d.velocity.y>=0f)
        {
            if (Input.GetKey(upKey))
            {
                y = jumpForce;
                // jumped = true;
            }
        }

        if(x!=0f || y!=0f)
        {
            rb2d.AddForce(new Vector2(x, y), ForceMode2D.Impulse);            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger player");
        isOnGround = true;
        if (collision.gameObject.tag == "Water")
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isOnGround = false;
        //jumped = false;
    }

}