using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallB : MonoBehaviour
{
	public float MainSpeed { get; private set; }

	Vector3 LastPos;
	public Transform Ball;
	float Threashold = 1.0f;
	int XCounterStuck;
	int YCounterStuck;
	public int ReboundLimit = 5;

    public Rigidbody RB;

	public float MaxSpeed = 10f;
	public float BounceSpeed = 7f;

	private Vector3 StartPos;

	public Vector3[] PrevVelocity;

	void Start()
	{
		MainSpeed = BounceSpeed;

        XCounterStuck = 0;
		YCounterStuck = 0;
		LastPos = Ball.position;

        StartPos = new Vector3
			(
				StartPos.x = 17.63f,
				StartPos.y = 6.56f,
				StartPos.z = -4.24f
            );

        RB = GetComponent<Rigidbody>();
		Invoke(nameof(ResetBall), 2f);

		PrevVelocity = new Vector3[2] { RB.velocity, RB.velocity };

    }

	private void Update()
	{
		PrevVelocity[1] = PrevVelocity[0];
		PrevVelocity[0] = RB.velocity;
        RB.velocity = RB.velocity.normalized * BounceSpeed;
    }

	private void OnCollisionEnter(Collision collision)
	{
		
		if (collision.gameObject.name == "ball_deadzone")
		{
			gameObject.SetActive(false);
            Invoke(nameof(ResetBall), 1f);
        }	
		StuckHandle();
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

		Vector3 offset = Ball.position - LastPos;
		float xOffset = Mathf.Abs(offset.x);
		float yOffset = Mathf.Abs(offset.y);

		if (xOffset > Threashold) XCounterStuck = 0;
		if (yOffset > Threashold) YCounterStuck = 0;
		if (xOffset <= Threashold) XCounterStuck++;
		if (yOffset <= Threashold) YCounterStuck++;

		LastPos = Ball.position;
	}

	private void FixedUpdate()
	{

		if (RB.velocity.magnitude > MaxSpeed)
		{
			RB.velocity = RB.velocity.normalized * MaxSpeed;
		}		

	}

	private void ResetBall()
	{
        gameObject.SetActive(true);
        gameObject.transform.position = StartPos;
		RB.velocity = Vector3.zero;

        Vector3 force = new Vector3(Random.Range(-1f, 1f), -1f,0);
        RB.velocity = force.normalized * BounceSpeed;
    }


}
