using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : Currency
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameBall"))
        {
            LevelManager.Instance.AddCoins(Cost);
            DesplayCostAndDestroy();
        }       
    }  
}
