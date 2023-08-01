using UnityEngine;

public static class DamageDealer
{
    public static void DealDamage(int damage, Collider2D brickCollider, Vector2 direction)
    {
        Brick2D brick = GetBrickFromCollider(brickCollider);

        brick.Hit(damage, direction);
    }

    public static void DealDamage(int damage, Collider2D brickCollider)
    {
        Brick2D brick = GetBrickFromCollider(brickCollider);

        brick.Hit(damage);
    }

    private static Brick2D GetBrickFromCollider(Collider2D brickCollider)
    {
        Brick2D brick;
        Transform parent = brickCollider.transform.parent;
        if (parent != null && parent.gameObject.CompareTag("ExternalBrick"))
        {
            brick = parent.gameObject.GetComponent<Brick2D>();
        }
        else
        {
            brick = brickCollider.GetComponent<Brick2D>();
        }

        return brick;
    }
}
