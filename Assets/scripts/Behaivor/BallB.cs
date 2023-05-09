using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallB : MonoBehaviour
{
	public int Damage;
    public float MainSpeed { get; private set; }
    public float MaxSpeed = 50f;
    public float BounceSpeed;

    Vector3 LastPos;
	//public Transform Ball;
	public float Threashold = 1.0f;
	private int XCounterStuck;
	private int YCounterStuck;
	public int ReboundLimit = 5;

    private Rigidbody RB;
	public Material CloneMaterial;

	public Vector3[] PrevVelocity;

	public bool IsClone = false;
	public bool IsImmortal = false;

	/// <summary>
	/// Количество действующих модификаторов скорости
	/// </summary>
	public int SpeedModCounter = 0;
	private Vector3 _spawn;

	private void Awake()
	{
        _spawn = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        MainSpeed = BounceSpeed = GameManager.Speed;
        Damage = GameManager.Damage;
		RB = gameObject.GetComponent<Rigidbody>();

        GameManager.Balls.Add(this);
	}

	void Start()
	{
        XCounterStuck = 0;
		YCounterStuck = 0;
		LastPos = transform.position;

        RB = GetComponent<Rigidbody>();
		if (!IsClone) ResetBall();
		else gameObject.GetComponent<Renderer>().material = CloneMaterial;

        PrevVelocity = new Vector3[2] { RB.velocity, RB.velocity };

    }

	private void Update()
	{
		PrevVelocity[1] = PrevVelocity[0];
		PrevVelocity[0] = RB.velocity;
        SetMainSpeed();
    }

	private void OnCollisionEnter(Collision collision)
	{	
		if (collision.gameObject.name == "ball_deadzone" && !IsImmortal)
		{
            if (IsClone)
            {
                Destroy(gameObject);
                return;
            }
			GameManager.RemoveLive();
            gameObject.SetActive(false);
            Invoke(nameof(ResetBall), 1f);
        }

        StuckHandle();
	}
	private void BallB_OnSpeedModEnd()
	{
		BounceSpeed = MainSpeed;
		SetMainSpeed();
	}

	private void SetMainSpeed()
	{
		RB.velocity = RB.velocity.normalized * BounceSpeed;
	}

	private void StuckHandle()
	{
        int rand = Random.Range(-3, 4);

        if (XCounterStuck >= ReboundLimit)
		{
			RB.velocity = new Vector3(RB.velocity.x + rand, RB.velocity.y, RB.velocity.z);
			XCounterStuck = 0;
		}
		else if (YCounterStuck >= ReboundLimit)
		{
			RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y + rand, RB.velocity.z);
			YCounterStuck = 0;
		}

		Vector3 offset = transform.position - LastPos;
		float xOffset = Mathf.Abs(offset.x);
		float yOffset = Mathf.Abs(offset.y);

		if (xOffset > Threashold) XCounterStuck = 0;
		if (yOffset > Threashold) YCounterStuck = 0;
		if (xOffset <= Threashold) XCounterStuck++;
		if (yOffset <= Threashold) YCounterStuck++;

		LastPos = transform.position;
	}

	private void ResetBall()
	{
		BounceSpeed = MainSpeed;
        gameObject.SetActive(true);
        gameObject.transform.position = _spawn;

		RB.velocity = Vector3.zero;
        Vector3 force = new Vector3(Random.Range(-1f, 1f), -1f,0);
        RB.velocity = force.normalized * BounceSpeed;
    }

	private void OnDestroy()
	{
		GameManager.Balls.Remove(this);
	}

}
