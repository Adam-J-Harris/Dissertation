using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverState : IEnemyState
{
    private readonly StatePatternEnemy enemy;

    private float rangeDist = 100;

    private int nextWayPointCover;

    private bool checkedCovers = false;
    
    public CoverState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        enemy.meshRendererFlag.material.color = Color.blue;

        if (!checkedCovers)
        {
            ChooseCover();
        }

        GoToCover();

        RotateTowards();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Can't use this");
    }

    public void ToAlertState()
    {
        Debug.Log("Atlert");

        enemy.currentState = enemy.alertState;
    }

    public void ToCheckState()
    {
        Debug.Log("Check");
    }

    public void ToCoverState()
    {
        Debug.Log("Can't transition to same state CoverState");
    }

    public void ToEngageState()
    {
        Debug.Log("Engage");

        enemy.currentState = enemy.engageState;
    }

    public void ToExamineState()
    {
        Debug.Log("Examine");
    }

    public void ToFlankState()
    {
        Debug.Log("Flank");
    }

    public void ToPatrolState()
    {
        Debug.Log("Patrol");
    }

    public void ToSuppressState()
    {
        Debug.Log("Suppress");
    }

    public void ChooseCover()
    {
        for (int i = 0; i < enemy.arrayOfCovers.Length; i++)
        {

            float Dist = Vector3.Distance(enemy.arrayOfCovers[i].position, enemy.transform.position);

            if (Dist < rangeDist)
            {
                rangeDist = Dist;
                enemy.wayPointCover = enemy.arrayOfCovers[i].transform;
            }
        }

        checkedCovers = true;
    }

    public void GoToCover()
    {
        enemy.inCover = false;

        enemy.navMeshAgent.destination = enemy.wayPointCover.transform.position;

        enemy.navMeshAgent.updateRotation = false;

        enemy.navMeshAgent.Resume();

        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            enemy.navMeshAgent.Stop();

            enemy.inCover = true;

            checkedCovers = false;

            Reset();

            ToEngageState();
        }
    }

    private void Reset()
    {
        enemy.navMeshAgent.updateRotation = true;

        enemy.navMeshAgent.Stop();
    }

    private void RotateTowards()
    {
        Vector3 direction = (enemy.chaseTarget.transform.position - enemy.transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));

        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation , Time.deltaTime * 10f);
    }
}