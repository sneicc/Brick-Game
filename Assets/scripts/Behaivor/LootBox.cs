using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    /// <summary>
    /// ���� ���������
    /// </summary>
    public int Chance;
    /// <summary>
    /// ������ ������
    /// </summary>
    public GameObject CoinPrefab;
    /// <summary>
    /// ������ ������
    /// </summary>
    public GameObject DaimondPrefab;
    /// <summary>
    /// ������ ����������
    /// </summary>
    public GameObject SpeedPrefab;
    /// <summary>
    /// ������ ���������
    /// </summary>
    public GameObject DoublingPrefab;
    /// <summary>
    /// ���� ��������
    /// </summary>
    private bool IsLootBox;
    /// <summary>
    /// ����
    /// </summary>
    private Bricks Brick;

    /// <summary>
    /// ������ ��������
    /// </summary>
    public Shader Shader;
    /// <summary>
    /// ���� ������
    /// </summary>
    private bool _isQuitting;

    /// <summary>
    /// ����������� �������� �� ���� ��������� � ��������� �������
    /// </summary>
    void Start()
    {
        if (Random.Range(1, 100) <= Chance) 
        {
            IsLootBox = true;
            Brick = gameObject.GetComponent<Bricks>();

            Renderer renderer = gameObject.GetComponent<Renderer>();
            Material material = new Material(Shader);           
            material.SetTexture("_MainTexture", renderer.material.mainTexture);
            material.color = renderer.material.color;
            renderer.material = material;
        } 
    }

    /// <summary>
    /// ��������� ����� ������
    /// </summary>
    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }

    /// <summary>
    /// ����� ���� ��� �����������
    /// </summary>
    private void OnDisable()
    {
        if (!IsLootBox) return;
        if (Brick.HP > 0) return;

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
            //��������� ���-��
        }
    }
}
