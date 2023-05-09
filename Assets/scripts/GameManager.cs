using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    private const int NumberOfLevels = 99;
    public static GameManager Instance;

    public static List<BallB> Balls = new List<BallB>();

    public static float Speed = 6; //��������� ����� ��������� �������
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

    public static bool IsPause { get; private set; }

    //Events
    //==========
    public static event Action CoinsChanged;
    public static event Action DiamondChanged;
    public static event Action GameLoose;
    public static event Action GameWin;
    //==========

    //Settings
    //==========

    //==========

    private void Awake()
    {
        Application.targetFrameRate = 120;

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
            GameLoose?.Invoke();
            //(Canvas)losseGameMenu.SetActive(true);
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
        LevelStartTime = Time.time;
        Lives = 3;

        LoadLevel(level);
    }

    public static void LoadLevel(int level)
    {       
        SceneManager.LoadScene($"Level {level}", LoadSceneMode.Single);
        SceneManager.LoadScene("IN-GAME TOPBAR", LoadSceneMode.Additive);
        SceneManager.LoadScene("GAME_LOOSE", LoadSceneMode.Additive);        
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
        Time.timeScale = 0f; // ���������� �����
        IsPause = true;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1f; // ����������� �����
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
                Coins -= cost;
                DiamondChanged?.Invoke();
                return true;
            }
        }
        return false;
    }

}
