using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����������� ��������� ����.
/// </summary>
public abstract class Modifier : Upgradable
{
    public event Action<int> AmountChanged;

    /// <summary>
    /// ����� ������ ������������.
    /// </summary>
    protected int WorkingTime;
    /// <summary>
    /// ���������� �������������.
    /// </summary>
    public int Amount { get; protected set; }
    /// <summary>
    /// ���� ������ ������������.
    /// </summary>
    protected int Price;

    /// <summary>
    /// ������ � ������� �������� �����������.
    /// </summary>
    protected Button _button;

    /// <summary>
    /// ��� ������� ���������� �������� ������ �� ������.
    /// </summary>
    /// 
    protected virtual void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// ��������� 1 ������� ������������, ��� ������� ������� ������ ����� �����.
    /// </summary>
    /// <returns>������ �������� ���������� ��������.</returns>
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
    /// ������ 1 ������� ������������, ��� ������� ������� ���� �� ����� ������� ������������ .
    /// </summary>
    /// <returns>������ �������� ���������� ��������.</returns>
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
    /// ����������� ������ �������� ��������� �� 1, ��� ������� ������� ������ ����� �����.
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
    /// ���������� ������ ��� ����������� ������� ������������� ������������.
    /// </summary>
    /// <param name="workingTime">����� ����������.</param>
    /// <returns>��������</returns>
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
