using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 0.1f;
    Rigidbody2D rb;
    private bool isRunning;
    private bool isRotated;

    private Animator anim;


    private void Start()
    {
        isRunning = false;
        rb = GetComponent<Rigidbody2D>();
        isRotated = false;
        anim = GetComponent<Animator>();

    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D) )
        {
            if (isRotated){
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
            //anim.Play("idle");
        }




    }

}
