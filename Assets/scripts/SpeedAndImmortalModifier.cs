using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpeedAndImmortalModifier : Modifier, IModifier // запретить ускорения до запуска шара
{
    public static SpeedAndImmortalModifier Instance;

    [SerializeField]
    private int SpeedWorkingTime = 5;
    [SerializeField]
    private int SpeedAmount = 10;
    [SerializeField]
    private int SpeedPrice = 10;

    [SerializeField]
    private float[] SpeedUpgrade = { 1, 1.5f, 2f, 2.5f, 3f, 3.5f };
    [SerializeField]
    private int[] SpeedUpgradePrice = { 0, 100, 350, 700, 1200, 1900 };
    [SerializeField]
    private int SpeedUpgradeIndex = 0;

    private List<Ball> _ballsCopy;

    void Awake()
    {
        if (Instance is not null) Destroy(gameObject);

        WorkingTime = SpeedWorkingTime;
        Amount = SpeedAmount;
        Price = SpeedPrice;

        UpgradeBonus = SpeedUpgrade;
        UpgradePrice = SpeedUpgradePrice;
        UpgradeIndex = SpeedUpgradeIndex;

        Instance = this;
    }
    
    public override void Activate()
    {
        if (Spend())
        {
            _ballsCopy = new List<Ball>(GameManager.Balls);

            foreach (var ball in GameManager.Balls)
            {
                if (!ball.gameObject.active) continue;

                ball.SpeedModCounter++;
                ball.IsImmortal = true;
                ball.BounceSpeed +=  UpgradeBonus[UpgradeIndex];
            }
            Invoke(nameof(Disable), WorkingTime);
        }
    }

    public void Disable()
    {
        foreach (var ball in _ballsCopy)
        {
            if (!ReferenceEquals(ball, null))
            {
                if (ball.SpeedModCounter == 0) continue;
                if(ball.SpeedModCounter == 1) ball.IsImmortal = false;
                ball.SpeedModCounter--;
                ball.BounceSpeed -= UpgradeBonus[UpgradeIndex];
            }
        }
    }

}
