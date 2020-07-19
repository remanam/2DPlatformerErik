using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private LayerMask platformsLayerMask;


    public float speed = 0.1f;
    public float jumpVelocity = 5f;
    public float climbVelocity = 2f;
    
    private bool isRunning;
    private bool isJumping;
    private bool isRotated;
    private bool canClimb;

    private float oldMass;
    private float oldGravity;

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider2D;


    private void Start()
    {
        isRunning = false;
        rb = transform.GetComponent<Rigidbody2D>();
        isRotated = false;
        anim = GetComponent<Animator>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();

        oldGravity = rb.gravityScale;
        oldMass = rb.mass;

    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (isRotated)
            {
                isRotated = false;
                transform.Rotate(0, 180, 0, Space.Self);
                Debug.Log("Rotating Right");
            }
            transform.position = new Vector3(transform.position.x + speed * 0.1f, transform.position.y, transform.position.z);

            isRunning = true;
            anim.SetBool("isRunning", isRunning);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (!isRotated)
            {
                isRotated = true;
                transform.Rotate(0, -180, 0, Space.Self);
                Debug.Log("Rotating Left");
            }
            transform.position = new Vector3(transform.position.x - speed * 0.1f, transform.position.y, transform.position.z);

            isRunning = true;
            anim.SetBool("isRunning", isRunning);
        }
        else
        {
            isRunning = false;
            anim.SetBool("isRunning", isRunning);
        }




        if (canClimb)
        {

            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("Going Up");

                rb.gravityScale = 0;
                rb.mass = 0;
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * 0.1f, transform.position.z);
            }

        }






    }

    private void Update()
    {

        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
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





        if (collision.gameObject.tag == "Stairs")
        {
            canClimb = true;
            Debug.Log("Entered Stairs");
            Debug.Log(canClimb);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canClimb = false;
        Debug.Log("Exited Stairs");
        Debug.Log(canClimb);

        rb.gravityScale = oldGravity;
        rb.mass = oldMass;
    }

}
