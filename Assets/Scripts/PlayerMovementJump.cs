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
    public bool isOnGround = true;

    public LayerMask layerMask;

    private CapsuleCollider2D capsuleCollider;    
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        GroundCheck();
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

        if (isOnGround && /*!jumped &&*/ rb2d.velocity.y>=0f)
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
        if (collision.gameObject.tag == "Water")
        {
            SceneManager.LoadScene(0);
        }
    }

    /// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
    private void GroundCheck()
    {
        float dx = capsuleCollider.size.x * 0.5f;
        float dy = capsuleCollider.size.y;

        Vector2 start = new Vector2(transform.position.x, transform.position.y);
        float distance = 2f * dy - dx + 0.01f;

        RaycastHit2D hit = Physics2D.CircleCast(start, 0.9f * dx, Vector2.down, distance, layerMask);
        if (hit.collider != null)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }

}