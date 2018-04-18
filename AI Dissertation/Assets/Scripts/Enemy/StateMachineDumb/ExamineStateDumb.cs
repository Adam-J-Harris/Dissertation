using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineStateDumb : IEnemyStateDumb {

    private readonly StatePatternEnemyDumb enemy;

    private float searchTimer;

    public ExamineStateDumb(StatePatternEnemyDumb statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        enemy.meshRendererFlag.material.color = Color.yellow;

        Look();

        Search();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Can't use this");
    }

    public void ToAlertStateDumb()
    {
        Debug.Log("Atlert");

        enemy.currentState = enemy.alertStateDumb;

        searchTimer = 0f;
    }

    public void ToEngageStateDumb()
    {
        Debug.Log("Engage");
    }

    public void ToExamineStateDumb()
    {
        Debug.Log("Can't transition to same state ExamineState");
    }

    public void ToPatrolStateDumb()
    {
        Debug.Log("Patrol");

        enemy.currentState = enemy.patrolStateDumb;
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

    private void Search()
    {
        enemy.navMeshAgent.Stop();
        enemy.transform.Rotate(0, enemy.searchingTurnSpeed * Time.deltaTime, 0);

        searchTimer += Time.deltaTime;

        if (searchTimer >= enemy.seachingDuration)
        {
            ToPatrolStateDumb();
        }
    }
}
