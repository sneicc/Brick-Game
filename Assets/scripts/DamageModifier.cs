using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DamageModifier : Modifier, IModifier  // Закрепить все на гейм менеджере или создать отедльный менеджер модов, получать необходимую кнопку из загруженной сцены по тегу или имени
{  
    public static DamageModifier Instance;

    public int DamageWorkingTime = 5;
    public int DamageModifierAmount = 10;
    public int DamageModifierPrice = 10;

    public float[] DamageModifierUpgrade = { 2, 2.5f, 3, 3.5f, 4 };
    public int[] DamageModifierUpgradePrice = { 100, 350, 700, 1200, 1900 };
    public int DamageModifierIndex = 0;

    private static List<BallB> BallsCopy; 

    void Awake()
    {
        if(Instance is not null) Destroy(gameObject);

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

    public void Subscribe(Button button)
    {
        _button = button;
        _button.onClick.AddListener(Activate);
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
