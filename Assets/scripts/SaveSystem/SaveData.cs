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
    public int BallDamageAmount;

    public int PlatformSpeedIndex;
    public int PlatformSpeedAmount;

    public int DamageModIndex;
    public int DamageModAmount;

    public int SpeedModIndex;
    public int SpeedModAmount;

    public int DoublingModIndex;
    public int DoublingModAmount;

    public int ExplosionModIndex;
    public int ExplosionModAmount;

    public int SkinIndex;

    public SaveData()
    {
        Coins = 0;
        Daimonds = 0;

        BallDamageIndex = 0;

        PlatformSpeedIndex = 0;

        DamageModIndex = 0;
        DamageModAmount = 0;

        SpeedModIndex = 0;
        SpeedModAmount = 0;

        DoublingModIndex = 0;
        DoublingModAmount = 0;

        ExplosionModIndex = 0;
        ExplosionModAmount = 0;

        LevelStars = new int[99];
        CurrentOpenedLevel = 0;

        SkinIndex = 0;
    }
}
