using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    public int Chance;
    public GameObject CoinPrefab;
    public GameObject DaimondPrefab;
    public GameObject SpeedPrefab;
    public GameObject DoublingPrefab;
    private bool IsLootBox;
    private Bricks Brick;


    public Shader shader;

    void Start()
    {
        if (Random.Range(1, 100) <= Chance) 
        {
            IsLootBox = true;
            Brick = gameObject.GetComponent<Bricks>();

            Renderer renderer = gameObject.GetComponent<Renderer>();
            Material material = new Material(shader);           
            material.SetTexture("_MainTexture", renderer.material.mainTexture);
            material.color = renderer.material.color;
            renderer.material = material;
        } 
    }

    private void OnCollisionEnter(Collision collision) // оперделить вероятности и добавить спавн модификаторов
    {
        if (!IsLootBox) return;
        if (Brick.HP > 0) return;

        int value = Random.Range(1, 100);

        if (value <= 45)
        {
            Instantiate(CoinPrefab, transform.position, transform.rotation);
        }
        else if (value >= 46 && value <= 90)
        {
            Instantiate(DaimondPrefab, transform.position, transform.rotation);
        }
        else
        {
            //придумать что-то
        }

    }
}
