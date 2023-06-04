using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private string _name;

    public int LevelCoins { get; private set; }
    private int _levelCoins;
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
        if (Instance is not null) Destroy(gameObject);
        Instance = this;

        _name = SceneManager.GetActiveScene().name;

        LevelCoins = 0;
        LevelDaimonds = 0;

        LevelNumber = GetCurrentLevelNumber();

        GameManager.GameWin += OnGameWin;
        GameManager.GameLoose += OnGameLoose;
        GameManager.ResumeGame();
    }

    public void AddCoins(int cost)
    {
        if (cost >= 0)
        {
            LevelCoins += cost;
            _levelCoins += cost;
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
    private void SaveChanges(float coef)
    {
        int collectedCoins = (int)Math.Ceiling(_levelCoins * coef);
        GameManager.AddCoins(collectedCoins);
        _levelCoins = 0;
    }

    /// <summary>
    /// Сохраняет все ресурсы
    /// </summary>
    /// <param name="coef"></param>
    private void SaveChanges(int coef)
    {
        _levelCoins *= coef;
        LevelDaimonds *= coef;
        GameManager.AddCoins(_levelCoins);
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

    private void OnGameLoose()
    {
        SaveChanges(LooseCoefficient);
    }
}
