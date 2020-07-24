using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClimbuttonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public void OnPointerDown(PointerEventData eventData)
    {
        isClimb = true;
        Debug.Log("Climb Button pressed!");
        Debug.Log(isClimb);
    }
    

    public void OnPointerUp(PointerEventData eventData)
    {
        isClimb = false;
        Debug.Log("Climb Button released!");
        Debug.Log(isClimb);
    }

    public bool isClimb = false;

    public Button btn;

    


    void Update()
    {
    }
}
