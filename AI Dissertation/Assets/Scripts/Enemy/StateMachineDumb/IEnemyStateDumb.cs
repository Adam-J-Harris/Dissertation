using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyStateDumb {

    void UpdateState();

    void OnTriggerEnter(Collider other);

    void ToAlertStateDumb();

    void ToExamineStateDumb();

    void ToPatrolStateDumb();

    void ToEngageStateDumb();
}
