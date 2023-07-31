using UnityEngine;

[RequireComponent(typeof(IPickable))]
public class Picker : MonoBehaviour
{
    public PickableType ObjectType;
    private IPickable _pickable;
    public bool IsPicked { get; private set; }

    private void OnEnable()
    {
        IsPicked = false;
        _pickable = gameObject.GetComponent<IPickable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnPickUpAction(collision);
    }

    protected virtual void OnPickUpAction(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ObjectType.ToString()))
        {
            _pickable.PickUp();
            IsPicked = true;
        }
    }

    public enum PickableType
    {
        GameBall,
        Platform
    }
}
