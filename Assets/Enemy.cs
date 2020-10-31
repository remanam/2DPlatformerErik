using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

   public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Current health = " + currentHealth);
        //Play Hurt animation
        gameObject.GetComponent<Animator>().SetBool("isTakingDamage", true);

        if (currentHealth <= 0) {
            Die();
        }
    }

    void StopTakeDamageAnimation()
    {
        gameObject.GetComponent<Animator>().SetBool("isTakingDamage", false);
    }

    private void Die()
    {
        gameObject.GetComponent<Animator>().SetBool("isDead", true);
    }
}
