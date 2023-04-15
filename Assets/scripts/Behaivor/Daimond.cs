using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daimond : Currency
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameBall"))
        {
            GameManager.AddDaimonds(Cost);
            DesplayCostAndDestroy();
        }
    }
}
