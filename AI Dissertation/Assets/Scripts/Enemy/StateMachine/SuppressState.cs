using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppressState : IEnemyState
{
    private readonly StatePatternEnemy enemy;

    private float suppressTimer;
    private float switchTime = 5f;

    Player playerBools;

    public SuppressState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;

        playerBools = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void UpdateState()
    {
        enemy.meshRendererFlag.material.color = Color.cyan;

        suppressTimer += Time.deltaTime;

        Suppressing();

        RotateTowards();
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

        suppressTimer = 0f;

        enemy.currentState = enemy.engageState;
    }

    public void ToExamineState()
    {
        Debug.Log("Examine");

        suppressTimer = 0f;

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
        Debug.Log("Can't transition to same state SuppressState");
    }

    public void Suppressing()
    {
        enemy.inSuppress = true;

        playerBools.inSuppression = true;

        if (suppressTimer >= switchTime)
        {
            enemy.inSuppress = false;

            playerBools.inSuppression = false;

            ToEngageState();
        }
    }

    private void RotateTowards()
    {
        Vector3 direction = (enemy.chaseTarget.transform.position - enemy.transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));

        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 10f);
    }
}