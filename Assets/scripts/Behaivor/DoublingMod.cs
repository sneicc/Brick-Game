using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublingMod : MonoBehaviour
{
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameBall"))
        {
            gameObject.SetActive(false);

            BallCloner.CreateClone(other.gameObject);
        }
    }  
}
