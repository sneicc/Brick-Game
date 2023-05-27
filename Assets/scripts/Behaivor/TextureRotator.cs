using UnityEngine;

public class TextureRotator : MonoBehaviour
{
    public float rotationSpeed = 1f;
    private Material _material;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        Vector2 direction = new Vector2(_rb.velocity.x, _rb.velocity.y).normalized;

        // Применяем вращение к текстуре, умножая на скорость вращения
        _material.mainTextureOffset += direction * rotationSpeed * Time.deltaTime;
    }
}
