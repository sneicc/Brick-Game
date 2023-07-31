using UnityEngine;

public class Hearth : MonoBehaviour, IPickable
{
    public void PickUp()
    {
        GameManager.AddLive();
        Destroy(gameObject);
    }
}
