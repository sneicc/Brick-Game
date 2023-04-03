using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    public int HP = 1;
    public Material[] Materials;
    private Renderer Renderer;
    public ParticleSystem VFX;

    public bool Unbreakable;
    public bool Fragile;

    public Color FirstColor;
    public Color SecondColor;

    public bool Gradient;
    private float RangeMaxBorder;

    public TextMeshProUGUI HPText;
    void Start()
    {
        Renderer = GetComponent<Renderer>();
        if(!Unbreakable && !Fragile)
        {
            if (Gradient) 
            {
                Renderer.material.color = FirstColor; 
            }
            else
            {
                HP = Materials.Length;
                Renderer.material = Materials[HP - 1];
            }                        
        }
        SetHPText();
        RangeMaxBorder = HP;
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
        if (Unbreakable) return;

        HP -= collision.gameObject.GetComponent<BallB>().Damage;

        if (Fragile || HP <= 0)
        {
            gameObject.SetActive(false);
            ShowVFX(collision);
        }
        else if (Gradient)
        {
            float range = ConvertRange(0, RangeMaxBorder, 0, 1, HP);
            Renderer.material.color = Color.Lerp(SecondColor, FirstColor, range); ;
        }
        else
        {
            Renderer.material = Materials[HP - 1];
        }

        SetHPText();
    }

    private void SetHPText()
    {
        if (HPText is not null) HPText.text = HP.ToString();
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

    private float ConvertRange(float oldMinBorder, float oldMaxBorder, float newMinBorder,float newMaxBorder, float value)
    {       
        return (value - oldMinBorder) / (oldMaxBorder - oldMinBorder) * (newMaxBorder - newMinBorder) + newMinBorder;
    }

    


}
