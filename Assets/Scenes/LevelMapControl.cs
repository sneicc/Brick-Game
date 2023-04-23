using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMapControl : MonoBehaviour
{
    public Button[] buttons;


    void Start()
    {

        SceneManager.LoadScene("TOPBAR", LoadSceneMode.Additive);
        //ButtonInitialize();
    }

    //private void ButtonInitialize()
    //{
    //    for (int i = 0; i < buttons.Length; i++)
    //    {
    //        Button button = buttons[i];

    //        int level = i;
    //        button.onClick.AddListener(() => GameManager.NewGame(level));
    //    }
    //}
}
