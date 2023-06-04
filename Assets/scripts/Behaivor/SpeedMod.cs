using UnityEngine;

public class SpeedMod : MonoBehaviour
{
    public float Speed  = 7;
    public float Duration = 3;
    private Vector3 AddedSpeed;

    private Ball2D _ball;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GameBall"))
        {
            _ball = collision.gameObject.GetComponent<Ball2D>();

            SpeedBooster.AddSpeed(_ball, Speed);

            Invoke(nameof(RemoveMod), Duration);
            gameObject.SetActive(false);
        }
    }

    private void RemoveMod()
    {
        if (!ReferenceEquals(_ball, null))
        {
            SpeedBooster.RemoveSpeed(_ball, Speed);
        }

        Destroy(gameObject);
    }
}
