using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPOPUP : MonoBehaviour
{
    void Start()
    {

        //DamagePopup.Create(Vector3.zero, 200);
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            // Convert mouse position to worldCoordinates
            DamagePopup.Create(mousePos, 200);
        }
    }


}
