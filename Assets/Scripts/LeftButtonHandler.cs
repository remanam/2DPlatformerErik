using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public bool isMovingLeft = false;
    public GameObject _object;

    public void OnPointerDown(PointerEventData eventData)
    {
        isMovingLeft = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isMovingLeft = false;
    }
}
