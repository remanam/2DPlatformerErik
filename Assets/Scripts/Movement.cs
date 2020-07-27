using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Movement : MonoBehaviour {

    // It's used to set isGroundFalse i guess
    [SerializeField]
    private LayerMask platformsLayerMask;

    [SerializeField]
    private ParticleSystem ps;


    public float speed = 0.1f;
    public float jumpVelocity = 5f;
    public float climbSpeed = 2f;

    [SerializeField]
    private float fallSpeed = 0.5f;
    


    private bool isRunning;
    private bool isJumping;
    private bool isAtacking;

    private bool isRotated;
    private bool canClimb;

    private bool right; // if moving right
    private bool left; // if moving left
    private bool jump; // if Jumping
    private bool climb; // If climbing
    public bool playLanding = false;


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

    Vector2 velocityVector;




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

        ps.Stop();


        climb = climbButton.GetComponent<ClimbuttonHandler>().isClimb;

        Vector2 velocityVector = rb.velocity;
    }

    void FixedUpdate()
    {
        right = rightButton.GetComponent<RightButtonHandler>().isMovingRight;

        left = leftButton.GetComponent<LeftButtonHandler>().isMovingLeft;

        if (right || Input.GetKey(KeyCode.D))
        {
            moveRight();

        }
        else if (left || Input.GetKey(KeyCode.A))
        {

            moveLeft(); //move left function

        }
        else
        {
            isRunning = false;
            anim.SetBool("isRunning", isRunning);
        }

/*        if (!isGrounded())
        {
            transform.position -= new Vector3(transform.position.x, transform.position.y * Time.deltaTime * fallSpeed, transform.position.z);
        }*/

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
                Debug.Log(climb);
                Debug.Log("Going Up");

                rb.gravityScale = 0;
                rb.mass = 0;
                transform.position = new Vector3(transform.position.x, transform.position.y + climbSpeed * 0.1f, transform.position.z);

            }
            else {
                 rb.gravityScale = oldGravity;
                 rb.mass = oldMass;
            }

    }

    private void Update()
    {

        jump = jumpButton.GetComponent<JumpButtonHandler>().isJump;


        if ((isGrounded() && jump) || (isGrounded() && Input.GetKey(KeyCode.Space)))
        {
            Debug.Log("Pressed Jump");
            playLanding = true;
            rb.velocity = Vector2.up * jumpVelocity;
        }


        if (!isGrounded())
        {
            anim.SetBool("isJumping", true);       
        }
        else { 
            anim.SetBool("isJumping", false);     
        }


        if (playLanding && isGrounded())
        {  
            Debug.Log("Landing particle played");
            playLanding = false;
            ps.Play();
        }

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
        transform.position = new Vector3(transform.position.x + speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        //rb.MovePosition(new Vector2((transform.position.x + speed * Time.fixedDeltaTime), transform.position.y));

        //rb.AddForce(Vector2.right * speed, ForceMode2D.Force);

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
        transform.position = new Vector3(transform.position.x - speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);

        //rb.MovePosition(new Vector2((transform.position.x - speed * Time.fixedDeltaTime), transform.position.y));

        //rb.AddForce(Vector2.left * speed, ForceMode2D.Force );

        isRunning = true;
        anim.SetBool("isRunning", isRunning);
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center,
            boxCollider2D.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        // Debug.Log(raycastHit2d.collider);
        //Debug.DrawRay(transform.position, Vector3.down * 2, Color.green);
        return raycastHit2d.collider != null;
    }

}
