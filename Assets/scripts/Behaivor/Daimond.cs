using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daimond : Currency
{
    public bool IsCollected { get; private set; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GameBall"))
        {
            LevelManager.Instance.AddDaimonds(Cost);
            IsCollected = true;
            DesplayCostAndDisable();
        }
    }
}
