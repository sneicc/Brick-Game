using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int Coins;
    public int Daimonds;

    public int[] LevelStars;
    public int CurrentOpenedLevel;

    public int BallDamageIndex;
    public int PlatformSpeedIndex;
    public int DamageModIndex;
    public int SpeedModIndex;
    public int DoublingModIndex;
    public int ExplosionModIndex;

    public SaveData()
    {
        Coins = 0;
        Daimonds = 0;

        BallDamageIndex = 0;
        PlatformSpeedIndex = 0;
        DamageModIndex = 0;
        SpeedModIndex = 0;
        DoublingModIndex = 0;
        ExplosionModIndex = 0;

        LevelStars = new int[99];
        CurrentOpenedLevel = 0;
    }
}
