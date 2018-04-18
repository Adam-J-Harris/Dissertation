using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public int scoreValue = 1;

    //private ParticleSystem hitParticles;

    public bool Hit = false;

    private bool isDead;

    void Awake()
    {
       // hitParticles = GetComponent<ParticleSystem>();

        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount , Vector3 hitPoint)
    {
        if (isDead)
            return;

        currentHealth -= amount;

        Hit = true;
  
        if (currentHealth <= 0)
        {
            ScoreManager.Enemies += scoreValue;

            Death();
        }
    }

    public void Death()
    {
        isDead = true;

        Destroy(gameObject);
    }
}
