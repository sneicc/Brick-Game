using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class GameWinHandler : MonoBehaviour
{
    public GameObject Canvas;

    public Button Exit;
    public Button NextLevel;
    public Button MultiplyReward;

    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI DaimondText;

    public Image BigStarBright;
    public Image SmallStarLeftBright;
    public Image SmallStarRightBright;

    public Animator LeftStarAnimator;
    public Animator BigStarAnimator;
    public Animator RightStarAnimator;

    private bool _isADWatched;
    private bool _needUpdateText;

    private int _prevCoinsValue;
    private int _prevDaimondsValue;

    private void Awake()
    {
        _prevCoinsValue = 0;
        _prevDaimondsValue = 0;

        GameManager.GameWin += OnGameWin;

        Exit.onClick.AddListener(OnExit);
        NextLevel.onClick.AddListener(OnNextLevel);
        MultiplyReward.onClick.AddListener(OnMultiplyReward);

        CoinText.text = "0";
        DaimondText.text = "0";

        Canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_needUpdateText) return;

        int coins = LevelManager.Instance.LevelCoins;
        int daimonds = LevelManager.Instance.LevelDaimonds;
        StartCoroutine(TextAnimation(coins, CoinText, _prevCoinsValue));
        StartCoroutine(TextAnimation(daimonds, DaimondText, _prevDaimondsValue));

        _prevCoinsValue = coins;
        _prevDaimondsValue = daimonds;
            
        _needUpdateText = false;

    }

    private void OnGameWin(int stars)
    {
        Canvas.SetActive(true);
        _needUpdateText = true;

        if (stars >= 1) LeftStarAnimator.SetTrigger("Activate");
        if (stars >= 2)
        {
            StartCoroutine(WaitBeforeStartAnimation(1f));
            RightStarAnimator.SetTrigger("Activate");
        }
        if(stars >= 3)
        {
            StartCoroutine(WaitBeforeStartAnimation(2f));
            BigStarAnimator.SetTrigger("Activate");
        }

        IEnumerator WaitBeforeStartAnimation(float time)
        {
            yield return new WaitForSecondsRealtime(time);
        }
            
    }

    private void OnExit()
    {
        GameManager.LoadMainMenu();
    }

    private void OnNextLevel()
    {
        LevelManager.Instance.NextLevel();
    }

    private void OnMultiplyReward()
    {
        if (!_isADWatched)
        {
            _isADWatched = true;

            var colors = MultiplyReward.colors;
            colors.normalColor = colors.selectedColor = colors.highlightedColor = colors.pressedColor;
            MultiplyReward.colors = colors;

            //throw new NotImplementedException(); (реклама)

            LevelManager.Instance.MultiplyRewardX2();
            _needUpdateText = true;
        }
    }

    private void OnDestroy()
    {
        GameManager.GameWin -= OnGameWin;

        Exit.onClick.RemoveAllListeners();
        NextLevel.onClick.RemoveAllListeners();
        MultiplyReward.onClick.RemoveAllListeners();
    }

    IEnumerator TextAnimation(int value, TextMeshProUGUI text, int currentValue)
    {       
        float incrementPercentage = 0.1f;

        while (currentValue != value)
        {
            float remainingDistance = value - currentValue;
            if (remainingDistance < 10)
            {
                incrementPercentage = 0.01f;
            }
            currentValue = Mathf.CeilToInt(Mathf.Lerp(currentValue, value, incrementPercentage));
            text.text = '+' + currentValue.ToString();
            yield return new WaitForSecondsRealtime(0.08f);
        }
    }
}
