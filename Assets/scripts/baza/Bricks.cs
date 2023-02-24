using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    public int HP = 1;
    public Material[] Materials;
    public Renderer Renderer;

    public bool Unbreakable;
    void Start()
    {
        Renderer = GetComponent<Renderer>();
        if(!Unbreakable)
        {
            HP = Materials.Length;
            Renderer.material = Materials[HP - 1];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Sphere")
        {
            Hit();
        }
    }

    void Hit()
    {
        if(Unbreakable) return;

        HP--;
        if(HP <= 0) gameObject.SetActive(false);
        else Renderer.material = Materials[HP - 1];
    }
}
