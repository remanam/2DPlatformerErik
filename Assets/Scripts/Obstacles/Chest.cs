using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    ParticleSystem ps;

    private void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
        ps.Stop();
    }
    public void ChangeSprite()
    {
        spriteRenderer.sprite = newSprite;
        ps.Play();
        gameObject.GetComponent<AudioSource>().Play();
    }
    // Start is called before the first frame update

}
