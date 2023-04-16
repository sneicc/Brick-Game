using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Modifier : MonoBehaviour
{
    protected int WorkingTime;
    protected int Amount;
    protected int Price;

    protected float[] UpgradeBonus;
    protected int[] UpgradePrice;
    protected int UpgradeIndex;

    protected Button _Button;
    protected virtual void  Start()
    {
        _Button = gameObject.GetComponent<Button>();
    }

    /// <summary>
    /// Добавляет 1 еденицу модификатора, при условии налачия нужной суммы монет 
    /// </summary>
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
        if(Amount > 0)
        {
            Amount--;
            StartCoroutine(StartCooldown(WorkingTime));
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
    
    protected IEnumerator StartCooldown(int workingTime)
    {
        if (workingTime <= 0) yield break; 

        _Button.interactable = false;
        yield return new WaitForSeconds(workingTime);
        _Button.interactable = true;
    }
}
