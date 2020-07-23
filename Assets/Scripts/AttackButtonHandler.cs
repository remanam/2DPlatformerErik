using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    public void OnPointerDown(PointerEventData eventData)
    {
        isAttacking = true;
        Debug.Log("AttackButton pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isAttacking = false;
        Debug.Log("AttackButton released");
    }

    public bool isAttacking = false;
    public GameObject _object;
    void Update()
    {
        
    }
}
