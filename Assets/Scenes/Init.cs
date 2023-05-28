using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{

    void Start()
    {
        Physics2D.queriesHitTriggers = true;
        Application.targetFrameRate = 120;

        SceneManager.LoadScene("LevelMap", LoadSceneMode.Single);
        SceneManager.LoadScene("TOPBAR", LoadSceneMode.Additive);
    }

}
