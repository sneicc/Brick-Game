using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDamageManager : Upgradable
{
    public static BallDamageManager Instance;

    [SerializeField]
    private float[] DamageUpgrade = { 5, 11, 24, 50, 100, 250 };
    [SerializeField]
    private int[] DamageUpgradePrice = { 0, 100, 350, 700, 1200, 1900 };
    [SerializeField]
    private int DamageUpgradeIndex = 0;
    public int Damage { get; private set; }
    private void Awake()
    {
        if (Instance is not null) Destroy(gameObject);
        Instance = this;

        UpgradeBonus = DamageUpgrade;
        UpgradePrice = DamageUpgradePrice;
        UpgradeIndex = DamageUpgradeIndex;

        SetDamage(UpgradeIndex);
    }

    private void SetDamage(int upgradeIndex)
    {
        Damage = (int)UpgradeBonus[upgradeIndex];
    }

    public override void Upgrade()
    {
        int nextIndex = UpgradeIndex + 1;
        if (nextIndex < UpgradeBonus.Length)
        {
            int currentPrice = UpgradePrice[nextIndex];
            if (GameManager.RemoveCoins(currentPrice)) 
            {
                UpgradeIndex++;
                SetDamage(UpgradeIndex);
            }
        }

    }
}
