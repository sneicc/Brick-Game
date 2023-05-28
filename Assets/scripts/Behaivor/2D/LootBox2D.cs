using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox2D : MonoBehaviour
{
    /// <summary>
    /// Шанс появления
    /// </summary>
    public int Chance;
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

        int value = Random.Range(1, 100);

        if (value <= 45)
        {
            Instantiate(CoinPrefab, transform.position, new Quaternion());
        }
        else if (value >= 46 && value <= 90)
        {
            Instantiate(DaimondPrefab, transform.position, new Quaternion());
        }
        else
        {
            //придумать что-то
        }
    }
}
