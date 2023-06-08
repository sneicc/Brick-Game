using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class Brick : MonoBehaviour
{
    public int HP = 1;
    public Material[] Materials;
    private Renderer _renderer;
    public ParticleSystem VFX;
    public ParticleSystem BasicVFX;
    public float VFXParticleBrightness = 3f;

    public bool Unbreakable;
    public bool Fragile;

    public Color FirstColor;
    public Color SecondColor;

    public bool Gradient;
    private float RangeMaxBorder;

    public TextMeshProUGUI HPText;

    
    void Start()
    {
        GameManager.AddBrick();

        _renderer = GetComponent<Renderer>();
        if(!Unbreakable && !Fragile)
        {
            if (Gradient) 
            {
                _renderer.material.color = FirstColor; 
            }
            else
            {
                HP = Materials.Length;
                _renderer.material = Materials[HP - 1];
            }                        
        }
        SetHPText();
        RangeMaxBorder = HP;
    }

    private void OnDisable()
    {
        GameManager.RemoveBrick();
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

        HP -= collision.gameObject.GetComponent<Ball>().Damage;
        CheckHP(collision);
        SetHPText();
    }

    private void CheckHP(Collision collision = null)
    {
        if (Fragile || HP <= 0)
        {
            gameObject.SetActive(false);
            if (collision != null) ShowVFXBallBrick(collision);
            else ShowVFXBasic();
        }
        else if (Gradient)
        {
            float range = ConvertRange(0, RangeMaxBorder, 0, 1, HP);
            _renderer.material.color = Color.Lerp(SecondColor, FirstColor, range); ;
        }
        else
        {
            _renderer.material = Materials[HP - 1];
        }
    }

    private void ShowVFXBasic()
    {
        if (BasicVFX == null) return;
        CreateVFX(BasicVFX);
    }
    public void Hit(int Damage)
    {
        if (Unbreakable) return;

        HP -= Damage;
        CheckHP();
        SetHPText();
    }

    private void SetHPText()
    {
        if (HPText != null) HPText.text = HP.ToString();
    }

    private void ShowVFXBallBrick(Collision collision)
    {
        if (VFX == null) return;

        //Vector3 rotation = new Vector3(collision.transform.eulerAngles.x, 90, 0);
        try
        {
            ParticleSystem particleSystem = CreateVFX(VFX);

            var shape = particleSystem.shape;
            Quaternion quaternion = Quaternion.LookRotation(collision.gameObject.GetComponent<Ball>().PrevVelocity[1]);
            Vector3 rotation = quaternion.eulerAngles;
            shape.rotation = rotation;// * -1f;          
        }
        catch
        {
            Debug.Log($"{collision.gameObject.name} не имеет компонент BallB");
        }       
    }

    private ParticleSystem CreateVFX(ParticleSystem ps)
    {
        var vfx = Instantiate(ps.gameObject, transform.position, transform.rotation);
        ParticleSystem particleSystem = vfx.GetComponent<ParticleSystem>();
        var vfxSettings = particleSystem.main;
        _renderer.material.SetFloat("_Saturation", VFXParticleBrightness);
        vfxSettings.startColor = _renderer.material.color;
        return particleSystem;
    }

    private float ConvertRange(float oldMinBorder, float oldMaxBorder, float newMinBorder,float newMaxBorder, float value)
    {       
        return (value - oldMinBorder) / (oldMaxBorder - oldMinBorder) * (newMaxBorder - newMinBorder) + newMinBorder;
    }
}
