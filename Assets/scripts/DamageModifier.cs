using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DamageModifier : Modifier, IModifier
{  
    public static DamageModifier Instance;

    public static int DamageWorkingTime = 5;
    public static int DamageModifierAmount = 10;
    public static int DamageModifierPrice = 10;

    public static float[] DamageModifierUpgrade = { 2, 2.5f, 3, 3.5f, 4 };
    public static int[] DamageModifierUpgradePrice = { 100, 350, 700, 1200, 1900 };
    public static int DamageModifierIndex = 0;

    private static List<BallB> BallsCopy; 

    void Awake()
    {
        WorkingTime = DamageWorkingTime;
        Amount = DamageModifierAmount;
        Price = DamageModifierPrice;

        UpgradeBonus = DamageModifierUpgrade;
        UpgradePrice = DamageModifierUpgradePrice;
        UpgradeIndex = DamageModifierIndex;

        Instance = this;
    }

    public void Activate()
    {
        if (Spend()) 
        {
            BallsCopy = new List<BallB>(GameManager.Balls); 

            int tempDamage = (int)(GameManager.Damage * UpgradeBonus[UpgradeIndex]);
            foreach (var ball in GameManager.Balls)
            {
                ball.Damage = tempDamage;
            }
            Invoke(nameof(Disable), WorkingTime);
        }
    }

    public void Disable()
    {
        foreach (var ball in BallsCopy)
        {
            if (!ReferenceEquals(ball, null)) ball.Damage = GameManager.Damage;
        }
    }

    //public void Disable()
    //{
    //    var intersect = GameManager.Balls.Intersect(BallsCopy);
    //    foreach (var ball in intersect)
    //    {
    //        if (!ReferenceEquals(ball, null)) ball.Damage = GameManager.Damage; // протестировать скорость
    //    }
    //}
}
