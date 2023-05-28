using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    private const int NumberOfLevels = 99;
    public static GameManager Instance;

    public static List<Ball> Balls = new List<Ball>();

    public static float Speed = 6; //установка через параметры уровная
    public static int Coins { get; private set; }
    public static int Daimonds { get; private set; }
    public static float Luck { get; private set; }
    public static int Lives { get; private set; }
    public static int Stars { get; private set; }

    public static float LevelStartTime { get; private set; }
    public static bool[] LevelStatus = new bool[NumberOfLevels];
    [SerializeField]
    private static float[,] LevelCompliteTime = new float[NumberOfLevels, 3];
    private static int _bricksOnLevel;

    public static bool IsPause { get; private set; }
    public static bool IsGameWin { get; private set; }

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

    private void Start()
    {

    }

    public static void AddBrick()
    {
        _bricksOnLevel++;
    }

    public static void RemoveBrick()
    {
        _bricksOnLevel--;
        if (_bricksOnLevel == 0 && Lives > 0) EndGame();
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
        if (Lives <= 0) EndGame();
    }

    private static void EndGame()
    {      
        PauseGame();
        if(Lives > 0)
        {
            IsGameWin = true;
            int stars = 2;//CalculateStart();
            GameWin?.Invoke(stars);
            
        }
        else
        {
            GameLoose?.Invoke();
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
        IsGameWin = false;
        LevelStartTime = Time.time;
        Lives = 3;

        LoadLevel(level);
    }

    public static void LoadLevel(int level)
    {
        string levelName = $"Level {level}";
        if(SceneUtility.GetBuildIndexByScenePath($"Scenes/{levelName}") >= 0)
        {
            SceneManager.LoadScene(levelName, LoadSceneMode.Single);
            SceneManager.LoadScene("GAME_LOOSE", LoadSceneMode.Additive);
            SceneManager.LoadScene("GAME_WIN", LoadSceneMode.Additive);
            SceneManager.LoadScene("IN-GAME TOPBAR", LoadSceneMode.Additive);
            SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
        }
        else
        {
            Debug.Log($"Scene {levelName} does not exist");
        }       
    }

    public static void LoadMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("LevelMap", LoadSceneMode.Single);
        SceneManager.LoadScene("TOPBAR", LoadSceneMode.Additive);
    }

    public static void OpenPauseMenu()
    {
        //(Canvas) pauseMenu.SetActive(true);
        PauseGame();
    }

    public static void ClosePauseMenu()
    {
        //(Canvas) pauseMenu.SetActive(false);
        ResumeGame();
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

}
