using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageModifier : Modifier, IModifier
{
    private const int DamageWorkingTime = 5;
    public DamageModifier Instance;

    private static float[] DamageMltiplier = { 2, 2.5f, 3, 3.5f, 4 };
    private static int[] DamageMltiplierPrice = { 100, 350, 700, 1200, 1900 };
    private static int DamageMltiplierIndex = 0;
    private static int DamageMltiplierAmount = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        this.WorkingTime = DamageWorkingTime;
    }

    public void Upgrade()
    {
        if (DamageMltiplier.Length <= DamageMltiplierIndex)
        {
            int currentPrice = DamageMltiplierPrice[DamageMltiplierIndex];
            if (currentPrice <= GameManager.Coins)
            {
                DamageMltiplierIndex++;
                GameManager.RemoveCoins(currentPrice);
            }
        }
    }

    public void Activate()
    {
        if (DamageMltiplierAmount >= 1)
        {
            DamageMltiplierAmount--;

            int tempDamage = (int)(GameManager.Damage * DamageMltiplier[DamageMltiplierIndex]);
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
