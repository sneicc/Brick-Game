using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLooseHandler : MonoBehaviour
{
    public GameObject Canvas;

    public Button Exit;
    public Button Retry;
    public Button AD;

    public TextMeshProUGUI CollectedCoins;
    public TextMeshProUGUI TotalCoins;
    public TextMeshProUGUI LooseCoefficientText;

    private bool _isADWatched;
    private bool _isLevelEnd;
    private bool _needUpdateText;

    private float _looseCoefficient;

    private void Awake()
    {
        GameManager.GameLoose += OnGameLoose;

        Exit.onClick.AddListener(OnExit);
        Retry.onClick.AddListener(OnRetry);
        AD.onClick.AddListener(OnAD);

        _looseCoefficient = LevelManager.Instance.LooseCoefficient;

        Canvas.SetActive(false);
    }
    void Start()
    {
        
    }


    void Update()
    {
        if (_needUpdateText)
        {
            int coins = LevelManager.Instance.LevelCoins;
            CollectedCoins.text = '+' + coins.ToString();
            LooseCoefficientText.text = $"x {_looseCoefficient} =";

            int collectedCoins = (int)Math.Ceiling(coins * _looseCoefficient);
            StartCoroutine(TextAnimation(collectedCoins));

            _needUpdateText = false;
        }      
    }

    public void OnGameLoose()
    {
        if (!_isLevelEnd)
        {
            Canvas.SetActive(true);
            _needUpdateText = true;
        }
    }

    public void OnExit()
    {
        GameManager.RemoveAllListeners();
        _isLevelEnd = true;
        GameManager.LoadMainMenu();
    }

    public void OnRetry()
    {
        GameManager.RemoveAllListeners();
        _isLevelEnd = true;
        LevelManager.Instance.Retry();
    }

    public void OnAD()
    {
        if (!_isADWatched)
        {
            _isADWatched = true;

            var colors = AD.colors;
            colors.normalColor = colors.selectedColor = colors.highlightedColor = colors.pressedColor;
            AD.colors = colors;

            //throw new NotImplementedException(); (�������)

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

    IEnumerator TextAnimation(int value)
    {
        int currentValue = 0;
        float incrementPercentage = 0.1f;

        while (currentValue != value)
        {
            float remainingDistance = value - currentValue;
            if (remainingDistance < 10)
            {
                incrementPercentage = 0.01f;
            }
            currentValue = Mathf.CeilToInt(Mathf.Lerp(currentValue, value, incrementPercentage));
            TotalCoins.text = currentValue.ToString();
            yield return new WaitForSecondsRealtime(0.08f);
        }
    }
}
