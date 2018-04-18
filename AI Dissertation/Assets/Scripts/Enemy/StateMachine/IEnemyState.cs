using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState {

    void UpdateState();

    void OnTriggerEnter(Collider other);

    void ToAlertState();

    void ToCoverState();

    void ToCheckState();

    void ToEngageState();

    void ToExamineState();

    void ToFlankState();

    void ToPatrolState();

    void ToSuppressState();
}
