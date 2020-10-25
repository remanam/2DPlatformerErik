using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    public Animator transition;
    public Animator playerAnimator;


    public GameObject healthScript;
    public GameObject player;


    public float transitionTime = 1.5f;

    bool needToCoroutine = true;


    // Update is called once per frame
    void Update()
    {

        if (player.transform.position.y < -5 && needToCoroutine == true ) {
            //StartCoroutine(LoadLevel("MainMenu"));
            StartCoroutine(LoadLevel("GameOver", 1f));
            playerAnimator.SetBool("isDead", true);
            needToCoroutine = false;
        }

        if (healthScript.GetComponent<HealthScript>().health <= 0 && needToCoroutine == true) {
            //StartCoroutine(LoadLevel("MainMenu"));
            StartCoroutine(LoadLevel("GameOver", 1f));
            playerAnimator.SetBool("isDead", true);
            needToCoroutine = false;
        }

    }


    IEnumerator LoadLevel(string levelname, float delay)
    {


        transition.SetTrigger("Start");
        yield return new WaitForSeconds(delay);
        
        SceneManager.LoadScene(levelname);

        //Load scene
    }

}
