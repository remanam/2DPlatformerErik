using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelLoader_gameover : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    bool needToCoroutine = true;

    [SerializeField]
    private Button playAgain;

    [SerializeField]
    private Button toMainMenu;
     
    void Start()
    {
        playAgain.onClick.AddListener(OnClickPlayAgain);

        toMainMenu.onClick.AddListener(OnClickToMainMenu);
    }

    void OnClickPlayAgain()
    {
        StartCoroutine(LoadLevel("SampleScene"));
    }

    void OnClickToMainMenu()
    {
        StartCoroutine(LoadLevel("MainMenu"));
    }

    // Update is called once per frame
    void Update()
    {


    }


    IEnumerator LoadLevel(string levelname)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelname);

        //Load scene
    }


}
