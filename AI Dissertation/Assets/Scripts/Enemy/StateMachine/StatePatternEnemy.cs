using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePatternEnemy : MonoBehaviour {

    public float searchingTurnSpeed = 120f;
    public float seachingDuration = 4f;
    public float sightRange = 100f;
    public float hearRange = 50f;

    public Transform[] wayPoints;
    public Transform[] wayPointsCover;

    [HideInInspector] public Transform wayPointCover;

    public Transform[] arrayOfCovers;

    public Transform eyes;

    public Vector3 offset = new Vector3(0, 0.5f, 0);

    public MeshRenderer meshRendererFlag;

    public bool inCover;
    public bool inFlanking;
    public bool inFlank;
    public bool inSuppress;

    public EnemyHealth enemyHealth;

    [HideInInspector] public Transform chaseTarget;

    [HideInInspector] public IEnemyState currentState;

    [HideInInspector] public AlertState alertState;
    [HideInInspector] public CoverState coverState;
    [HideInInspector] public CheckState checkState;
    [HideInInspector] public EngageState engageState;
    [HideInInspector] public ExamineState examineState;
    [HideInInspector] public FlankState flankState;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public SuppressState suppressState;

    [HideInInspector] public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        alertState = new AlertState(this);
        coverState = new CoverState(this);
        checkState = new CheckState(this);
        engageState = new EngageState(this);
        examineState = new ExamineState(this);
        flankState = new FlankState(this);
        patrolState = new PatrolState(this);
        suppressState = new SuppressState(this);

        navMeshAgent = GetComponent<NavMeshAgent>();

        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Use this for initialization
    void Start () {

        currentState = patrolState;
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
