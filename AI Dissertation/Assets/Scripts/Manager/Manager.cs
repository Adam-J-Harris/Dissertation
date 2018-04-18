using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {


    void Awake()
    {
        Cursor.visible = true;
    }

    public void ChangeToScene(string value)
    {
        SceneManager.LoadScene(value);
    }

    public void Exit()
    {
        Application.Quit();
    }

    void Update()
    {
        Cursor.visible = true;
    }
}
