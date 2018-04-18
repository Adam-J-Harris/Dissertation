using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : MonoBehaviour {

    public static int ammoCount;
    public static Color ammoColor;
    public static Color defualtAmmoColor;

    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        ammoColor = text.color;
        defualtAmmoColor = text.color;
        ammoCount = 30;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        text.text = "" + ammoCount;
        text.color = ammoColor;
    }
}
