using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageModifier : Modifier, IModifier
{  
    public DamageModifier Instance;

    private const int DamageWorkingTime = 5;
    private static int DamageMltiplierAmount = 10;
    private static int DamageMltiplierPrice = 10;

    private static float[] DamageMultiplier = { 2, 2.5f, 3, 3.5f, 4 };
    private static int[] DamageMultiplierUpgradePrice = { 100, 350, 700, 1200, 1900 };
    private static int DamageMultiplierIndex = 0;


    private DamageModifier() : base(DamageWorkingTime, DamageMltiplierAmount, DamageMltiplierPrice)
    {

    }


    // Start is called before the first frame update
    void Awake()
    {
        Instance = new DamageModifier();
    }

    public void Upgrade() //перенести в базовый класс
    {
        if (DamageMultiplier.Length <= DamageMultiplierIndex)
        {
            int currentPrice = DamageMultiplierUpgradePrice[DamageMultiplierIndex];
            if (currentPrice <= GameManager.Coins)
            {
                DamageMultiplierIndex++;
                GameManager.RemoveCoins(currentPrice);
            }
        }
    }

    public void Activate()
    {
        if (DamageMltiplierAmount >= 1)
        {
            DamageMltiplierAmount--;

            int tempDamage = (int)(GameManager.Damage * DamageMultiplier[DamageMultiplierIndex]);
            foreach (var ball in GameManager.Balls)
            {
                ball.Damage = tempDamage;
            }
            Invoke(nameof(Disable), WorkingTime);
        }
    }

    public void Disable()
    {
        foreach (var ball in GameManager.Balls)
        {
            ball.Damage = GameManager.Damage;
        }
    }
}
