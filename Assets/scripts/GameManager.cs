using System;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    private const int NumberOfLevels = 99;
    public static GameManager Instance;

    public static List<Ball2D> Balls = new List<Ball2D>();

    public static float Speed = 6; //установка через параметры уровная
    public static int Coins { get; private set; }
    public static int Daimonds { get; private set; }
    public static float Luck { get; private set; }
    public static int Lives { get; private set; }
    public static int Stars { get; private set; }

    
    public static float LevelStartTime { get; private set; }
    private static int[] _levelStars = new int[NumberOfLevels];
    public static int CurrentOpenedLevel { get; private set; }
    private static int _loadedLevel;
    [SerializeField]
    private static float[,] LevelCompliteTime = new float[NumberOfLevels, 3];
    private static int _bricksOnLevel;

    public static bool IsPause { get; private set; }
    public static bool IsGameWin { get; private set; }
    private static bool _isLevelExit;

    //Events
    //==========
    public static event Action LivesChanged;
    public static event Action CoinsChanged;
    public static event Action DiamondChanged;
    public static event Action GameLoose;
    public static event Action<int> GameWin;
    //==========

    //Settings
    //==========

    //==========

    private void Awake()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
            return;
        }

#if DEBUG
        Lives = 3;
        Coins = 2000;
        Daimonds = 100;
#endif
        DontDestroyOnLoad(gameObject);
        Instance = this;
        _bricksOnLevel = 0;
    }

    public static void AddBrick()
    {
        _bricksOnLevel++;
    }

    public static void RemoveBrick()
    {
        _bricksOnLevel--;
        if (_bricksOnLevel == 0 && Lives > 0 && !_isLevelExit) WinGame();
    }

    public static void AddLive()
    {
        if (Lives < 6) 
        {          
            Lives++;
            LivesChanged?.Invoke();
        }
    }

    public  static void RemoveLive()
    {
        Lives--;
        LivesChanged?.Invoke();
        LooseGame();
    }

    private static void LooseGame()
    {
        if (Lives <= 0)
        {
            PauseGame();
            GameLoose?.Invoke();
        }
    }

    private static void WinGame()
    {
        if (IsGameWin) return;

        PauseGame();
        if(Lives > 0)
        {
            IsGameWin = true;

            int stars = 2;//CalculateStart();
            SetStars(_loadedLevel, stars);

            if (_loadedLevel == CurrentOpenedLevel) CurrentOpenedLevel++;

            GameWin?.Invoke(stars);           
        }         
    }

    private static int CalculateStart()
    {
        float timeInLevel = Time.time - LevelStartTime;
        if (timeInLevel < 90) return 3;
        else if (timeInLevel < 180) return 2;
        else return 1;
    }

    public static void NewGame(int level)
    {       
        LoadLevel(level);
        _loadedLevel = level;

        var bricks = FindObjectsOfType<Brick2D>();
        foreach (Brick2D brick in bricks) Destroy(brick.gameObject);

        IsGameWin = false;
        _isLevelExit = false;
        LevelStartTime = Time.time;
        Lives = 3;
    }

    public static void LoadLevel(int level)
    {
        _isLevelExit = true;

        string levelName = $"Level {level}";
        if(SceneUtility.GetBuildIndexByScenePath($"Scenes/{levelName}") >= 0)
        {
            SceneManager.LoadScene(levelName, LoadSceneMode.Single);
            SceneManager.LoadScene("GAME_LOOSE", LoadSceneMode.Additive);
            SceneManager.LoadScene("GAME_WIN", LoadSceneMode.Additive);
            SceneManager.LoadScene("IN-GAME TOPBAR", LoadSceneMode.Additive);
            SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
        }
#if DEBUG
        else
        {
            Debug.Log($"Scene {levelName} does not exist");
        }
#endif
    }

    public static void LoadMainMenu()
    {
        ResumeGame();
        _isLevelExit = true;
        SceneManager.LoadScene("LevelMap", LoadSceneMode.Single);
        SceneManager.LoadScene("TOPBAR", LoadSceneMode.Additive);
    }

    public static void PauseGame()
    {
        Time.timeScale = 0f; // остановить время
        IsPause = true;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1f; // возобновить время
        IsPause = false;
    }

    public static void AddCoins(int cost)
    {
        if(cost >= 0) Coins += cost;
        CoinsChanged?.Invoke();
    }

    public static bool RemoveCoins(int cost) 
    {
        if(Coins >= cost)
        {
            if (cost >= 0)
            {
                Coins -= cost;
                CoinsChanged?.Invoke();
                return true;
            }
        }
        return false;
    }

    public static void AddDaimonds(int cost)
    {
        if (cost >= 0) Daimonds += cost;
        DiamondChanged?.Invoke();
    }

    public static bool RemoveDaimonds(int cost)
    {
        if (Daimonds >= cost)
        {
            if (cost >= 0)
            {
                Daimonds -= cost;
                DiamondChanged?.Invoke();
                return true;
            }
        }
        return false;
    }

    public static void RemoveAllListeners()
    {
        GameLoose = null;
        GameWin = null;
    }

    private static void SetStars(int level, int stars)
    {
        if (_levelStars[level] < stars && stars < 4) _levelStars[level] = stars;
    }

}
