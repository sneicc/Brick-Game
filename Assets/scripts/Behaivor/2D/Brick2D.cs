using System.Collections;
using TMPro;
using UnityEngine;

public class Brick2D : MonoBehaviour
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

    [SerializeField]
    private float NextHitDelay;



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

    /// <summary>
    /// Наносит урон блоку
    /// </summary>
    /// <param name="damage">Урон</param>
    /// <param name="direction">Направление в котором полетят частицы после уничтожения. Если не указать, то разлёт будет сферическим.</param>
    public void Hit(int damage, Vector2 direction = new Vector2())
    {
        if (Unbreakable) return;

        HP -= damage;
        CheckHP(direction);
        SetHPText();

        Unbreakable = true;

        if(gameObject.active) StartCoroutine(WaitBeforeNextHit(NextHitDelay));

        IEnumerator WaitBeforeNextHit(float delay)
        {
            yield return new WaitForSeconds(delay);
            Unbreakable = false;
        }
    }

    private void CheckHP(Vector2 direction)
    {
        if (Fragile || HP <= 0)
        {
            gameObject.SetActive(false);
            if (direction.magnitude != 0) ShowVFXBallBrick(direction);
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
        if (BasicVFX is null) return;
        CreateVFX(BasicVFX);
    }

    private void SetHPText()
    {
        if (HPText is not null) HPText.text = HP.ToString();
    }

    private void ShowVFXBallBrick(Vector2 direction)
    {
        if (VFX is null) return;

        //Vector3 rotation = new Vector3(collision.transform.eulerAngles.x, 90, 0);

        ParticleSystem particleSystem = CreateVFX(VFX);

        var shape = particleSystem.shape;
        Quaternion quaternion = Quaternion.LookRotation(direction);
        Vector2 rotation = quaternion.eulerAngles;
        shape.rotation = rotation;// * -1f;          
    
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
