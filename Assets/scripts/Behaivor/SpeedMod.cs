using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SpeedMod : MonoBehaviour
{
    public float Speed  = 7;
    public float Duration = 3;
    private Vector3 AddedSpeed;

    private BallB Ball;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameBall"))
        {
            Ball = other.gameObject.GetComponent<BallB>();

            Ball.SpeedModCounter++;
            Ball.IsImmortal = true;
            Ball.BounceSpeed += Speed;              

            Invoke(nameof(RemoveMod), Duration);
            gameObject.SetActive(false);
        }
    }

    private void RemoveMod()
    {
        if (!ReferenceEquals(Ball, null))
        {
            if(Ball.SpeedModCounter == 1) Ball.IsImmortal = false;
            Ball.SpeedModCounter--;
            Ball.BounceSpeed -= Speed;
        }

        Destroy(gameObject);
    }
}
