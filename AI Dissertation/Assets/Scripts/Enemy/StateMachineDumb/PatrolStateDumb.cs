using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolStateDumb : IEnemyStateDumb {

    private readonly StatePatternEnemyDumb enemy;

    private int nextWayPoint;

    public PatrolStateDumb(StatePatternEnemyDumb statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        enemy.meshRendererFlag.material.color = Color.green;

        Look();

        Patrol();
    }

    public void OnTriggerEnter(Collider other)
    {
        ToExamineStateDumb();
    }

    public void ToAlertStateDumb()
    {
        enemy.currentState = enemy.alertStateDumb;
    }

    public void ToExamineStateDumb()
    {
        enemy.currentState = enemy.examineStateDumb;
    }

    public void ToPatrolStateDumb()
    {

    }

    public void ToEngageStateDumb()
    {

    }

    private void Look()
    {
        RaycastHit hit;

        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;

            ToAlertStateDumb();
        }
    }

    private void Patrol()
    {
        enemy.navMeshAgent.destination = enemy.wayPoints[nextWayPoint].position;
        enemy.navMeshAgent.Resume();

        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            nextWayPoint = (nextWayPoint + 1) % enemy.wayPoints.Length;
        }
    }
}
