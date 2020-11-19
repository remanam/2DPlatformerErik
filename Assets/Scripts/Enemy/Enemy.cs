using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;

    Animator enemyAnim;

    public Rigidbody2D rb;
    bool needToFollow;

    bool isRotated;
    bool isRunning;

    bool goRight;
    bool goLeft;

    bool needToGoRight = true;
    bool needToGoLeft = false;

    public float speed;
    public int maxHealth = 100;
    int currentHealth;


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
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Current health = " + currentHealth);
        //Play Hurt animation
        gameObject.GetComponent<Animator>().SetBool("isTakingDamage", true);

        if (currentHealth <= 0) {
            Die();
        }
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
        for (int i = 1; i < 16; i++) {
            transform.Rotate(0, 0, 1);
        }
        //rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        StartCoroutine("DestroyEnemy");
    }



    private void FixedUpdate()
    {
        HorizontalMovement();

        //Debugging with delay
/*        if (Time.fixedTime > nextTime) {
            Debug.Log("needToGoRight = " + needToGoRight);
            Debug.Log("needToGoLeft = " + needToGoLeft);
            Debug.Log("needToFollow" + needToFollow);
            nextTime = Time.fixedTime + timeStep;
        }*/
    }

    private void Update()
    {

    }


    public Vector3 point1;
    public Vector3 point2;
    public float patrolSpeed;


    private void HorizontalMovement()
    {
        if (needToGoRight /*&& needToFollow == false*/ || Input.GetKey(KeyCode.RightArrow)) {

            if (needToFollow == false)
                MoveRight();

            if (transform.position.x >= point2.x) {
                needToGoRight = false;
                needToGoLeft = true;
                //Debug.Log("Enemy changed to Left");
            }


        }
        else if (needToGoLeft /*&& needToFollow == false*/ || Input.GetKey(KeyCode.LeftArrow)) {

            if (needToFollow == false)
                MoveLeft();
            
            if (transform.position.x <= point1.x) {
                needToGoRight = true;
                needToGoLeft = false;
                //Debug.Log("Enemy changed to Right");
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
    // Function MoveLeft without moving, just animation handler
    void MoveLeftAnimation()
    {
        if (!isRotated) {
            isRotated = true;

            gameObject.transform.Rotate(0, -180, 0, Space.Self);
            //Debug.Log("Rotating Left");
        }
        isRunning = true;
        enemyAnim.SetBool("isRunning", isRunning);

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
    void MoveRightAnimation()
    {
        if (isRotated) {
            isRotated = false;
            gameObject.transform.Rotate(0, 180, 0, Space.Self);
            //Debug.Log("Rotating Right");
        }
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

    bool ifGotDamage = false;  // Переменная проверяет получал ли я 1 раз урон от ловушек 

    Vector3 distnaceToPlayer;// То есть за один вход в тригер урон нужно получать 1 раз
    private void AttackHandle(GameObject player)
    {
        if ( Time.time > nextAttack) {

            isAttacking = true;
            enemyAnim.SetBool("isAttacking", isAttacking);

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

            int i = 0; // Чтобы, если у врага найдётся 2 колайдера, урон нанёсся только один раз
            foreach (Collider2D enemy in hitEnemies) {
                if ( i == 0) {

                    player.GetComponent<Movement>().TakeDamage(attackDamage);
                    ifGotDamage = true;
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
        Vector3 enemyPos = transform.position;
        Vector3 collisionPos = collision.transform.position;
        if (collision.gameObject.tag == "Player") {
            needToFollow = true;

        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (needToFollow && collision.gameObject.tag == "Player" ) {

            if (Vector3.Distance(transform.position, collision.transform.position) >= 1.2f) {
                if (Time.fixedTime > nextTime) {
                    Debug.Log("needToGoRight = " + needToGoRight);
                    Debug.Log("needToGoLeft = " + needToGoLeft);
                    nextTime = Time.fixedTime + timeStep;
                }
                //Right animation + rotation towards player
                if (needToGoRight) {
                    bool ifRotatedonce1 = false;

                    if (ifRotatedonce1 == false) {
                        transform.rotation = collision.transform.rotation;
                    }
                    
                    MoveRightAnimation();
                }
                // Left animaion + rotation toward player
                if (needToGoLeft) {
                    bool ifRotatedOnce2 = false;

                    if (ifRotatedOnce2 == false) {

                        ifRotatedOnce2 = true;
                        transform.rotation = collision.transform.rotation;
                    }
                    MoveLeftAnimation();
                }
                rb.position = Vector2.MoveTowards(rb.position, collision.gameObject.transform.position, 0.01f * speed * Time.fixedDeltaTime);
            }

            if (Vector3.Distance(transform.position, collision.transform.position) < 1.2f) {

                
                AttackHandle(collision.gameObject);
                
            }

        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            needToFollow = false;

        ifGotDamage = false;
    }

}
