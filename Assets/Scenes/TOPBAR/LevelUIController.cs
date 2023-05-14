using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour
{
    public Button DamageButton;
    public Button SpeedButton;
    public Button DoublingButton;
    public Button ExplosionButton;

    public Image[] Hearts;
    public Color SpendHearth;
    public Color BonusHearth;

    private Color _mainColor;

    public TextMeshProUGUI COINS_COUNT;
    public TextMeshProUGUI DIAMOND_COUNT;

    private void Awake()
    {
        _mainColor = Hearts[0].color;
        GameManager.LivesChanged += OnLivesChanged;

        DamageModifier.Instance.Subscribe(DamageButton);
        SpeedAndImmortalModifier.Instance.Subscribe(SpeedButton);
        DoublingAllBalls.Instance.Subscribe(DoublingButton);
        Explosion.Instance.Subscribe(ExplosionButton);

        LevelManager.Instance.CoinsChanged += OnCoinsChanged;
        LevelManager.Instance.DaimondsChanged += OnDiamondChanged;

        COINS_COUNT.text = LevelManager.Instance.LevelCoins.ToString();
        DIAMOND_COUNT.text = LevelManager.Instance.LevelDaimonds.ToString();
    }
    protected void OnCoinsChanged()
    {
        COINS_COUNT.text = LevelManager.Instance.LevelCoins.ToString();
    }

    protected void OnDiamondChanged()
    {
        DIAMOND_COUNT.text = LevelManager.Instance.LevelDaimonds.ToString();
    }

    private void OnLivesChanged()
    {
        int lives = GameManager.Lives - 1;

        for (int i = 0; i < Hearts.Length; i++)
        {
            Hearts[i].color = _mainColor;
        }

        if (lives < 3)
        {
            for (int i = 2; i > lives; i--)
            {
                Hearts[i].color = SpendHearth;
            }
        }
        else
        {
            lives -= 2;
            for (int i = 0; i < lives; i++)
            {
                Hearts[i].color = BonusHearth;
            }
        }
     
    }

    private void OnDestroy()
    {
        DamageModifier.Instance.Unsubscribe();
        SpeedAndImmortalModifier.Instance.Unsubscribe();
        DoublingAllBalls.Instance.Unsubscribe();
        Explosion.Instance.Unsubscribe();

        LevelManager.Instance.CoinsChanged -= OnCoinsChanged;
        LevelManager.Instance.DaimondsChanged -= OnDiamondChanged;

        GameManager.LivesChanged -= OnLivesChanged;
    }    
}
