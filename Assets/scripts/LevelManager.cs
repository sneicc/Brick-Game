using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private string _name;

    public int LevelCoins { get; private set; }
    public int LevelDaimonds { get; private set; }

    public event Action CoinsChanged;
    public event Action DaimondsChanged;

    public int LevelNumber { get; private set; }
    [SerializeField]
    private float _looseCoefficient;
    public float LooseCoefficient
    {
        get { return _looseCoefficient; }
        set { _looseCoefficient = value; }
    }

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        _name = SceneManager.GetActiveScene().name;

        LevelCoins = 0;
        LevelDaimonds = 0;

        LevelNumber = GetCurrentLevelNumber();

        GameManager.GameWin += OnGameWin;
        GameManager.ResumeGame();
    }

    public void AddCoins(int cost)
    {
        if (cost >= 0)
        {
            LevelCoins += cost;
            CoinsChanged?.Invoke();
        }
    }

    public void AddDaimonds(int cost)
    {
        if (cost >= 0)
        {
            LevelDaimonds += cost;
            DaimondsChanged?.Invoke();
        }
    }

    /// <summary>
    /// Сохраняет только монеты
    /// </summary>
    /// <param name="coef"></param>
    public void SaveChanges(float coef)
    {
        int collectedCoins = (int)Math.Ceiling(LevelCoins * coef);
        GameManager.AddCoins(collectedCoins);
    }

    /// <summary>
    /// Сохраняет все ресурсы
    /// </summary>
    /// <param name="coef"></param>
    public void SaveChanges(int coef)
    {
        LevelCoins *= coef;
        LevelDaimonds *= coef;
        GameManager.AddCoins(LevelCoins);
        GameManager.AddDaimonds(LevelDaimonds);
    }

    public void Retry()
    {    
        GameManager.NewGame(LevelNumber);
    }

    public void NextLevel()
    {
        int level = LevelNumber + 1;
        GameManager.NewGame(level);
    }

    private int GetCurrentLevelNumber()
    {
        int level;
        _ = int.TryParse(_name.Split(' ')[1], out level);
        return level;
    }

    public void MultiplyRewardX2()
    {
        SaveChanges(1);
    }

    private void OnGameWin(int _)
    {
        SaveChanges(1);
    }

    private void OnDestroy()
    {
        Instance = null;
        GameManager.GameWin -= OnGameWin;
    }
}
