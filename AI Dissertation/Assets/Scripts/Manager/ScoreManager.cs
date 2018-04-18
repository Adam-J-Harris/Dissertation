using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int Enemies;

    private List<GameObject> AmountOfEnemies;

    bool firstRun = false;

    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        Enemies = 0;
    }

	// Use this for initialization
	void Start () {
        AmountOfEnemies = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!firstRun)
        {
            foreach (GameObject a in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                AmountOfEnemies.Add(a);
            }

            firstRun = true;
        }

        text.text = "Enemies Defeated: " + Enemies + " / " + AmountOfEnemies.Count;

        if (Enemies == AmountOfEnemies.Count)
        {
            SceneManager.LoadScene("Completed");
        }
	}
}
