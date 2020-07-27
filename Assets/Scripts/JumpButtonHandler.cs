using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JumpButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    [SerializeField]
    private GameObject player;


    void Start()
    { 
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pressed Jump");
        isJump = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isJump = false;
        player.GetComponent<Movement>().playLanding = true;
    }

    public bool isJump = false;
    public GameObject _object;


    void Update()
    {
 
    }
}
