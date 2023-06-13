using UnityEngine;

public class SpeedMod : MonoBehaviour
{
    public float Speed  = 7;
    public float Duration = 3;
    private Vector3 AddedSpeed;

    private Ball2D _ball;

    private bool IsUsed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GameBall") && !IsUsed)
        {           
            IsUsed = true;

            _ball = collision.gameObject.GetComponent<Ball2D>();
            SpeedBooster.AddSpeed(_ball, Speed);

            gameObject.SetActive(false);
            Invoke(nameof(RemoveMod), Duration);
            
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
