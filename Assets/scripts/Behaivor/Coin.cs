using UnityEngine;

public class Coin : Currency, IPickable
{
    public void PickUp()
    {
        LevelManager.Instance.AddCoins(Cost);
        DesplayCostAndDisable();
    }    
}
