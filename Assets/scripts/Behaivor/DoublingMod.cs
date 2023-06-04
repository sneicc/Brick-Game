using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublingMod : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GameBall"))
        {
            gameObject.SetActive(false);

            BallCloner.CreateClone(collision.gameObject);
        }
    }
}
