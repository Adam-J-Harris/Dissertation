using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    private float timeBetweenAttacks = 0.5f;
    private int attackDamage = 25;

    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    [SerializeField]private float randomness = 0.1f;

    StatePatternEnemy enemy;

    float timer;

    //private int damagePerShot = 25;
    //private float timeBetweenShots = 0.15f;
    private float range = 200f;

    Ray shootRay;
    RaycastHit shootHit;

    Vector3 randomRange;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        enemyHealth = GetComponentInParent<EnemyHealth>();

        enemy = GetComponentInParent<StatePatternEnemy>();
    }
	
	// Update is called once per frame
	void Update () {

        //Shoot();

        timer += Time.deltaTime;

        //if (enemy.currentState == enemy.engageState ||
        //    enemy.currentState == enemy.suppressState ||
        //    enemy.currentState == enemy.coverState ||
        //    enemy.currentState == enemy.flankState)
        //{
        //    if (timer >= timeBetweenAttacks && enemyHealth.currentHealth > 0)
        //    {
        //        Shoot();
        //    }

        //    if (playerHealth.currentHealth <= 0)
        //    {

        //    }
        //}

        if (enemy.currentState == enemy.suppressState || enemy.currentState == enemy.coverState)
        {
            randomness = 0.1f;
            timeBetweenAttacks = 0.3f;
            if (timer >= timeBetweenAttacks && enemyHealth.currentHealth > 0)
            {
                Shoot();
            }
        }
        else if (enemy.currentState == enemy.engageState)
        {
            randomness = 0.1f;
            timeBetweenAttacks = 0.5f;
            if (timer >= timeBetweenAttacks && enemyHealth.currentHealth > 0)
            {
                Shoot();
            }
        }
        else if (enemy.currentState == enemy.flankState)
        {
            randomness = 0.1f;
            timeBetweenAttacks = 0.4f;
            if (timer >= timeBetweenAttacks && enemyHealth.currentHealth > 0)
            {
                Shoot();
            }
        }
    }

    void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    void Shoot()
    {
        timer = 0f;

        randomRange = new Vector3(Random.Range(-randomness, randomness), Random.Range(-randomness, randomness));

        shootRay.origin = transform.position;
        shootRay.direction = enemy.transform.forward + randomRange;

        if (Physics.Raycast(shootRay, out shootHit, range) && shootHit.collider.CompareTag("Player"))
        {
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(attackDamage);
              
            }
        }

       // Debug.DrawRay(shootRay.origin, shootRay.direction, Color.red);    
    }
}
