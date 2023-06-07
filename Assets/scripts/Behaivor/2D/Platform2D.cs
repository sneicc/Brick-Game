using UnityEngine;
using UnityEngine.EventSystems;

public class Platform2D : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private float Speed = 10;

    private Collider2D _collider;

    [SerializeField]
    private float MaxBounceAngle = 80;

    private Vector2 _targetPosition;
    private bool _isMoving;
    [SerializeField]
    private float Threshold = 0.1f;

    void Start()
    {
        Speed = PlatformSpeedManager.Instance.Speed;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            bool uiElementSelected = EventSystem.current.currentSelectedGameObject != null;
            if (uiElementSelected) return;

            Touch touch = Input.GetTouch(0);

            _targetPosition = Camera.main.ScreenToWorldPoint(touch.position);
            _targetPosition = new Vector2(_targetPosition.x, transform.position.y);

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
            Vector2 direction = (_targetPosition - (Vector2)transform.position).normalized;
            float distance = Vector2.Distance(transform.position, _targetPosition);

            if (distance > Threshold)
            {
                _rigidbody.velocity = direction * Speed;
            }
            else
            {
                _rigidbody.velocity = Vector2.zero;
            }
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GameBall"))
        {
            Vector2 platformPosition = transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = platformPosition.x - contactPoint.x;
            float width = _collider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, collision.rigidbody.velocity);
            float bounceAngle = (offset / width) * MaxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -MaxBounceAngle, MaxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            collision.rigidbody.velocity = rotation * Vector2.up * collision.rigidbody.velocity.magnitude;
        }
    }
}
