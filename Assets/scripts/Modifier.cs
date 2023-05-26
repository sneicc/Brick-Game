using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Модификатор поведения шара.
/// </summary>
public abstract class Modifier : Upgradable
{
    public event Action<int> AmountChanged;

    /// <summary>
    /// Время работы модификатора.
    /// </summary>
    protected int WorkingTime;
    /// <summary>
    /// Количество модификаторов.
    /// </summary>
    public int Amount { get; protected set; }
    /// <summary>
    /// Цена одного модификатора.
    /// </summary>
    protected int Price;

    /// <summary>
    /// Кнопка к которой привязан модификатор.
    /// </summary>
    protected Button _button;

    /// <summary>
    /// При запуске приложения получает ссылку на кнопку.
    /// </summary>
    /// 
    protected virtual void Start()
    {
        DontDestroyOnLoad(gameObject);
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
            AmountChanged?.Invoke(Amount);
            StartCoroutine(StartCooldown(WorkingTime));
            return true;
        }
        return false;
    }

    /// <summary>
    /// Увеличивает индекс текущего улучшения на 1, при условии налачия нужной суммы монет.
    /// </summary>
    public override void Upgrade()
    {
        int nextIndex = UpgradeIndex + 1;
        if (nextIndex < UpgradeBonus.Length)
        {
            int currentPrice = UpgradePrice[nextIndex];
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

        if (GameObject.ReferenceEquals(_button, null)) yield break;
        _button.interactable = false;
        yield return new WaitForSeconds(workingTime);
        if (GameObject.ReferenceEquals(_button, null)) yield break;
        _button.interactable = true;
    }

    public abstract void Activate();

    public virtual void Subscribe(Button button)
    {
        _button = button;
        _button.onClick.AddListener(Activate);
    }

    public virtual void Unsubscribe()
    {
        _button.onClick.RemoveListener(Activate);
        _button = null;
    }
}
