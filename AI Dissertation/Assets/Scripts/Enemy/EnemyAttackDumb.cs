using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDumb : MonoBehaviour {

    private float timeBetweenAttacks = 1f;
    private int attackDamage = 25;

    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    StatePatternEnemyDumb enemy;

    float timer;

    private int damagePerShot = 25;
    private float timeBetweenShots = 0.15f;
    private float range = 100f;

    Ray shootRay;
    RaycastHit shootHit;

    Vector3 randomRange;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        enemyHealth = GetComponentInParent<EnemyHealth>();

        enemy = GetComponentInParent<StatePatternEnemyDumb>();
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (enemy.currentState == enemy.engageStateDumb)
        {
            if (timer >= timeBetweenAttacks && enemyHealth.currentHealth > 0)
            {
                Shoot();
            }

            if (playerHealth.currentHealth <= 0)
            {

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

        shootRay.origin = transform.position;
        shootRay.direction = enemy.transform.forward;

        randomRange = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5));

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
