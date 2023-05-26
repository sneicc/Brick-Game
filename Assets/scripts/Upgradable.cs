using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgradable : MonoBehaviour
{
    /// <summary>
    /// Массив улучшений характеристик модификатора.
    /// </summary>
    protected float[] UpgradeBonus;
    /// <summary>
    /// Массив цен для улучшения модификатора.
    /// </summary>
    protected int[] UpgradePrice;
    /// <summary>
    /// Текущий индекс улучшения модификатора.
    /// </summary>
    protected int UpgradeIndex;

    public int NextUpgradePrice
    {
        get
        {
            if (UpgradeIndex + 1 >= UpgradePrice.Length) return -1;
            return UpgradePrice[UpgradeIndex + 1];
        }
    }

    public int CurrentUpgradePrice
    {
        get
        {
            return UpgradePrice[UpgradeIndex];
        }
    }

    public float NextUpgradeBonus
    {
        get
        {
            if (UpgradeIndex + 1 >= UpgradeBonus.Length) return -1;
            return UpgradeBonus[UpgradeIndex + 1];
        }
    }

    public float CurrentUpgradeBonus
    {
        get
        {
            return UpgradeBonus[UpgradeIndex];
        }
    }

    public int CurrentUpgradeIndex
    {
        get
        {
            return UpgradeIndex;
        }
    }

    public int[] UpgradePrices
    {
        get
        {
            int[] temp = new int[UpgradePrice.Length];
            UpgradePrice.CopyTo(temp, 0);
            return temp;
        }
    }

    public float[] UpgradeBonuses
    {
        get
        {
            float[] temp = new float[UpgradeBonus.Length];
            UpgradeBonus.CopyTo(temp, 0);
            return temp;
        }
    }

    public abstract void Upgrade();

}
