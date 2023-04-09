using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAndImmortalModifier : Modifier, IModifier
{
    public SpeedAndImmortalModifier Instance;

    public static int SpeedWorkingTime = 5;
    public static int SpeedAmount = 10;
    public static int SpeedPrice = 10;

    public static float[] SpeedUpgrade = { 1.5f, 2f, 2.5f, 3f, 3.5f };
    public static int[] SpeedUpgradePrice = { 100, 350, 700, 1200, 1900 };
    public static int SpeedUpgradeIndex = 0;

    private static List<BallB> BallsCopy;

    void Awake()
    {
        WorkingTime = SpeedWorkingTime;
        Amount = SpeedAmount;
        Price = SpeedPrice;

        UpgradeBonus = SpeedUpgrade;
        UpgradePrice = SpeedUpgradePrice;
        UpgradeIndex = SpeedUpgradeIndex;

        Instance = this;
    }
    
    public void Activate()
    {
        if (Spend())
        {
            BallsCopy = new List<BallB>(GameManager.Balls);

            foreach (var ball in GameManager.Balls)
            {
                ball.IsImmortal = true;
                ball.BounceSpeed +=  UpgradeBonus[UpgradeIndex];
            }
            Invoke(nameof(Disable), WorkingTime);
        }
    }

    public void Disable()
    {
        foreach (var ball in BallsCopy)
        {
            if (!ReferenceEquals(ball, null))
            {
                ball.IsImmortal = false;
                ball.BounceSpeed -= UpgradeBonus[UpgradeIndex];
            }
        }
    }
}
