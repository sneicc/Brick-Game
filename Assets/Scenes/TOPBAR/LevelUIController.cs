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

    private void OnDestroy()
    {
        DamageButton.onClick?.RemoveAllListeners();
        SpeedButton.onClick?.RemoveAllListeners();
        DoublingButton.onClick?.RemoveAllListeners();
        ExplosionButton.onClick?.RemoveAllListeners();
    }
}
