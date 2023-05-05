using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Platform : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private Vector3 _direction; 
    public float Speed = 10;

    private Collider _collider;
    private float _halfScreenWidth;

    public float MaxBounceAngle = 80;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _direction = Vector3.zero;

        _collider = GetComponent<Collider>();

        _halfScreenWidth = Screen.width / 2;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            bool uiElementSelected = EventSystem.current.currentSelectedGameObject != null;
            if (uiElementSelected) return;
  
            Touch touch = Input.GetTouch(0);
            float touchX = touch.position.x;

            if (touchX < _halfScreenWidth) _direction = Vector3.left;
            else _direction = Vector3.right;

        }
        else
        {
            _direction = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(_direction * Speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GameBall"))
        {
            Vector3 platformPosition = transform.position;
            Vector3 contactPoint = collision.GetContact(0).point;

            float offset = platformPosition.x - contactPoint.x;
            float width = _collider.bounds.size.x / 2;

            float currentAngle = Vector3.SignedAngle(Vector3.up, collision.rigidbody.velocity, Vector3.forward);
            float bounceAngle = (offset / width) * MaxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -MaxBounceAngle, MaxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            collision.rigidbody.velocity = rotation * Vector3.up * collision.rigidbody.velocity.magnitude;
        }
    }

}
