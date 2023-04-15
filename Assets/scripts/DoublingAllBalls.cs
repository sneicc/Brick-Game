using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoublingAllBalls : Modifier, IModifier
{
    public static DoublingAllBalls Instance;

    public int DoublingTime = 5;
    public int DoublingAmount = 10;
    public int DoublingPrice = 10;

    public float[] DoublingUpgrade = { 2, 2.5f, 3, 3.5f, 4 };
    public int[] DoublingUpgradePrice = { 100, 350, 700, 1200, 1900 };
    public int DoublingIndex = 0;

    private static GameObject[] Clones;

    void Awake()
    {
        WorkingTime = DoublingTime;
        Amount = DoublingAmount;
        Price = DoublingPrice;

        UpgradeBonus = DoublingUpgrade;
        UpgradePrice = DoublingUpgradePrice;
        UpgradeIndex = DoublingIndex;

        Instance = this;
    }

    public void Activate()
    {
        if (Spend())
        {
            int size = GameManager.Balls.Count;
            Clones = new GameObject[size];

            for (int i = 0; i < size; i++)
            {
                Clones[i] = CreateClone(GameManager.Balls[i].gameObject);
            }         
        }
    }
    public void Disable()
    {
        foreach (var ball in Clones)
        {
            if (!ReferenceEquals(ball, null)) Destroy(ball.gameObject);
        }
    }
    private GameObject CreateClone(GameObject gameObject)
    {
        Vector3 position = GetClonePosition(gameObject.transform.position, gameObject.GetComponent<SphereCollider>().radius, gameObject.transform.localScale);
        var clone = Instantiate(gameObject, position, gameObject.transform.rotation);

        clone.gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
        var ballB = clone.gameObject.GetComponent<BallB>();
        ballB.BounceSpeed = GameManager.Speed;
        ballB.SpeedModCounter = 0;
        ballB.IsClone = true;
        ballB.IsImmortal = false;

        return clone;
    }

    private Vector3 GetClonePosition(Vector3 originalPosition, float radius, Vector3 scale)
    {
        float offset = radius * scale.x * 2 + 0.05f;
        float trueRadius = radius * scale.x;

        Vector3 left = new Vector3(originalPosition.x - offset, originalPosition.y, originalPosition.z);
        Vector3 right = new Vector3(originalPosition.x + offset, originalPosition.y, originalPosition.z);
        Vector3 up = new Vector3(originalPosition.x, originalPosition.y + offset, originalPosition.z);
        Vector3 down = new Vector3(originalPosition.x, originalPosition.y - offset, originalPosition.z);

        if (!Physics.CheckSphere(left, trueRadius)) return left;
        else if (!Physics.CheckSphere(right, trueRadius)) return right;
        else if (!Physics.CheckSphere(down, trueRadius)) return down;
        else if (!Physics.CheckSphere(up, trueRadius)) return up;
        else return originalPosition;
    }
}