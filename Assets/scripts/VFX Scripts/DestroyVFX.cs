using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DestroyVFX : MonoBehaviour
{
    public GameObject VFX;
    private void OnDisable()
    {
        GameObject destroyVFX = Instantiate(VFX, transform.position, transform.rotation);
    }
}
