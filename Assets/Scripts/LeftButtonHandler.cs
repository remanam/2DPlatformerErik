﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    public void OnPointerDown(PointerEventData eventData)
    {
        isMovingLeft = true;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isMovingLeft = false;

    }

    public bool isMovingLeft = false;
    public GameObject _object;


    void Update()
    {
       // if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {
       //     isMovingLeft = true;
       // }
       // else isMovingLeft = false;


    }   
}
