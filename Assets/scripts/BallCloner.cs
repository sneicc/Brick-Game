using UnityEngine;

public static class BallCloner
{
    public static GameObject CreateClone(GameObject gameObject)
    {
        Vector3 position = GetClonePosition(gameObject.transform.position, gameObject.GetComponent<CircleCollider2D>().radius, gameObject.transform.localScale);
        var clone = GameObject.Instantiate(gameObject, position, gameObject.transform.rotation);

        clone.gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        var ball = clone.gameObject.GetComponent<Ball2D>();
        ball.BounceSpeed = GameManager.Speed;
        ball.SpeedModCounter = 0;
        ball.IsClone = true;
        ball.IsImmortal = false;
        clone.GetComponent<Renderer>().material = GameObject.Instantiate(ball.CloneMaterial);

        return clone;
    }

    private static Vector3 GetClonePosition(Vector3 originalPosition, float radius, Vector3 scale)
    {
        float offset = radius * scale.x * 2 + 0.05f;
        float trueRadius = radius * scale.x;

        Vector3 left = new Vector3(originalPosition.x - offset, originalPosition.y, originalPosition.z);
        Vector3 right = new Vector3(originalPosition.x + offset, originalPosition.y, originalPosition.z);
        Vector3 up = new Vector3(originalPosition.x, originalPosition.y + offset, originalPosition.z);
        Vector3 down = new Vector3(originalPosition.x, originalPosition.y - offset, originalPosition.z);

        Collider2D[] collidersLeft = Physics2D.OverlapCircleAll(left, trueRadius);
        Collider2D[] collidersRight = Physics2D.OverlapCircleAll(right, trueRadius);
        Collider2D[] collidersDown = Physics2D.OverlapCircleAll(down, trueRadius);
        Collider2D[] collidersUp = Physics2D.OverlapCircleAll(up, trueRadius);

        if (collidersLeft.Length == 0)
        {
            return left;
        }
        else if (collidersRight.Length == 0)
        {
            return right;
        }
        else if (collidersDown.Length == 0)
        {
            return down;
        }
        else if (collidersUp.Length == 0)
        {
            return up;
        }
        else
        {
            return originalPosition;
        }
    }
}
