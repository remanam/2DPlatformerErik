using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {

    // It's used to set isGroundFalse i guess
    [SerializeField]
    private LayerMask platformsLayerMask;

    public HealthScript healthScript;

    [SerializeField]
    private ParticleSystem ps;


    public float speed = 0.1f;
    public float jumpVelocity = 5f;
    public float climbSpeed = 2f;

    [SerializeField]
    private float maxVelocity = 15f;

    [SerializeField]
    private float fallSpeed = 0.5f;

    

    bool isCollideToSomething = false;

    private bool isRunning;
    private bool isJumping;
    private bool isAttacking;

    private bool isRotated;
    private bool canClimb;

    private bool right; // if moving right
    private bool left; // if moving left
    private bool jump; // if Jumping
    private bool climb; // If climbing

    private bool showLandingParticle;
    public bool playLanding = false;


    private float oldMass;
    private float oldGravity;

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider2D;

    // Attach buttons object to access variables
    public GameObject rightButton;
    public GameObject leftButton;
    public GameObject jumpButton;
    public GameObject climbButton;
    public GameObject attackButton;

    Vector2 velocityVector;

    private void Start()
    {
        // bool which checking if movement buttons pressed
        right = rightButton.GetComponent<RightButtonHandler>().isMovingRight;
        left = leftButton.GetComponent<LeftButtonHandler>().isMovingLeft;
        jump = jumpButton.GetComponent<JumpButtonHandler>().isJump;

        isRunning = false;
        isRotated = false;

        rb = transform.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();

        ps.Stop();

        climb = climbButton.GetComponent<ClimbuttonHandler>().isClimb;
        

        Vector2 velocityVector = rb.velocity;
    }

    void FixedUpdate()
    {
        right = rightButton.GetComponent<RightButtonHandler>().isMovingRight;
        left = leftButton.GetComponent<LeftButtonHandler>().isMovingLeft;
        jump = jumpButton.GetComponent<JumpButtonHandler>().isJump;

        HorizontalMovement();
        //print(rb.velocity.x);

        //RunAnimation();
        AtackHandle();

        ClimbMove();
        
    }

    private void Update()
    {
        JumpMove();

    }


    private void ClimbMove()
    {
        if (canClimb && climbButton.GetComponent<ClimbuttonHandler>().isClimb) {
            Debug.Log(climb);
            Debug.Log("Going Up");

            //rb.gravityScale = 0;
            //rb.mass = 0;
            transform.position = new Vector3(transform.position.x, transform.position.y + climbSpeed * Time.deltaTime, transform.position.z);

            //rb.AddForce(Vector2.up * climbSpeed);
        }
        else {
            //rb.gravityScale = oldGravity;
            //rb.mass = oldMass;
        }
    }


    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float attackRange = 0.5f;

    public int attackDamage = 1;

    //bool ifTookDamage = false; // Check if enemy got damage from one sword swing Кажется не нужно это вообще
    float attackDelay = 0.5f; // Delay between attacks
    float nextAttack;

    private void AtackHandle()
    {
        if (attackButton.GetComponent<AttackButtonHandler>().isAttacking == true && Time.time > nextAttack) {

            isAttacking = true;
            anim.SetBool("isAtacking", isAttacking);

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            

            int i = 0; // Чтобы, если у врага найдётся 2 колайдера, урон нанёсся только один раз
            foreach(Collider2D enemy in hitEnemies) {
                if (/*ifTookDamage == false &&*/ i == 0) {
                    
                    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                    ifGotDamage = true;
                    i = 1;
                }
            }
            nextAttack = Time.time + attackDelay; // Каждый одинаковый промежуток времени будет возможжность атаки 
        }
    }

    void StopAttack()// Called when Animation is finished
    {
        anim.SetBool("isAtacking", false);
    }

    //Поможет видеть графически радиусы raycast и тд
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }



    private void RunAnimation() 
    {
        if (right || Input.GetKey(KeyCode.D)) {
            isRunning = true;
            anim.SetBool("isRunning", isRunning);
        }
        else if (left || Input.GetKey(KeyCode.A)) {
            isRunning = true;
            anim.SetBool("isRunning", isRunning);
        }
    }



    bool ifGotDamage = false;  // Переменная проверяет получал ли я 1 раз урон от ловушек 
                               // То есть за один вход в тригер урон нужно получать 1 раз



    private void OnTriggerEnter2D(Collider2D collision)
    {
        //jumpButton.GetComponent<JumpButtonHandler>().gameObject.SetActive(false);
        //climbButton.GetComponent<ClimbuttonHandler>().btn.gameObject.SetActive(true);




        if (collision.gameObject.tag == "Stairs")
        {         
            canClimb = true;
            //Debug.Log("Entered Stairs");
            //Debug.Log(canClimb);
        }


        if (collision.gameObject.tag == "Chest") {
            collision.gameObject.GetComponent<Chest>().ChangeSprite();
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        jumpButton.GetComponent<JumpButtonHandler>().gameObject.SetActive(true);
        //climbButton.GetComponent<ClimbuttonHandler>().btn.gameObject.SetActive(false);
        
        canClimb = false;
        //Debug.Log("Exited Stairs");
        //Debug.Log(canClimb);

        //rb.gravityScale = oldGravity;
        //rb.mass = oldMass;
        ifGotDamage = false;

        anim.SetBool("isTakingDamage", false);
    }

    private void JumpMove()
    {
        if ((isGrounded() && jump) || (isGrounded() && Input.GetKey(KeyCode.Space))) {
            //Debug.Log("Pressed Jump");
            rb.velocity = Vector2.up * jumpVelocity;

            //rb.MovePosition(new Vector2(transform.position.x, transform.position.y + jumpVelocity * Time.deltaTime));
            //rb.AddForce(Vector2.up * jumpVelocity * Time.deltaTime, ForceMode2D.Impulse);
        }

        if (!isGrounded()) {
            anim.SetBool("isJumping", true);

        }
        else {
            anim.SetBool("isJumping", false);
            playLanding = !playLanding;
        }

        if (playLanding && isGrounded()) {
            playLanding = false;
            ps.Play();
        }
    }

    private void HorizontalMovement()
    {

        if (right || Input.GetKey(KeyCode.D) ) {
            MoveRight(); // move right function

        }
        else if (left || Input.GetKey(KeyCode.A)) {

            MoveLeft(); //move left function
        }
        else {

            isRunning = false;
            anim.SetBool("isRunning", isRunning);
        }

    }

    private void MoveRight()
    {
        if (isRotated) {
            isRotated = false;
            transform.Rotate(0, 180, 0, Space.Self);
            //Debug.Log("Rotating Right");
        }
        isRunning = true;
        anim.SetBool("isRunning", isRunning);
        //transform.position = new Vector3(transform.position.x + speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);

        var newPositionRight = new Vector2(speed * Time.deltaTime, rb.velocity.y);
        rb.velocity = newPositionRight;

        //rb.MovePosition(new Vector2((transform.position.x + speed * Time.deltaTime), transform.position.y));

        //isRunning = true;
        //anim.SetBool("isRunning", isRunning);
    }

    private void MoveLeft()
    {
        if (!isRotated) {
            isRotated = true;
            transform.Rotate(0, -180, 0, Space.Self);
            //Debug.Log("Rotating Left");
        }
        //transform.position = new Vector3(transform.position.x - speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        isRunning = true;
        anim.SetBool("isRunning", isRunning);

        var newPositionLeft = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
        rb.velocity = newPositionLeft; 

        //rb.MovePosition(new Vector2((transform.position.x - speed * Time.fixedDeltaTime), transform.position.y));

        //isRunning = true;
        //anim.SetBool("isRunning", isRunning);
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center,
            boxCollider2D.bounds.size, 0f, Vector2.down, .2f, platformsLayerMask);
        // Debug.Log(raycastHit2d.collider);
        //Debug.DrawRay(transform.position, Vector3.down * 2, Color.green);
        if (raycastHit2d.collider != null) {
            return true;     
        }
        else {
            return false;          
        }          
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "movingPlatform") {
            gameObject.transform.parent = collision.transform;
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "movingPlatform") {
            gameObject.transform.parent = null;
        }
    }

    public void TakeDamage(int damage)
    {
        healthScript.GetDamage(damage);
        ifGotDamage = true;
        Debug.Log("Get damage set to True");
        anim.SetBool("isTakingDamage", true);
    }


}
