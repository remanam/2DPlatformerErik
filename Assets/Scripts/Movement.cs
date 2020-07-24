using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Movement : MonoBehaviour {

    // It's used to set isGroundFalse i guess
    [SerializeField]
    private LayerMask platformsLayerMask;


    public float speed = 0.1f;
    public float jumpVelocity = 5f;
    public float climbSpeed = 2f;

    private bool isRunning;
    private bool isJumping;
    private bool isAtacking;

    private bool isRotated;
    private bool canClimb;

    private bool ifShowClimbButton;
    private bool right; // if moving right
    private bool left; // if moving left
    private bool jump; // if Jumping
    private bool climb; // If climbing


    private float oldMass;
    private float oldGravity;

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider2D;
    public GameObject rightButton;
    public GameObject leftButton;
    public GameObject jumpButton;
    public GameObject climbButton;
    public GameObject attackButton;




    private void Start()
    {
        isRunning = false;
        isRotated = false;

        rb = transform.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();

        // Default physics value that need to be applied after climbing
        oldGravity = rb.gravityScale;
        oldMass = rb.mass;

    }

    void FixedUpdate()
    {


        right = rightButton.GetComponent<RightButtonHandler>().isMovingRight;

        left = leftButton.GetComponent<LeftButtonHandler>().isMovingLeft;
        
        
        


        //if (Input.GetKey(KeyCode.D))
        if (right || Input.GetKey(KeyCode.D))
            {
            moveRight();

        }
        //if (Input.GetKey(KeyCode.A))
        else if (left || Input.GetKey(KeyCode.A))  {

            moveLeft(); //move left function
        }
        else
        {
            isRunning = false;
            anim.SetBool("isRunning", isRunning);
        }

        if (attackButton.GetComponent<AttackButtonHandler>().isAttacking == true) {
            isAtacking = true;
            anim.SetBool("isAtacking", isAtacking);
        }
        else {
            isAtacking = false;
            anim.SetBool("isAtacking", isAtacking);
        }


        if (canClimb && climbButton.GetComponent<ClimbuttonHandler>().isClimb)
        {
                Debug.Log(climbButton.GetComponent<ClimbuttonHandler>().isClimb);
                Debug.Log("Going Up");

                rb.gravityScale = 0;
                rb.mass = 0;
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * 0.1f, transform.position.z);

            }
            else {
                 rb.gravityScale = oldGravity;
                 rb.mass = oldMass;
            }

    }

    private void Update()
    {
        jump = jumpButton.GetComponent<JumpButtonHandler>().isJump;
      

        if ((isGrounded() && jump) || (isGrounded() && Input.GetKeyDown(KeyCode.Space)))
        {

            rb.velocity = Vector2.up * jumpVelocity;

        }

        bool isGrounded()
        {
            RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center,
                boxCollider2D.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
            // Debug.Log(raycastHit2d.collider);
            //Debug.DrawRay(transform.position, Vector3.down * 2, Color.green);
            return raycastHit2d.collider != null;
        }

        if (!isGrounded())
        {
            anim.SetBool("isJumping", true);
        }
        else
            anim.SetBool("isJumping", false);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        jumpButton.GetComponent<JumpButtonHandler>().gameObject.SetActive(false);
        //climbButton.GetComponent<ClimbuttonHandler>().btn.gameObject.SetActive(true);

        
        if (collision.gameObject.tag == "Stairs")
        {
            
            canClimb = true;
            Debug.Log("Entered Stairs");
            Debug.Log(canClimb);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        jumpButton.GetComponent<JumpButtonHandler>().gameObject.SetActive(true);
        //climbButton.GetComponent<ClimbuttonHandler>().btn.gameObject.SetActive(false);
        
        canClimb = false;
        Debug.Log("Exited Stairs");
        Debug.Log(canClimb);

        rb.gravityScale = oldGravity;
        rb.mass = oldMass;
    }

    private void moveRight()
    {
        if (isRotated) {
            isRotated = false;
            transform.Rotate(0, 180, 0, Space.Self);
            Debug.Log("Rotating Right");
        }
        transform.position = new Vector3(transform.position.x + speed * 0.1f, transform.position.y, transform.position.z);

        isRunning = true;
        anim.SetBool("isRunning", isRunning);
    }


    private void moveLeft()
    {
        if (!isRotated) {
            isRotated = true;
            transform.Rotate(0, -180, 0, Space.Self);
            Debug.Log("Rotating Left");
        }
        transform.position = new Vector3(transform.position.x - speed * 0.1f, transform.position.y, transform.position.z);

        isRunning = true;
        anim.SetBool("isRunning", isRunning);
    }

}
