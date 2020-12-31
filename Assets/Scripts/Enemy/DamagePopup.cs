using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public float XOffset;
    public float YOffset;
    
    //public Vector3 moveVector;


    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;

    private const float DISSAPPEAR_TIMER_MAX = 1f;
   
    public float increaseScaleAmount;
    public float decreaseScaleAmount;

    //Prevents older popups to overlap new ones
    private static int sortingOrder;

    //Create a damage popup
    public static DamagePopup Create(Vector3 position, int damageAmount)
    {
        Transform transformDamagePopup = Instantiate(GameAssets.i.defaultDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = transformDamagePopup.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damage)
    {
        textMesh.SetText(damage.ToString());
        textColor = textMesh.color;
        disappearTimer = DISSAPPEAR_TIMER_MAX;

        //every new popup will be on top of the earlier ones
        sortingOrder += 1;
        textMesh.sortingOrder = sortingOrder;
    }



    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, YOffset) * Time.deltaTime;

        // increase and decrease scale
        if (disappearTimer > DISSAPPEAR_TIMER_MAX * .5f) {
            //Fist half of the popup
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;

        } else {
            //Second half of the popup
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }


        //Damage dissappearing and after we destroy object
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0) {
            //Start disappearing
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a < 0)
                Destroy(gameObject);
        }
    }
}
