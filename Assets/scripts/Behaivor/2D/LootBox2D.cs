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

        if (value <= 25) //25
        {
            Instantiate(CoinPrefab, transform.position, new Quaternion());
        }
        else if (value >= 26 && value <= 50) //25
        {
            Explosion.Instance.Explode(transform.position, ExplosionDamage);
        }
        else if (value >= 51 && value <= 65) //15
        {
            Instantiate(SpeedPrefab, transform.position, new Quaternion());
        }
        else if (value >= 66 && value <= 80) //15
        {
            Instantiate(DoublingPrefab, transform.position, new Quaternion());
        }
        else if (value >= 81 && value <= 90) //10
        {
            Instantiate(HearthPrefab, transform.position, new Quaternion());
        }
        else // 10
        {
            Instantiate(DaimondPrefab, transform.position, new Quaternion());
        }
    }
}
