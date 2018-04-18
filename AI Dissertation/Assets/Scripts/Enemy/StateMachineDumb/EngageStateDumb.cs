using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngageStateDumb : IEnemyStateDumb {

    private readonly StatePatternEnemyDumb enemy;

    private float searchTimer;
    private float seachLimit = 5f;

    public EngageStateDumb(StatePatternEnemyDumb statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        enemy.meshRendererFlag.material.color = Color.grey;

        searchTimer += Time.deltaTime;

        if (searchTimer >= seachLimit)
        {
            Reset();

            ToAlertStateDumb();
        }

        Look();

        Engaging();

        RotateTowards();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Can't use this");
    }

    public void ToAlertStateDumb()
    {
        Debug.Log("Atlert");

        enemy.currentState = enemy.alertStateDumb;
    }

    public void ToEngageStateDumb()
    {
        Debug.Log("Can't transition to same state EngageState");
    }

    public void ToExamineStateDumb()
    {
        Debug.Log("Examine");
    }

    public void ToPatrolStateDumb()
    {
        Debug.Log("Patrol");
    }

    private void Engaging()
    {
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;

        enemy.navMeshAgent.updateRotation = false;
    }

    private void Look()
    {
        RaycastHit hit;

        Vector3 enemyToTarget = ((enemy.chaseTarget.position + enemy.offset) - enemy.eyes.transform.position);

        if (Physics.Raycast(enemy.eyes.transform.position, enemyToTarget, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;

            searchTimer = 0f;
        }
    }

    private void Reset()
    {
        enemy.navMeshAgent.updatePosition = true;
        enemy.navMeshAgent.updateRotation = true;

        enemy.navMeshAgent.Stop();
    }

    private void RotateTowards()
    {
        Vector3 direction = (enemy.chaseTarget.transform.position - enemy.transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 10f);
    }
}
