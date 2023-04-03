using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformaMove : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    public Vector3 Direction { get; private set; }
    public float Speed = 10;

    private Collider Collider;

    public float MaxBounceAngle = 80;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Direction = Vector3.zero;

        Collider = GetComponent<Collider>();
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
        if (collision.gameObject.CompareTag("GameBall"))
        {

            Vector3 platformPosition = transform.position;
            Vector3 contactPoint = collision.GetContact(0).point;

            float offset = platformPosition.x - contactPoint.x;
            float width = Collider.bounds.size.x / 2;

            float currentAngle = Vector3.SignedAngle(Vector3.up, collision.rigidbody.velocity, Vector3.forward);
            float bounceAngle = (offset / width) * MaxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -MaxBounceAngle, MaxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            collision.rigidbody.velocity = rotation * Vector3.up * collision.rigidbody.velocity.magnitude;
        }
    }

}
