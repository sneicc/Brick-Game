using UnityEngine;

public class DoublingMod : MonoBehaviour
{
    [SerializeField]
    private Gradient TrailColor;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GameBall"))
        {
            BallCloner.CreateClone(collision.gameObject, TrailColor);

            Destroy(gameObject);
        }
    }
}
