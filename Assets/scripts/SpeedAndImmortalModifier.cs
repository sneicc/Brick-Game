using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAndImmortalModifier : Modifier, IModifier
{
    public SpeedAndImmortalModifier Instance;

    private const int SpeedWorkingTime = 5;
    private static int SpeedAmount = 10;
    private static int SpeedPrice = 10;

    private static float[] SpeedMultiplier = { 1.5f, 2f, 2.5f, 3f, 3.5f };
    private static int[] SpeedUpgradePrice = { 100, 350, 700, 1200, 1900 };
    private static int SpeedUpgradeIndex = 0;
    


    private SpeedAndImmortalModifier() : base(SpeedWorkingTime, SpeedAmount, SpeedPrice)
    {

    }

    void Awake()
    {
        Instance = new SpeedAndImmortalModifier();
    }
    
    public void Activate()
    {
        if (SpeedAmount >= 1)
        {
            SpeedAmount--;

            foreach (var ball in GameManager.Balls)
            {
                ball.Immortal = true;
                float currentSpeed = ball.BounceSpeed * SpeedMultiplier[SpeedUpgradeIndex];
                ball.BounceSpeed = currentSpeed;
                ball.RB.velocity = ball.RB.velocity.normalized * currentSpeed;
            }
            Invoke(nameof(Disable), WorkingTime);
        }
    }

    public void Disable()
    {
        foreach (var ball in GameManager.Balls)
        {
            ball.Immortal = false;
            ball.BounceSpeed = ball.MainSpeed;
            ball.RB.velocity = ball.RB.velocity.normalized * ball.MainSpeed;
        }
    }

    public void Upgrade() //перенести в базовый класс
    {
        if (SpeedMultiplier.Length <= SpeedUpgradeIndex)
        {
            int currentPrice = SpeedUpgradePrice[SpeedUpgradeIndex];
            if (currentPrice <= GameManager.Coins)
            {
                SpeedUpgradeIndex++;
                GameManager.RemoveCoins(currentPrice);
            }
        }
    }
}
