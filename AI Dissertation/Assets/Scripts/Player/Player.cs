using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool inCover;

    public bool inSuppression;

    // Use this for initialization
    void Start ()
    {
        inSuppression = false;
        inCover = false;
	}
}
