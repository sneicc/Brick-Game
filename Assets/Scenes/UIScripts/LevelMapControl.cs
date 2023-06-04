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
    public Button[] Buttons;

    private void Awake()
    {
        GameManager.ResumeGame();
    }
    void Start()
    {        
        ButtonInitialize();
        CloseLevels();
    }

    private void ButtonInitialize()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Button button = Buttons[i];

            int level = i;
            button.onClick.AddListener(() => GameManager.NewGame(level));
        }
    }

    private void CloseLevels()
    {
        int CurrentOpenedLevel = GameManager.CurrentOpenedLevel;
        for (int i = 0; i < Buttons.Length; i++)
        {
            if(i > CurrentOpenedLevel)
            {
                Buttons[i].interactable = false;
            }
        }
    }
}
