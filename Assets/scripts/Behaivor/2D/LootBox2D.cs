using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox2D : MonoBehaviour
{
    /// <summary>
    /// Шанс появления
    /// </summary>
    public int Chance;

    [SerializeField]
    private int ExplosionDamage = 25;
    /// <summary>
    /// Префаб монеты
    /// </summary>
    public GameObject CoinPrefab;
    /// <summary>
    /// Префаб алмаза
    /// </summary>
    public GameObject DaimondPrefab;
    /// <summary>
    /// Префаб ускорителя
    /// </summary>
    public GameObject SpeedPrefab;
    /// <summary>
    /// Префаб удвоителя
    /// </summary>
    public GameObject DoublingPrefab;

    public GameObject HearthPrefab;
    /// <summary>
    /// Флаг лутбокса
    /// </summary>
    private bool IsLootBox;
    /// <summary>
    /// Блок
    /// </summary>
    private Brick2D brick2D;

    /// <summary>
    /// Шейдер лутбокса
    /// </summary>
    public Shader Shader;
    /// <summary>
    /// Флаг выхода
    /// </summary>
    private bool _isQuitting;

    /// <summary>
    /// Определение является ли блок лутбоксом и установка шейдера
    /// </summary>
    void Start()
    {
        if (Random.Range(1, 100) <= Chance) 
        {
            IsLootBox = true;
            brick2D = gameObject.GetComponent<Brick2D>();

            Renderer renderer = gameObject.GetComponent<Renderer>();
            Material material = new Material(Shader);           
            material.SetTexture("_MainTexture", renderer.material.mainTexture);
            material.color = renderer.material.color;
            renderer.material = material;
        } 
    }

    /// <summary>
    /// Установка флага выхода
    /// </summary>
    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }

    /// <summary>
    /// Спавн лута при уничтожении
    /// </summary>
    private void OnDisable()
    {
        if (!IsLootBox) return;
        if (brick2D.HP > 0) return;

        if (!gameObject.scene.isLoaded) return;
        if (_isQuitting) return;

        int value = Random.Range(1, 101);

        if (value <= 25)
        {
            Instantiate(CoinPrefab, transform.position, new Quaternion());
        }
        else if (value >= 26 && value <= 50)
        {
            Explosion.Instance.Explode(transform.position, ExplosionDamage);
        }
        else if (value >= 51 && value <= 75)
        {
            Instantiate(SpeedPrefab, transform.position, new Quaternion());
        }
        else if (value >= 76 && value <= 82)
        {
            Instantiate(DoublingPrefab, transform.position, new Quaternion());
        }
        else if (value >= 83 && value <= 94)
        {
            Instantiate(HearthPrefab, transform.position, new Quaternion());
        }
        else
        {
            Instantiate(DaimondPrefab, transform.position, new Quaternion());
        }
    }
}
