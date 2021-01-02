using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    [SerializeField] Vector3 movePosition;

    public float collisionSpeed = 0.3f;
    bool isColliding = false;

    [SerializeField] float moveSpeed = 0.7f;

    [Range(0, 1)] [SerializeField] float moveProgress; //0 - object didn't move, object moved full interval

    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {

        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveProgress = Mathf.PingPong(Time.time * moveSpeed, 1);

        Vector3 offset = movePosition * moveProgress;
        transform.position = startPosition + offset;
    }

    public int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            if (isColliding) return;
            isColliding = true;
            // Rest of the code
            StartCoroutine(Reset());

            
            collision.gameObject.GetComponent<Movement>().TakeDamage(damage);
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(collisionSpeed);
        isColliding = false;
    }
}
