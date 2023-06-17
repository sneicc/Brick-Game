using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    [SerializeField]
    private string FileName;

    private SaveData _saveData;
    private List<ISaveable> _saveables;
    private SaveDataHandler _saveHandler;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _saveHandler = new SaveDataHandler(Application.persistentDataPath, FileName);
        _saveables = new List<ISaveable>(FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>());

        LoadGame();
    }

    public void NewGame()
    {
        _saveData = new SaveData();
        PlayerPrefs.DeleteAll();
    }

    public void LoadGame()
    {
        _saveData = _saveHandler.Load();

        if (_saveData == null)
        {
            NewGame();
        }

#if DEBUG
        Debug.Log(Application.persistentDataPath);
        NewGame();
#endif

        foreach (var item in _saveables)
        {
            item.Load(_saveData);
        }
    }

    public void SaveGame()
    {
        foreach (var item in _saveables)
        {
            item.Save(_saveData);
        }

        _saveHandler.Save(_saveData);

    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveGame();
        }        
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveGame();
        }
    }

    private void OnDestroy()
    {
        SaveGame();
    }

}
