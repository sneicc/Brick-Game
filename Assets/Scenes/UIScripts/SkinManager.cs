using UnityEngine;

public class SkinManager : MonoBehaviour, ISaveable
{
    public static SkinManager Instance;

    [SerializeField]
    private Material[] Skins;
    private int _skinIndex;

    private void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if(_skinIndex >= 0)
        {
            Ball2D.CustomSkin = Skins[_skinIndex];
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
            _skinIndex = i;
            Ball2D.CustomSkin = Skins[_skinIndex];
        }
    }

    public void Save(SaveData saveData)
    {
        saveData.SkinIndex = _skinIndex;
    }

    public void Load(SaveData saveData)
    {
        _skinIndex = saveData.SkinIndex;
    }
}
