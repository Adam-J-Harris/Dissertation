using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour {

    public float fillAmount;

    float inMin;
    float inMax;
    float outMin;
    float outMax;

    float cal;

    [SerializeField] private Image health;

	// Use this for initialization
	void Start () {

        inMin = 0f;
        inMax = 100f;
        outMin = 0f;
        outMax = 1f;	

	}
	
	// Update is called once per frame
	void Update () {

        HandleBar();
	}

    private void HandleBar()
    {
        health.fillAmount = fillAmount;
    }

    public float Map(float val)
    {
        cal = (val - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        return cal;
    }
}
