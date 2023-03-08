using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    public int HP = 1;
    public Material[] Materials;
    private Renderer Renderer;
    public ParticleSystem VFX;

    public bool Unbreakable;
    public bool Fragile;
    void Start()
    {
        Renderer = GetComponent<Renderer>();
        if(!Unbreakable && !Fragile)
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
        if(collision.gameObject.CompareTag("GameBall"))
        {
            Hit(collision);
        }
    }

    public void Hit(Collision collision)
    {
        if(Unbreakable) return;

        HP--;
        if (Fragile || HP <= 0) 
        {
            gameObject.SetActive(false);
            ShowVFX(collision);
        }
        else Renderer.material = Materials[HP - 1];
    }

    private void ShowVFX(Collision collision)
    {
        var shape = VFX.shape;
        //Vector3 rotation = new Vector3(collision.transform.eulerAngles.x, 90, 0);
        Quaternion quaternion = Quaternion.LookRotation(collision.gameObject.GetComponent<BallB>().PrevVelocity[1]);
        Vector3 rotation = quaternion.eulerAngles;
        shape.rotation = rotation;// * -1f;
        Instantiate(VFX.gameObject, transform.position,transform.rotation);
    }


}
