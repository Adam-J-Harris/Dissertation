using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineState : IEnemyState
{
    private readonly StatePatternEnemy enemy;

    private float searchTimer;

    public ExamineState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        enemy.meshRendererFlag.material.color = Color.yellow;

        Look();

        Search();

        if (Input.GetButtonDown("Fire1"))
        {
            float Dist = Vector3.Distance(enemy.transform.position, GameObject.Find("Player").transform.position);

            if (Dist < (enemy.hearRange))
            {
                ToCheckState();
            }
        }
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

    public void ToCoverState()
    {
        Debug.Log("Cover");
    }

    public void ToCheckState()
    {
        Debug.Log("Check");

        searchTimer = 0f;

        enemy.currentState = enemy.checkState;
    }

    public void ToEngageState()
    {
        Debug.Log("Engage");
    }

    public void ToExamineState()
    {
        Debug.Log("Can't transition to same state ExamineState");
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

    private void Search()
    {
        enemy.navMeshAgent.Stop();
        enemy.transform.Rotate(0, enemy.searchingTurnSpeed * Time.deltaTime, 0);

        searchTimer += Time.deltaTime;

        if (searchTimer >= enemy.seachingDuration)
        {
            ToCheckState();
        }
    }
}
