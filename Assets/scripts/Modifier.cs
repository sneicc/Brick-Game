using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modifier : MonoBehaviour
{
    protected int WorkingTime;
    protected int Amount;
    protected int Price;


    protected Modifier(int workingTime, int amount, int price)
    {
        WorkingTime = workingTime;
        Amount = amount;
        Price = price;
    }

    protected void Buy()
    {
        if (GameManager.RemoveCoins(Price))
            Amount++;
        else
            throw new NotImplementedException(); //высплывающее окно нехватки монет
    }
}
