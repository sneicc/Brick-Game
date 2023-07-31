using UnityEngine;

public class Loot2D : Picker
{
    [SerializeField]
    private float _speed = 3f;
    private Transform _transform;
    void Start()
    {
        _transform = gameObject.transform;
    }

    protected override void OnPickUpAction(Collider2D collision)
    {
        base.OnPickUpAction(collision);

        if (collision.gameObject.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPicked) return;
        _transform.position = new Vector3(_transform.position.x, _transform.position.y - _speed * Time.deltaTime, _transform.position.z);       
    }
}
