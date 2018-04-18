using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngageState : IEnemyState
{
    private readonly StatePatternEnemy enemy;

    private float searchTimer;
    private float seachLimit = 10f;

    Player playerBools;

    public EngageState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;

        playerBools = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void UpdateState()
    {
        enemy.meshRendererFlag.material.color = Color.grey;

        searchTimer += Time.deltaTime;

        EngageType();

        Look();

        Engaging();

        RotateTowards();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Can't use this");
    }

    public void ToAlertState()
    {
        Debug.Log("Atlert");

        searchTimer = 0f;

        enemy.currentState = enemy.alertState;
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
        Debug.Log("Can't transition to same state EngageState");
    }

    public void ToExamineState()
    {
        Debug.Log("Examine");
    }

    public void ToFlankState()
    {
        Debug.Log("Flank");

        searchTimer = 0f;

        enemy.currentState = enemy.flankState;
    }

    public void ToPatrolState()
    {
        Debug.Log("Patrol");
    }

    public void ToSuppressState()
    {
        Debug.Log("Suppress");

        searchTimer = 0f;

        enemy.currentState = enemy.suppressState;
    }

    public void EngageType()
    {
        if (!enemy.inSuppress && playerBools.inSuppression == true)
        {
            ToFlankState();
        }
        else if (searchTimer >= 5f)
        {
            seachLimit = 3f;

            ToSuppressState();
        }
        else if (searchTimer >= seachLimit)
        {
            Reset();

            enemy.inCover = false;

            ToAlertState();
        }
    }

    public void Engaging()
    {
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;

        enemy.navMeshAgent.updateRotation = false;
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

                searchTimer = 0f;
            }
        }
    }

    private void Reset()
    {
        enemy.navMeshAgent.updateRotation = true;

        enemy.navMeshAgent.Stop();

        seachLimit = 10f;
    }

    private void RotateTowards()
    {
        Vector3 direction = (enemy.chaseTarget.transform.position - enemy.transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));

        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 10f);
    }
}
