using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static List<BallB> Balls = new List<BallB>();

    public static float Speed = 7; //установка через параметры уровная
    public static Vector3 SpawnPoint;
    public static int Damage = 10;
    public static int Coins { get; private set; }
    public static int Daimonds { get; private set; }


    //private static int Time
    public static float Luck {get; private set; }


    //public static event Action On;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddCoins(int cost)
    {
        if(cost >= 0) Coins += cost;
    }

    public static void RemoveCoins(int cost) 
    {
        if(cost >= 0) Coins -= cost;
    }

   




}
