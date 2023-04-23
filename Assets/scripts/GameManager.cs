using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public sealed class GameManager : MonoBehaviour
{
    private const int NumberOfLevels = 99;
    public static GameManager Instance;

    public static List<BallB> Balls = new List<BallB>();

    public static float Speed = 7; //установка через параметры уровная
    public static Vector3 SpawnPoint;
    public static int Damage = 10;
    public static int Coins { get; private set; }
    public static int Daimonds { get; private set; }

    public static float Luck { get; private set; }

    public static int Lives { get; private set; }
    public static int Score { get; private set; }

    public static float LevelStartTime { get; private set; }

    private static bool[] LevelStatus = new bool[NumberOfLevels];
    private static int _bricksOnLevel;


    //==========
    public static event EventHandler CoinsChanged;
    public static event EventHandler DiamondChanged;
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
        if (_bricksOnLevel == 0) EndGame();
    }

    public static void AddLive()
    {
        if (Lives < 6) Lives++;
    }
    public  static void RemoveLive()
    {
        Lives--;
        if (Lives <= 0) EndGame();
    }

    private static void EndGame()
    {
        PauseGame();
        if(Lives > 0)
        {
            //endGameMenu.getcomponent<script>().StarsAmount()
            //(Canvas) endGameMenu.SetActive(true);
            int stars = CalculateStart();
        }
        else
        {
            //(Canvas)losseGameMenu.SetActive(true);
        }            
        
        throw new NotImplementedException();
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
        LevelStartTime = Time.time;
        Lives = 3;

        LoadLevel(level);
    }

    private static void LoadLevel(int level)
    {
       SceneManager.LoadScene($"Level {level}", LoadSceneMode.Single);
       SceneManager.LoadScene("IN-GAME TOPBAR", LoadSceneMode.Additive);
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

    private static void PauseGame()
    {
        Time.timeScale = 0f; // остановить время
    }

    private static void ResumeGame()
    {
        Time.timeScale = 1f; // возобновить время
    }

    public static void AddCoins(int cost)
    {
        if(cost >= 0) Coins += cost;
        CoinsChanged?.Invoke(null, EventArgs.Empty);
    }

    public static bool RemoveCoins(int cost) 
    {
        if(Coins >= cost)
        {
            if (cost >= 0)
            {
                Coins -= cost;
                CoinsChanged?.Invoke(null, EventArgs.Empty);
                return true;
            }
        }
        return false;
    }

    public static void AddDaimonds(int cost)
    {
        if (cost >= 0) Daimonds += cost;
        DiamondChanged?.Invoke(null, EventArgs.Empty);
    }

    public static bool RemoveDaimonds(int cost)
    {
        if (Daimonds >= cost)
        {
            if (cost >= 0)
            {
                Coins -= cost;
                DiamondChanged?.Invoke(null, EventArgs.Empty);
                return true;
            }
        }
        return false;
    }
}
