using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallB : MonoBehaviour
{
	Vector3 LastPos;
	public Transform Ball;
	float Threashold = 1.0f;
	int XCounterStuck;
	int YCounterStuck;
	public int ReboundLimit = 5;

    private Rigidbody rb;

	public float MaxSpeed = 10f;
	public float BounceSpeed = 7f;

	private Vector3 StartPos;

	public Vector3[] PrevVelocity;

	void Start()
	{
		XCounterStuck = 0;
		YCounterStuck = 0;
		LastPos = Ball.position;

        StartPos = new Vector3
			(
				StartPos.x = 17.63f,
				StartPos.y = 6.56f,
				StartPos.z = -4.24f
            );

        rb = GetComponent<Rigidbody>();
		Invoke(nameof(ResetBall), 2f);

		PrevVelocity = new Vector3[2] { rb.velocity, rb.velocity };

    }

	private void Update()
	{		
			PrevVelocity[1] = PrevVelocity[0];
			PrevVelocity[0] = rb.velocity;			
	}

	private void OnCollisionEnter(Collision collision)
	{
		
		if (collision.gameObject.name == "ball_deadzone")
		{
			gameObject.SetActive(false);
            Invoke(nameof(ResetBall), 1f);
        }
		else
		{
			rb.velocity = rb.velocity.normalized * BounceSpeed;
		}		
		StuckHandle();
	}

	private void StuckHandle()
	{
        int rand = Random.Range(-3, 4);

        if (XCounterStuck >= ReboundLimit)
		{
			rb.velocity = new Vector3(rb.velocity.x + rand, rb.velocity.y, rb.velocity.z);
			XCounterStuck = 0;
		}
		else if (YCounterStuck >= ReboundLimit)
		{
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + rand, rb.velocity.z);
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

		if (rb.velocity.magnitude > MaxSpeed)
		{
			rb.velocity = rb.velocity.normalized * MaxSpeed;
		}		

	}

	private void ResetBall()
	{
        gameObject.SetActive(true);
        gameObject.transform.position = StartPos;
		rb.velocity = Vector3.zero;

        Vector3 force = new Vector3(Random.Range(-1f, 1f), -1f,0);
        rb.velocity = force.normalized * BounceSpeed;
    }


}
