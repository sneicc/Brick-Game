using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearth : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameBall"))
        {
            GameManager.AddLive();
            Destroy(gameObject);
        }
    }
}
