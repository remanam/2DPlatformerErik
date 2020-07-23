using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClimbuttonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public void OnPointerDown(PointerEventData eventData)
    {
        isClimb = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isClimb = false;
    }

    public bool isClimb = false;

    public GameObject _object;
    public Button btn;

    


    void Update()
    {
    }
}
