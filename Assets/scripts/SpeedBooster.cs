using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpeedBooster
{
    public static void AddSpeed(Ball2D ball, float speed)
    {
        ball.SpeedModCounter++;
        ball.IsImmortal = true;
        ball.BounceSpeed += speed;       
    }

    public static void RemoveSpeed(Ball2D ball, float speed)
    {
        if (ball.SpeedModCounter == 1) ball.IsImmortal = false;
        ball.SpeedModCounter--;
        ball.BounceSpeed -= speed;
    }
}
