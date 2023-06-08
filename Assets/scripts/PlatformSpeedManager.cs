using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpeedManager : Upgradable
{
    public static PlatformSpeedManager Instance;

    [SerializeField]
    private float[] PlatformSpeedUpgrade = { 5, 6, 7, 8, 9, 10};
    [SerializeField]
    private int[] PlatformSpeedUpgradePrice = { 0, 50, 100, 150, 200, 250};
    [SerializeField]
    private int PlatformSpeedUpgradeIndex = 0;
    public int Speed { get; private set; }
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        UpgradeBonus = PlatformSpeedUpgrade;
        UpgradePrice = PlatformSpeedUpgradePrice;
        UpgradeIndex = PlatformSpeedUpgradeIndex;

        SetDamage(UpgradeIndex);
    }

    private void SetDamage(int upgradeIndex)
    {
        Speed = (int)UpgradeBonus[upgradeIndex];
    }

    public override void Upgrade(IResourceRemovalStrategy removalStrategy)
    {
        int nextIndex = UpgradeIndex + 1;
        if (nextIndex < UpgradeBonus.Length)
        {
            int currentPrice = UpgradePrice[nextIndex];
            if (removalStrategy.RemoveResources(currentPrice))
            {
                UpgradeIndex++;
                SetDamage(UpgradeIndex);
            }
        }

    }
}
