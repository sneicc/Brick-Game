using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickDie : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Sphere")
        {

            Destroy(gameObject);

        }
    }
}
