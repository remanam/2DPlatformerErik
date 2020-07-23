using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JumpButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    public void OnPointerDown(PointerEventData eventData)
    {
        isJump = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isJump = false;
    }

    public bool isJump = false;
    public GameObject _object;


    void Update()
    {
 
    }
}
