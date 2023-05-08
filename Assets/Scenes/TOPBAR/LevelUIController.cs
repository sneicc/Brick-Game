using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIController : GameUIController
{
    public Button DamageButton;
    public Button SpeedButton;
    public Button DoublingButton;
    public Button ExplosionButton;

    private void Awake()
    {
        DamageModifier.Instance.Subscribe(DamageButton);
        SpeedAndImmortalModifier.Instance.Subscribe(SpeedButton);
        DoublingAllBalls.Instance.Subscribe(DoublingButton);
        Explosion.Instance.Subscribe(ExplosionButton);
    }

    private void Start()
    {
        AnimatorBTN = MainButton.GetComponent<Animator>();

        if (ButtonSprite == true)
        {
            MainButton.image.sprite = MainButtonSpriteMENU;
        }
        else
        {
            MainButton.image.sprite = MainButtonSpriteCROSS;
        }

        LevelManager.Instance.CoinsChanged += OnCoinsChanged;
        LevelManager.Instance.DaimondsChanged += OnDiamondChanged;

        COINS_COUNT.text = LevelManager.Instance.LevelCoins.ToString();
        DIAMOND_COUNT.text = LevelManager.Instance.LevelDaimonds.ToString();
    }

    protected override void OnCoinsChanged()
    {
        COINS_COUNT.text = LevelManager.Instance.LevelCoins.ToString();
    }

    protected override void OnDiamondChanged()
    {
        DIAMOND_COUNT.text = LevelManager.Instance.LevelDaimonds.ToString();
    }

    private void OnDestroy()
    {
        DamageButton.onClick?.RemoveAllListeners();
        SpeedButton.onClick?.RemoveAllListeners();
        DoublingButton.onClick?.RemoveAllListeners();
        ExplosionButton.onClick?.RemoveAllListeners();

        LevelManager.Instance.CoinsChanged -= OnCoinsChanged;
        LevelManager.Instance.DaimondsChanged -= OnDiamondChanged;
    }
}
