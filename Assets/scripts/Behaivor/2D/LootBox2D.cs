using UnityEngine;

public class LootBox2D : MonoBehaviour // ������ �������� �����, ���������� ��������� �����, ?��������� ��� � �������� ��������� ����?
{
    /// <summary>
    /// ���� ���������
    /// </summary>
    public int Chance;

    [SerializeField]
    private int ExplosionDamage = 25;
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

    public GameObject HearthPrefab;
    /// <summary>
    /// ���� ��������
    /// </summary>
    private bool IsLootBox;
    /// <summary>
    /// ����
    /// </summary>
    private Brick2D brick2D;

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
            brick2D = gameObject.GetComponent<Brick2D>();

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
        if (brick2D.HP > 0) return;

        if (!gameObject.scene.isLoaded) return;
        if (_isQuitting) return;

        int value = Random.Range(1, 101);

        if (value <= 50) //50
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
        else if (value >= 83 && value <= 99)
        {
            InstantiateLoot(DaimondPrefab);
        }
        else
        {
            InstantiateLoot(HearthPrefab);
        }
    }

    private void InstantiateLoot(GameObject gameObject)
    {
        if (gameObject.GetComponent<IPickable>() == null) return;

        var loot = Instantiate(gameObject, transform.position, new Quaternion());

        var picker = loot.GetComponent<Picker>();
        if(picker != null)
        {
            Destroy(picker);
        }

        var loot2D = loot.AddComponent<Loot2D>();
        loot2D.ObjectType = Picker.PickableType.Platform;
    }
}
