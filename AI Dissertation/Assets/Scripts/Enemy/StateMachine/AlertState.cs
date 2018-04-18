using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState
{
    private readonly StatePatternEnemy enemy;

    // private bool lastPosition = false;

    public AlertState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        enemy.meshRendererFlag.material.color = Color.red;

        Look();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Can't use this");
    }

    public void ToAlertState()
    {
        Debug.Log("Can't transition to same state AlertState");
    }

    public void ToCheckState()
    {
        Debug.Log("Check");
    }

    public void ToCoverState()
    {
        Debug.Log("Cover");

        enemy.currentState = enemy.coverState;
    }

    public void ToEngageState()
    {
        Debug.Log("Engage");

        enemy.currentState = enemy.engageState;
    }

    public void ToExamineState()
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

                if (enemy.inCover == false)
                {
                    ToCoverState();
                }
                else if (enemy.inCover == true)
                {
                    ToEngageState();
                }               
            }
            else
            {
                ToExamineState();
            }
        }
        else
        {
            ToExamineState();
        }
    }
}
