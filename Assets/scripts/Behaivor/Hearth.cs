using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearth : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GameBall"))
        {
            GameManager.AddLive();
            Destroy(gameObject);
        }
    }
}
