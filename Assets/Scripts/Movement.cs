using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform player;
    public float speed = 0.1f;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            player.position = new Vector3(transform.position.x + speed * 0.1f, transform.position.y, transform.position.z);
            Debug.Log("Going RIGHT");
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Going LEFT");
            player.position = new Vector3(transform.position.x - speed * 0.1f, transform.position.y, transform.position.z);
        }
    }

}
