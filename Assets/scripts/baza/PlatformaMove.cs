using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformaMove : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    public Vector3 Direction { get; private set; }
    public float Speed = 10;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Direction = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Direction = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Direction = Vector3.right;
        }
        else
        {
            Direction = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        Rigidbody.AddForce(Direction * Speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Ball ball = collision.gameObject.GetComponent<Ball>;
    }

}
