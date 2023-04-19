using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����������� ��������� ����.
/// </summary>
public abstract class Modifier : MonoBehaviour
{
    /// <summary>
    /// ����� ������ ������������.
    /// </summary>
    protected int WorkingTime;
    /// <summary>
    /// ���������� �������������.
    /// </summary>
    protected int Amount;
    /// <summary>
    /// ���� ������ ������������.
    /// </summary>
    protected int Price;

    /// <summary>
    /// ������ ��������� ������������� ������������.
    /// </summary>
    protected float[] UpgradeBonus;
    /// <summary>
    /// ������ ��� ��� ��������� ������������.
    /// </summary>
    protected int[] UpgradePrice;
    /// <summary>
    /// ������� ������ ��������� ������������.
    /// </summary>
    protected int UpgradeIndex;

    /// <summary>
    /// ������ � ������� �������� �����������.
    /// </summary>
    protected Button _Button;

    /// <summary>
    /// ��� ������� ���������� �������� ������ �� ������.
    /// </summary>
    protected virtual void  Start()
    {
        _Button = gameObject.GetComponent<Button>();
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
            StartCoroutine(StartCooldown(WorkingTime));
            return true;
        }
        return false;
    }

    /// <summary>
    /// ����������� ������ �������� ��������� �� 1, ��� ������� ������� ������ ����� �����.
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
    /// ���������� ������ ��� ����������� ������� ������������� ������������.
    /// </summary>
    /// <param name="workingTime">����� ����������.</param>
    /// <returns>��������</returns>
    protected IEnumerator StartCooldown(int workingTime)
    {
        if (workingTime <= 0) yield break; 

        _Button.interactable = false;
        yield return new WaitForSeconds(workingTime);
        _Button.interactable = true;
    }
}
