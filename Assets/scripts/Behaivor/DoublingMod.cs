using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublingMod : MonoBehaviour
{
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameBall"))
        {
            gameObject.SetActive(false);

            CreateClone(other.gameObject);
        }
    }

    private void CreateClone(GameObject gameObject)
    {
        Vector3 position = GetClonePosition(gameObject.transform.position, gameObject.GetComponent<SphereCollider>().radius, gameObject.transform.localScale);
        var clone = Instantiate(gameObject, position, gameObject.transform.rotation);

        clone.gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
        var ballB = clone.gameObject.GetComponent<Ball>();
        ballB.BounceSpeed = GameManager.Speed;
        ballB.SpeedModCounter = 0;
        ballB.IsClone = true;
        ballB.IsImmortal = false;

        clone.GetComponent<Renderer>().material = Instantiate(ballB.CloneMaterial);
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
