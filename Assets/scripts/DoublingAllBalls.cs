using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DoublingAllBalls : Modifier, IModifier
{
    public static DoublingAllBalls Instance;

    [SerializeField]
    private int DoublingTime = 5;
    [SerializeField]
    private int DoublingAmount = 10;
    [SerializeField]
    private int DoublingPrice = 10;

    [SerializeField]
    private float[] DoublingUpgrade = { 1, 2, 2.5f, 3, 3.5f, 4 };
    [SerializeField]
    private int[] DoublingUpgradePrice = { 0 ,100, 350, 700, 1200, 1900 };
    [SerializeField]
    private int DoublingIndex = 0;

    private GameObject[] _clones;

    void Awake()
    {
        if(Instance is not null) Destroy(gameObject);

        WorkingTime = DoublingTime;
        Amount = DoublingAmount;
        Price = DoublingPrice;

        UpgradeBonus = DoublingUpgrade;
        UpgradePrice = DoublingUpgradePrice;
        UpgradeIndex = DoublingIndex;

        Instance = this;
    }

    public override void Activate()
    {
        if (Spend())
        {
            int size = GameManager.Balls.Count;
            _clones = new GameObject[size];

            for (int i = 0; i < size; i++)
            {
                _clones[i] = CreateClone(GameManager.Balls[i].gameObject);
            }         
        }
    }
    public void Disable()
    {
        foreach (var ball in _clones)
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

        clone.GetComponent<Renderer>().material = Instantiate(ballB.CloneMaterial);

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
