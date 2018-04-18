using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckState : IEnemyState
{
    private readonly StatePatternEnemy enemy;

    private Vector3 lastKnowPosition = new Vector3(10,10,10);

    private bool switchFun = false;
    private bool switchSet = false;

    private float searchTimer;

    public CheckState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        enemy.meshRendererFlag.material.color = Color.white;

        Look();

        if (!switchFun)
        {
            LastPosition();
        }
        else
        {
            Search();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Reset();
        }

        if (enemy.enemyHealth.Hit == true)
        {
            enemy.enemyHealth.Hit = false;
            Reset();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Can't use this");

        if (other.gameObject.CompareTag("Player"))
        {
            ToExamineState();
        }
    }

    public void ToAlertState()
    {
        Debug.Log("Alert");

        searchTimer = 0f;

        enemy.currentState = enemy.alertState;
    }

    public void ToCheckState()
    {
        Debug.Log("Can't transition to same state CheckState");
    }

    public void ToCoverState()
    {
        Debug.Log("Cover");
    }

    public void ToEngageState()
    {
        Debug.Log("Engage");
    }

    public void ToExamineState()
    {
        Debug.Log("Examine");

        searchTimer = 0f;

        switchSet = false;
        switchFun = false;

        enemy.currentState = enemy.examineState;
    }

    public void ToFlankState()
    {
        Debug.Log("Flank");
    }

    public void ToPatrolState()
    {
        Debug.Log("Patrol");

        searchTimer = 0f;

        switchFun = false;
        switchSet = false;

        enemy.currentState = enemy.patrolState;
    }

    public void ToSuppressState()
    {
        Debug.Log("Suppress");
    }

    private void Reset()
    {
        float Dist = Vector3.Distance(enemy.transform.position, GameObject.Find("Player").transform.position);

        if (Dist < (enemy.hearRange + enemy.sightRange))
        {
            switchFun = false;
            switchSet = false;
            searchTimer = 0f;
        }
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
                switchSet = false;
                switchFun = false;

                ToAlertState();
            }
        }
    }

    private void LastPosition()
    {
        if (!switchSet)
        {
            lastKnowPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            enemy.navMeshAgent.updateRotation = true;
            switchSet = true;
        }
        else
        {
            enemy.navMeshAgent.destination = lastKnowPosition;
            enemy.navMeshAgent.Resume();

            if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
            {
                enemy.navMeshAgent.Stop();
                switchFun = true;
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
            switchFun = false;
            switchSet = false;

            ToPatrolState();
        }
    }
}
