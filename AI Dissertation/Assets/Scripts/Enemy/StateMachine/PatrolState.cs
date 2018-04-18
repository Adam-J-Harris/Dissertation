using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private readonly StatePatternEnemy enemy;

    GameObject player;

    private int nextWayPoint;

    public PatrolState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void UpdateState()
    {
        enemy.meshRendererFlag.material.color = Color.green;

        Look();

        Patrol();

        if (Input.GetButtonDown("Fire1"))
        {
            float Dist = Vector3.Distance(enemy.transform.position, player.transform.position);

            if (Dist < (enemy.hearRange))
            {
                ToCheckState();
            }
        }

        if (enemy.enemyHealth.Hit == true)
        {
            enemy.enemyHealth.Hit = false;
            ToAlertState();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ToExamineState();
        }
    }

    public void ToAlertState()
    {
        Debug.Log("Atlert");
 
        enemy.currentState = enemy.alertState;
    }

    public void ToCheckState()
    {
        Debug.Log("Check");

        enemy.currentState = enemy.checkState;
    }

    public void ToCoverState()
    {
        Debug.Log("Cover");
    }

    public void ToEngageState()
    {
        Debug.Log("Engage");
    }

    public  void ToExamineState()
    {
        Debug.Log("Examine");

        enemy.currentState = enemy.examineState;
    }

    public void ToFlankState()
    {
        Debug.Log("Flank");
    }

    public void ToPatrolState()
    {
        Debug.Log("Can't transition to same state PatrolState");
    }

    public void ToSuppressState()
    {
        Debug.Log("Suppress");
    }

    private void Look()
    {
        float feildOfViewAngle = 110f;
        
        Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - enemy.transform.position;
        float angle = Vector3.Angle(direction, enemy.transform.forward);

        if (angle < feildOfViewAngle * 0.5f)
        {
            RaycastHit hit;

            if (Physics.Raycast(enemy.eyes.transform.position, direction.normalized, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
            {
                enemy.chaseTarget = hit.transform;

                ToAlertState();
            }
        }
    }

    void Patrol()
    {
        enemy.navMeshAgent.destination = enemy.wayPoints[nextWayPoint].position;

        enemy.navMeshAgent.updateRotation = true;

        enemy.inCover = false;

        enemy.navMeshAgent.Resume();

        if(enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            nextWayPoint = (nextWayPoint + 1) % enemy.wayPoints.Length;
        }
    }
}
