using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilScript : MonoBehaviour
{
    
    //Anvil falls, and then it's laid on the floor with BodyType Kinematic
    bool isFallen = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            if (!isFallen) {
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                isFallen = true;
            }
            StartCoroutine(Delay(1f));

        }
    }

    IEnumerator Delay(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;




    }

}
