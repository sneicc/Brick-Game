using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public static Explosion Instance;

    [SerializeField]
    private GameObject ExplosionVFX;
    [SerializeField]
    private float ExplosionRadius;

    private void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;
    }

    public void Explode(Vector3 position, int damage)
    {
        Instantiate(ExplosionVFX, position, Quaternion.Euler(0, 180, 0));
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, ExplosionRadius);
        MakeDamage(colliders, damage);
    }

    private void MakeDamage(Collider2D[] colliders, int damage)
    {
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Brick"))
            {
                if (collider.transform.parent != null) collider.transform.parent.GetComponent<Brick2D>().Hit(damage);
                else collider.GetComponent<Brick2D>().Hit(damage);
            }
        }
    }
}
