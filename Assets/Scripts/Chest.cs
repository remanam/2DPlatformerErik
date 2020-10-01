using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public void ChangeSprite()
    {
        spriteRenderer.sprite = newSprite;
    }
    // Start is called before the first frame update

}
