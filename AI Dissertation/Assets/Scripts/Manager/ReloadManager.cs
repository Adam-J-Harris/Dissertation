using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadManager : MonoBehaviour {

    Text text;

    Color defaultColor;

    void Awake()
    {
        text = GetComponent<Text>();
        defaultColor = text.color;
        text.enabled = false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OutOfAmmo()
    {
        text.enabled = true;
    }

    public void Reloaded()
    {
        text.enabled = false;
    }
}
