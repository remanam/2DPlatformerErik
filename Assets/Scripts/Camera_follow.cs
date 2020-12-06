using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow : MonoBehaviour
{



    public Transform player;

    //This transform for backround movement
    // public Transform backgroundTransform;

    private void Start()
    {
        //backgroundSPrite image
        //backgroundTransform.GetComponentInChildren<Transform>();
        
    }
    void FixedUpdate()
    {
        
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        
    }
}
