using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScript : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
        
    }
}
