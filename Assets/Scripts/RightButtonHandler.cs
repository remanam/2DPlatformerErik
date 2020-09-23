using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RightButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public bool isMovingRight = false;
    public GameObject _object;

    public void OnPointerDown(PointerEventData eventData)
    {
        isMovingRight = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isMovingRight = false;
    }


}
