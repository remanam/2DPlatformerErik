﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    GameObject player;

    Animator enemyAnim;

    public Rigidbody2D rb;
    bool needToFollow;

    bool isRotated;
    bool isRunning;

    bool needToGoRight = true;
    bool needToGoLeft = false;

    public float speed;
    public int maxHealth = 100;
    public int currentHealth;

    public Text textDamage;



    float nowTime;

     public float timeStep = 0.9f;
    float nextTime;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        enemyAnim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        isRotated = false;
        transform.position = new Vector3(point1.x +0.01f, point1.y, point1.z);

        nextTime = Time.time + timeStep;

        textDamage = GetComponentInChildren<Text>();
    }

    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        if (currentHealth <= 0 ) {
            Die();
            
        }

        Debug.Log("Current health = " + currentHealth);
        //Play Hurt animation
        gameObject.GetComponent<Animator>().SetBool("isTakingDamage", true);

        // Показ уровна
        DamagePopup.Create(transform.position + new Vector3(-0.3f , 0.5f, 0), 2);


    }

    void StopTakeDamageAnimation()
    {
        gameObject.GetComponent<Animator>().SetBool("isTakingDamage", false);
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
    private void Die()
    {

        gameObject.GetComponent<Animator>().SetBool("isDead", true);
        StartCoroutine("DestroyEnemy");
        for (int i = 1; i < 16; i++) {
            transform.Rotate(0, 0, 1);
        }
        //rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        
    }



    private void FixedUpdate()
    {
        HorizontalMovement();

                
    }


    public Vector3 point1;
    public Vector3 point2;
    public float patrolSpeed;

    private void HorizontalMovement()
    {
        if (needToGoRight  || Input.GetKey(KeyCode.RightArrow)) {

            if (needToFollow == false) {
                MoveRight();

                if (transform.position.x >= point2.x) {
                    needToGoRight = false;
                    needToGoLeft = true;
                    //Debug.Log("Enemy changed to Left");
                }
            }

        }
        else if (needToGoLeft  || Input.GetKey(KeyCode.LeftArrow)) {

            if (needToFollow == false) {
                MoveLeft();
            
                if (transform.position.x <= point1.x) {
                    needToGoRight = true;
                    needToGoLeft = false;
                    //Debug.Log("Enemy changed to Right");
                }
            }
            
        }
        else {

            isRunning = false;
            enemyAnim.SetBool("isRunning", isRunning);
        }
        
    }

    private void MoveLeft()
    {
        if (!isRotated) {
            isRotated = true;

            gameObject.transform.Rotate(0, -180, 0, Space.Self);
            //Debug.Log("Rotating Left");
        }
        //transform.position = new Vector3(transform.position.x - speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        isRunning = true;
        enemyAnim.SetBool("isRunning", isRunning);

        var newPositionLeft = new Vector2(-speed * Time.fixedDeltaTime, rb.velocity.y);

        rb.velocity = newPositionLeft;

    }


    private void MoveRight()
    {
        if (isRotated) {
            isRotated = false;
            gameObject.transform.Rotate(0, 180, 0, Space.Self);
            //Debug.Log("Rotating Right");
        }
        isRunning = true;
        enemyAnim.SetBool("isRunning", isRunning);
        //transform.position = new Vector3(transform.position.x + speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);

        var newPositionRight = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);

        rb.velocity = newPositionRight;

    }
    // Function MoveRight without moving, just animation handler
    void RunAnimation()
    {
        isRunning = true;
        enemyAnim.SetBool("isRunning", isRunning);
    }

    // Attack variables
    bool isAttacking;
    public Transform attackPoint;
    public float attackRange;
    public int attackDamage;
    public LayerMask playerLayer;

    public float nextAttack;
    public float attackDelay = 0.8f;

    bool ifGotDamage = false;  // Позволяет получать урон от ловушек раз в 0.5 сек

    Vector3 distnaceToPlayer;
    private void AttackHandle(GameObject player)
    {
        if ( Time.time > nextAttack) {

            isAttacking = true;
            enemyAnim.SetBool("isAttacking", isAttacking);

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

            int i = 0; // Чтобы, если у врага найдётся 2 колайдера, урон нанёсся только один раз
            foreach (Collider2D enemy in hitEnemies) {
                if ( i == 0 && enemyAnim.GetBool("isDead") == false) {

                    player.GetComponent<Movement>().TakeDamage(attackDamage);

                    i = 1;
                }
            }
            nextAttack = Time.time + attackDelay; // Каждый одинаковый промежуток времени будет возможжность атаки 
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void StopAttack()// Called when Animation is finished
    {
        enemyAnim.SetBool("isAttacking", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player") {
            needToFollow = true;

        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {


        if (needToFollow && collision.gameObject.tag == "Player" ) {

            RunAnimationHandler(collision);

            FollowAndAttackHandle(collision);

        }
    }

    void RunAnimationHandler(Collider2D collision)
    {
        Vector3 enemyPos = transform.position;
        Vector3 collisionPos = collision.transform.position;
        //Right animation + rotation towards player
        if (needToGoRight) {

            if (isRotated == true /*&& enemyPos.x < collisionPos.x*/) {

                transform.Rotate(0, 180, 0, Space.Self);
                isRotated = false;


            }

            if (enemyPos.x > collisionPos.x) {
                needToGoRight = false;
                needToGoLeft = true;
            }

            RunAnimation();
        }
        // Left animaion + rotation toward player
        if (needToGoLeft) {

            if (isRotated == false /*&& enemyPos.x > collisionPos.x*/) {

                transform.Rotate(0, -180, 0, Space.Self);
                isRotated = true;

            }

            if (enemyPos.x < collisionPos.x) {
                needToGoRight = true;
                needToGoLeft = false;
            }

            RunAnimation();
        }
    }

    void FollowAndAttackHandle(Collider2D collision)
    {
        if (Vector3.Distance(transform.position, collision.transform.position) >= 1.2f) {
            if (Time.fixedTime > nextTime) {
                Debug.Log("needToGoRight = " + needToGoRight);
                Debug.Log("needToGoLeft = " + needToGoLeft);
                nextTime = Time.fixedTime + timeStep;
            }

            rb.position = Vector2.MoveTowards(rb.position, collision.gameObject.transform.position, 0.011f * speed * Time.fixedDeltaTime);

        }
        else if (Vector3.Distance(transform.position, collision.transform.position) < 1.2f) {

            AttackHandle(collision.gameObject);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            needToFollow = false;

        ifGotDamage = false;
    }

}
