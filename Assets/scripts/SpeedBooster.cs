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
        ball.BounceSpeed -= speed;
        ball.SpeedModCounter--;

        if (ball.SpeedModCounter == 0) ball.IsImmortal = false;
        
#if DEBUG
        if(ball.SpeedModCounter < 0)
        {
            Debug.Log("Bug");
        }
#endif        
    }
}
