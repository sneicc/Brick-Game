using System.Collections;
using System.Collections.Generic;
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

	public float MaxSpeed = 30f;
	public float BounceSpeed = 15f;

	private Vector3 StartPos;

	void Start()
	{
		XCounterStuck = 0;
		YCounterStuck = 0;
		LastPos = Ball.position;

        StartPos = new Vector3
			(
				StartPos.x = 18,
				StartPos.y = 7,
				StartPos.z = -4
			);

        rb = GetComponent<Rigidbody>();
		ResetBall();
	}

	private void OnCollisionEnter(Collision collision)
	{
		int rand = Random.Range(-3, 4);
		if (collision.gameObject.name == "ball_deadzone")
		{
			ResetBall();		
        }
		else
		{
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
            rb.velocity = rb.velocity.normalized * BounceSpeed;
        }

		if (XCounterStuck >= ReboundLimit)
		{
			rb.velocity = new Vector3(rb.velocity.x + rand, rb.velocity.y , rb.velocity.z);
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
        rb.velocity = new Vector3(0, -10, 0);
        gameObject.transform.position = StartPos;
    }


}
