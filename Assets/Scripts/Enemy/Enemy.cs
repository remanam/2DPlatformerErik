using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3[] Positions;

    Animator enemyAnim;

    public Rigidbody2D rb;

    bool isRotated;
    bool isRunning;

    bool goRight;
    bool goLeft;

    public float speed;
    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        enemyAnim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        isRotated = false;
        transform.position = Positions[0];
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

    private void Die()
    {
        
        gameObject.GetComponent<Animator>().SetBool("isDead", true);
        for(int i = 1; i < 16 ; i++) {
            transform.Rotate(0, 0, 1);
        }
        StartCoroutine("DestroyEnemy");
    }


    IEnumerator  DestroyEnemy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }


    private void FixedUpdate()
    {
        HorizontalMovement();
        
    }

    bool needToGoRight = true;
    bool needToGoLeft = false;
    private void HorizontalMovement()
    {
        if (needToGoRight || Input.GetKey(KeyCode.RightArrow)) {
        //if (transform.position.x < Positions[1].x && needToGoRight) {
            if (transform.position.x >= Positions[1].x) {
                Debug.Log("Changed to Left");
                needToGoRight = false;
                needToGoLeft = true;
            }

            if (transform.position.x < Positions[1].x)
                MoveRight(); // move right function
        }
        else if (needToGoLeft  || Input.GetKey(KeyCode.LeftArrow)) {
        //else if (transform.position.x > Positions[0].x && needToGoLeft) {
            if (transform.position.x <= Positions[0].x) {
                Debug.Log("Changed to Right");
                needToGoRight = true;
                needToGoLeft = false;
            }

            if (transform.position.x > Positions[0].x)
                MoveLeft(); //move left function
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
            Debug.Log("Rotating Left");
        }
        //transform.position = new Vector3(transform.position.x - speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        isRunning = true;
        enemyAnim.SetBool("isRunning", isRunning);

        var newPositionLeft = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
        rb.velocity = newPositionLeft;

    }

    private void MoveRight()
    {
        if (isRotated) {
            isRotated = false;
            gameObject.transform.Rotate(0, 180, 0, Space.Self);
            Debug.Log("Rotating Right");
        }
        isRunning = true;
        enemyAnim.SetBool("isRunning", isRunning);
        //transform.position = new Vector3(transform.position.x + speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);

        var newPositionRight = new Vector2(speed * Time.deltaTime, rb.velocity.y);
        rb.velocity = newPositionRight;

    }


}
