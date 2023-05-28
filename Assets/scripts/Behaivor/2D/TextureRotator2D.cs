using UnityEngine;

public class TextureRotator2D : MonoBehaviour
{
    [SerializeField]
    private float RotationSpeed = 1f;
    private Material _material;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        Vector2 direction = _rb.velocity.normalized;

        // Применяем вращение к текстуре, умножая на скорость вращения
        _material.mainTextureOffset += direction * RotationSpeed * Time.deltaTime;
    }
}
