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

    [SerializeField]
    private int DamageWorkingTime = 5;
    [SerializeField]
    private int DamageModifierAmount = 10;
    [SerializeField]
    private int DamageModifierPrice = 10;

    [SerializeField]
    private float[] DamageModifierUpgrade = { 1.5f, 2, 2.5f, 3, 3.5f, 4 };
    [SerializeField]
    private int[] DamageModifierUpgradePrice = { 0, 100, 350, 700, 1200, 1900 };
    [SerializeField]
    private int DamageModifierIndex = 0;

    private List<Ball2D> _ballsCopy;

    void Awake()
    {
        if(Instance != null) Destroy(gameObject);

        WorkingTime = DamageWorkingTime;
        Amount = DamageModifierAmount;
        Price = DamageModifierPrice;

        UpgradeBonus = DamageModifierUpgrade;
        UpgradePrice = DamageModifierUpgradePrice;
        UpgradeIndex = DamageModifierIndex;

        Instance = this;
    }

    public override void Activate()
    {
        if (Spend()) 
        {
            _ballsCopy = new List<Ball2D>(GameManager.Balls); 

            int tempDamage = (int)(BallDamageManager.Instance.Damage * UpgradeBonus[UpgradeIndex]);
            foreach (var ball in GameManager.Balls)
            {
                ball.Damage = tempDamage;
            }
            Invoke(nameof(Disable), WorkingTime);
        }
    }

    public void Disable()
    {
        foreach (var ball in _ballsCopy)
        {
            if (!ReferenceEquals(ball, null)) ball.Damage = BallDamageManager.Instance.Damage;
        }
    }

    //public void Disable()
    //{
    //    var intersect = GameManager.Balls.Intersect(BallsCopy);
    //    foreach (var ball in intersect)
    //    {
    //        if (!ReferenceEquals(ball, null)) ball.Damage = BallDamageManager.Instance.Damage; // протестировать скорость
    //    }
    //}
}
