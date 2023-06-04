using UnityEngine;

public class Coin : Currency
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GameBall"))
        {
            LevelManager.Instance.AddCoins(Cost);
            DesplayCostAndDisable();
        }
    }
}
