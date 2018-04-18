using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePatternEnemyDumb : MonoBehaviour {

    public float searchingTurnSpeed = 120f;
    public float seachingDuration = 4f;
    public float sightRange = 20f;

    public Transform[] wayPoints;

    public Transform eyes;

    public Vector3 offset = new Vector3(0, 0.5f, 0);

    public MeshRenderer meshRendererFlag;

    [HideInInspector] public Transform chaseTarget;

    [HideInInspector] public IEnemyStateDumb currentState;

    [HideInInspector] public AlertStateDumb alertStateDumb;
    [HideInInspector] public EngageStateDumb engageStateDumb;
    [HideInInspector] public ExamineStateDumb examineStateDumb;
    [HideInInspector] public PatrolStateDumb patrolStateDumb;

    [HideInInspector] public NavMeshAgent navMeshAgent;

    void Awake()
    {
        alertStateDumb = new AlertStateDumb(this);
        engageStateDumb = new EngageStateDumb(this);
        examineStateDumb = new ExamineStateDumb(this);
        patrolStateDumb = new PatrolStateDumb(this);

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Use this for initialization
    void Start () {
        currentState = patrolStateDumb;
	}
	
	// Update is called once per frame
	void Update () {
        currentState.UpdateState();
	}

    private void OnTriggerEnter(Collider collider)
    {
        currentState.OnTriggerEnter(collider);
    }
}
