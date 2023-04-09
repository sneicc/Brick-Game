using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modifier : MonoBehaviour
{
    internal int WorkingTime;
    internal int Amount;
    internal int Price;

    internal float[] UpgradeBonus;
    internal int[] UpgradePrice;
    internal int UpgradeIndex;


    /// <summary>
    /// Добавляет 1 еденицу модификатора, при условии налачия нужной суммы монет
    /// </summary>
    /// <returns>Булевский результат выполнения операции</returns>
    protected bool Buy()
    {
        if (GameManager.RemoveCoins(Price))
        {
            Amount++;
            return true;
        }
        return false;
    }

    protected bool Spend()
    {
        if(Amount >= 1)
        {
            Amount--;
            return true;
        }
        return false;
    }

    protected virtual void Upgrade()
    {
        if (UpgradeBonus.Length <= UpgradeIndex)
        {
            int currentPrice = UpgradePrice[UpgradeIndex];
            if (GameManager.RemoveCoins(currentPrice)) UpgradeIndex++;
        }
    }


}
