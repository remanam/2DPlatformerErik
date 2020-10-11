using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    

    public  int health;
    public int numberOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        for (int i = 0; i < hearts.Length; i++) {
            if (i < numberOfHearts && i < hearts.Length) {
                hearts[i].enabled = true;
            }
            else {
                hearts[i].enabled = false;
            }
            hearts[i].sprite = fullHeart;
        }
         
    }

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++) {

            

            if(health == 0) {
                hearts[i].sprite = emptyHeart;
                
            }
            else {
                if ((i + 1) <= health / 2) {
                    hearts[i].sprite = fullHeart;
                }
                else if (health % 2 == 1 && health/2 >=  i) {
                    hearts[i].sprite = halfHeart;
                }
                else {
                    hearts[i].sprite = emptyHeart;
                }
            }   
            
        }
    }

    public void GetDamage(int x)
    {
        health -= x;
    }
}
