using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertStateDumb : IEnemyStateDumb {

    private readonly StatePatternEnemyDumb enemy;

    public AlertStateDumb(StatePatternEnemyDumb statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        enemy.meshRendererFlag.material.color = Color.green;

        Look();
    }

    public void OnTriggerEnter(Collider other)
    {
        ToExamineStateDumb();
    }

    public void ToAlertStateDumb()
    {

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
        enemy.currentState = enemy.engageStateDumb;
    }

    private void Look()
    {
        RaycastHit hit;

        Vector3 enemyToTarget = ((enemy.chaseTarget.position + enemy.offset) - enemy.eyes.transform.position);

        if (Physics.Raycast(enemy.eyes.transform.position, enemyToTarget, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;

            ToEngageStateDumb();
        }
        else
        {
            ToExamineStateDumb();
        }
    }
}
