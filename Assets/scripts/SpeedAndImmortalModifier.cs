using System.Collections.Generic;
using UnityEngine;

public class SpeedAndImmortalModifier : Modifier, IModifier, ISaveable // запретить ускорения до запуска шара
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

    private List<Ball2D> _ballsCopy;

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);

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
            _ballsCopy = new List<Ball2D>(GameManager.Balls);

            foreach (var ball in GameManager.Balls)
            {
                if (!ball.gameObject.active) continue;

                SpeedBooster.AddSpeed(ball, UpgradeBonus[UpgradeIndex]);
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
                SpeedBooster.RemoveSpeed(ball, UpgradeBonus[UpgradeIndex]);
            }
        }
    }

    public void Save(SaveData saveData)
    {
        saveData.SpeedModIndex = UpgradeIndex;
    }

    public void Load(SaveData saveData)
    {
        UpgradeIndex = saveData.SpeedModIndex;
    }
}
