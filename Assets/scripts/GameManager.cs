using System;
using System.Collections;
using System.Collections.Generic;
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
        int stars = CalculateStart();
        //endGameMenu.getcomponent<script>().StarsAmount()
        //(Canvas) endGameMenu.SetActive(stars);
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
       SceneManager.LoadScene("Level " + level);
    }

    public static void OpenPauseMenu()
    {
        //(Canvas) pauseMenu.SetActive(true);;
        PauseGame();
    }

    public static void ClosePauseMenu()
    {
        //(Canvas) pauseMenu.SetActive(false);;
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

    private void Awake()
    {
#if DEBUG
        Lives = 3;
#endif
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void Start()
    {
        
    }

    public static void AddCoins(int cost)
    {
        if(cost >= 0) Coins += cost;
    }

    public static bool RemoveCoins(int cost) 
    {
        if(Coins >= cost)
        {
            if (cost >= 0)
            {
                Coins -= cost;
                return true;
            }
        }
        return false;
    }

    public static void AddDaimonds(int cost)
    {
        if (cost >= 0) Daimonds += cost;
    }

    public static bool RemoveDaimonds(int cost)
    {
        if (Daimonds >= cost)
        {
            if (cost >= 0)
            {
                Coins -= cost;
                return true;
            }
        }
        return false;
    }
}
