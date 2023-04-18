using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Модификатор поведения шара.
/// </summary>
public abstract class Modifier : MonoBehaviour
{
    /// <summary>
    /// Время работы модификатора.
    /// </summary>
    protected int WorkingTime;
    /// <summary>
    /// Количество модификаторов.
    /// </summary>
    protected int Amount;
    /// <summary>
    /// Цена одного модификатора.
    /// </summary>
    protected int Price;

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

    /// <summary>
    /// Кнопка к которой привязан модификатор.
    /// </summary>
    protected Button _Button;

    /// <summary>
    /// При запуске приложения получает ссылку на кнопку.
    /// </summary>
    protected virtual void  Start()
    {
        _Button = gameObject.GetComponent<Button>();
    }

    /// <summary>
    /// Добавляет 1 еденицу модификатора, при условии налачия нужной суммы монет.
    /// </summary>
    /// <returns>Булево значение результата операции.</returns>
    protected bool Buy()
    {
        if (GameManager.RemoveCoins(Price))
        {
            Amount++;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Тратит 1 еденицу модификатора, при условии налачия хотя бы одной единицы модификатора .
    /// </summary>
    /// <returns>Булево значение результата операции.</returns>
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

    /// <summary>
    /// Увеличивает индекс текущего улучшения на 1, при условии налачия нужной суммы монет.
    /// </summary>
    protected virtual void Upgrade()
    {
        if (UpgradeBonus.Length <= UpgradeIndex)
        {
            int currentPrice = UpgradePrice[UpgradeIndex];
            if (GameManager.RemoveCoins(currentPrice)) UpgradeIndex++;
        }     
    }
    
    /// <summary>
    /// Блокировка кнопки для ограничения частоты использования модификатора.
    /// </summary>
    /// <param name="workingTime">Время блокировки.</param>
    /// <returns>Итератор</returns>
    protected IEnumerator StartCooldown(int workingTime)
    {
        if (workingTime <= 0) yield break; 

        _Button.interactable = false;
        yield return new WaitForSeconds(workingTime);
        _Button.interactable = true;
    }
}
