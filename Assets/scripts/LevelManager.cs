using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private string _name;

    public int LevelCoins { get; private set; }
    public int LevelDaimonds { get; private set; }


    public event Action CoinsChanged;
    public event Action DaimondsChanged;


    private void Awake()
    {
        if (Instance is not null) Destroy(gameObject);
        Instance = this;

        _name = SceneManager.GetActiveScene().name;

        LevelCoins = 0;
        LevelDaimonds = 0;

        GameManager.ResumeGame();
    }
    void Update()
    {
        
    }

    public void AddCoins(int cost)
    {
        if (cost >= 0) LevelCoins += cost;
        CoinsChanged?.Invoke();
    }

    public void AddDaimonds(int cost)
    {
        if (cost >= 0) LevelDaimonds += cost;
        DaimondsChanged?.Invoke();
    }

    private void SaveChanges(float coef)
    {
        int collectedCoins = (int)Math.Ceiling(LevelCoins * coef);
        int collectedDaimonds = (int)Math.Ceiling(LevelDaimonds * coef);
        GameManager.AddCoins(collectedCoins);
        GameManager.AddDaimonds(collectedDaimonds);
    }

    public void Retry(float coef = 1f)
    {
        SaveChanges(coef);
        int level = GetCurrentLevelNumber();
        GameManager.NewGame(level);
    }

    public void Exit(float coef = 1f)
    {
        SaveChanges(coef);
        SceneManager.LoadScene("LevelMap");
    }

    public void NextLevel(float coef = 1f)
    {
        SaveChanges(coef);
        int level = GetCurrentLevelNumber() + 1;
        GameManager.NewGame(level);
    }

    private int GetCurrentLevelNumber()
    {
        int level;
        _ = int.TryParse(_name.Split(' ')[1], out level);
        return level;
    }
}
