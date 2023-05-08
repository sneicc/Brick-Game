using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLooseHandler : MonoBehaviour
{
    public float LooseCoefficient = 0.7f;

    public GameObject Canvas;
    public Button Exit;
    public Button Retry;
    public Button AD;

    private bool _isADWatched;
    private bool _isLevelEnd;

    private void Awake()
    {
        GameManager.GameLoose += OnGameLoose;

        Exit.onClick.AddListener(OnExit);
        Retry.onClick.AddListener(OnRetry);
        AD.onClick.AddListener(OnAD);

        Canvas.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameLoose()
    {
        if (!_isLevelEnd) Canvas.SetActive(true);
    }

    public void OnExit()
    {
        _isLevelEnd = true;
        LevelManager.Instance.Exit(LooseCoefficient);
    }

    public void OnRetry()
    {
        _isLevelEnd = true;
        LevelManager.Instance.Retry(LooseCoefficient);
    }

    public void OnAD()
    {
        if (!_isADWatched)
        {
            _isADWatched = true;

            var colors = AD.colors;
            colors.normalColor = colors.selectedColor = colors.highlightedColor = colors.pressedColor;
            AD.colors = colors;

            //throw new NotImplementedException(); (реклама)

            GameManager.AddLive();
            Canvas.SetActive(false);
            GameManager.ResumeGame();
        }        
    }

    private void OnDestroy()
    {
        GameManager.GameLoose -= OnGameLoose;

        Exit.onClick.RemoveAllListeners();
        Retry.onClick.RemoveAllListeners();
        AD.onClick.RemoveAllListeners();
    }
}
