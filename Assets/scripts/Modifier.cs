using System.Collections;
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
    protected Button _button;

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
            StartCoroutine(StartCooldown(WorkingTime));
            return true;
        }
        return false;
    }

    /// <summary>
    /// ����������� ������ �������� ��������� �� 1, ��� ������� ������� ������ ����� �����.
    /// </summary>
    public virtual void Upgrade()
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
