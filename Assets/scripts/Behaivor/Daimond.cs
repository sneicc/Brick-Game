using UnityEngine;

public class Daimond : Currency, IPickable
{
    public bool IsCollected { get; private set; }

    public void PickUp()
    {
        LevelManager.Instance.AddDaimonds(Cost);
        IsCollected = true;
        DesplayCostAndDisable();
    }
}
