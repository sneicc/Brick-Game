using UnityEngine;

public class SkinManager : MonoBehaviour, ISaveable
{
    public static SkinManager Instance;

    [SerializeField]
    private Material[] Skins;
    public int SkinIndex { get; private set; }

    private void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if(SkinIndex >= 0 && SkinIndex < Skins.Length)
        {
            Ball2D.CustomSkin = Skins[SkinIndex];
        }
        else
        {
            Ball2D.CustomSkin = null;
        }       
    }

    public void SetSkin(int i)
    {
        if(i >= 0 && i < Skins.Length)
        {
            SkinIndex = i;
            Ball2D.CustomSkin = Skins[SkinIndex];
        }
    }

    public void Save(SaveData saveData)
    {
        saveData.SkinIndex = SkinIndex;
    }

    public void Load(SaveData saveData)
    {
        SkinIndex = saveData.SkinIndex;
    }
}
