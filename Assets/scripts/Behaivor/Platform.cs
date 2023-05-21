using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Platform : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public float Speed = 10;

    private Collider _collider;
    private float _halfScreenWidth;

    public float MaxBounceAngle = 80;

    private Vector3 _targetPosition;
    public bool _isMoving;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
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

            _targetPosition = Camera.main.ScreenToWorldPoint(touch.position);
            _targetPosition = new Vector3(_targetPosition.x, transform.position.y, transform.position.z);

            if (touch.phase == TouchPhase.Began) _isMoving = true;      
        }
        else
        {
            _isMoving = false;
        }
    }

    private void FixedUpdate()
    {

        if (_isMoving)
        {
            Vector3 direction = (_targetPosition - transform.position).normalized;

            if (Vector3.Distance(transform.position, _targetPosition) > 0.05f)
            {
                _rigidbody.velocity = direction * Speed;
            }
            else
            {
                _rigidbody.velocity = Vector3.zero;
            }
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }

        
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
