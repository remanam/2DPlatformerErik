using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingObject : MonoBehaviour
{
    [SerializeField] Vector3 movePosition;

    //public float collisionSpeed = 0.3f;
    bool isColliding = false;

    private Rigidbody2D rb;

    // Rotation speed of gameobject
    [SerializeField] private float rotationSpeed;


    [SerializeField] float moveSpeed = 0.7f;

    [Range(0, 1)] [SerializeField] float moveProgress; //0 - object didn't move, object moved full interval

    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {

        startPosition = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveProgress = Mathf.PingPong(Time.time * moveSpeed, 1);

        Vector3 offset = movePosition * moveProgress;
        //transform.position = startPosition + offset;

        //Rolling handle
        if (rb.velocity.x > 0) {
            Roll_Right();
        }else if (rb.velocity.x < 0) {
            Roll_Left();
        }
    }

    private void Roll_Left()
    {
        transform.Rotate(0, 0, - rotationSpeed * Time.fixedDeltaTime);
    }

    private void Roll_Right()
    {
        transform.Rotate(0, 0, + rotationSpeed * Time.fixedDeltaTime);
    }

/*    public int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            if (isColliding) return;
            isColliding = true;
            // Rest of the code
            StartCoroutine(Reset());

        }
    }*/

/*    IEnumerator Reset()
    {
        yield return new WaitForSeconds(collisionSpeed);
        isColliding = false;
    }*/
}
