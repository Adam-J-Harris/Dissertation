using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlankState : IEnemyState
{
    private readonly StatePatternEnemy enemy;

    private float rangeDist = 200;

    private int nextWayPointCover;

    private bool checkedCovers = false;

    public FlankState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        enemy.meshRendererFlag.material.color = Color.magenta;

        //Flanking();

        //Look();

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
    }

    public void ToCheckState()
    {
        Debug.Log("Check");
    }

    public void ToCoverState()
    {
        Debug.Log("Cover");
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
        Debug.Log("Can't transition to same state FlankState");
    }

    public void ToPatrolState()
    {
        Debug.Log("Patrol");
    }

    public void ToSuppressState()
    {
        Debug.Log("Suppress");
    }

    //void Flanking()
    //{
    //    enemy.inFlank = false;

    //    enemy.navMeshAgent.destination = enemy.chaseTarget.position;

    //    enemy.navMeshAgent.Resume();
    //}

    //private void Look()
    //{
    //    float feildOfViewAngle = 110f;

    //    Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - enemy.transform.position;
    //    float angle = Vector3.Angle(direction, enemy.transform.forward);

    //    if (angle < feildOfViewAngle * 0.5f)
    //    {
    //        RaycastHit hit;

    //        if (Physics.Raycast(enemy.eyes.transform.position, direction.normalized, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
    //        {
    //            enemy.inFlank = true;

    //            ToEngageState();
    //        }
    //    }
    //}

    public void ChooseCover()
    {
        for (int i = 0; i < enemy.arrayOfCovers.Length; i++)
        {
            float Dist = Vector3.Distance(enemy.arrayOfCovers[i].position, GameObject.FindGameObjectWithTag("Player").transform.position);

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

        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 10f);
    }
}