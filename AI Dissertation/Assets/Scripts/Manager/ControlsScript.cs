using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsScript : MonoBehaviour {

    public Text WSAD;
    public Text FIRE;
    public Text Movement;
    public Text Goals;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void ControlsEnbaled()
    {
        Goals.enabled = false;

        WSAD.enabled = true;
        FIRE.enabled = true;
        Movement.enabled = true;       
    }

    public void GoalsEnbaled()
    {
        WSAD.enabled = false;
        FIRE.enabled = false;
        Movement.enabled = false;

        Goals.enabled = true;
    }
}
