using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    public Animator transition;

    [SerializeField]
    private GameObject player;

    public float transitionTime = 1f;

    bool needToCoroutine = true;


    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < -5 && needToCoroutine == true) {
            //StartCoroutine(LoadLevel("MainMenu"));
            StartCoroutine(LoadLevel("GameOver"));
            
            needToCoroutine = false;
        }
            

    }


    IEnumerator LoadLevel(string levelname)
    {
        transition.SetTrigger("Start");


        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelname);

        //Load scene
    }

}
