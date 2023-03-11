using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMod : MonoBehaviour
{
    public event Action OnSpeedModEnd;

    public float Speed  = 7;
    public float Duration = 3;

    private BallB BallB;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameBall"))
        {
            BallB = other.gameObject.GetComponent<BallB>();

            BallB.BounceSpeed = Speed;
            BallB.RB.velocity = BallB.RB.velocity.normalized * Speed;

            Invoke(nameof(RemoveMod), Duration);
            gameObject.SetActive(false);
        }
    }

    private void RemoveMod()
    {
        OnSpeedModEnd?.Invoke();
        Destroy(gameObject);
    }
}
