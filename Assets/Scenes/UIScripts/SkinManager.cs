using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Ball;
    [SerializeField]
    private Material[] Materials;

    private Material _material;

    private void Awake()
    {
        Ball.GetComponent<Renderer>().material = _material;
    }

    public void SetSkin(int i)
    {
        if(i >= 0 && i < Materials.Length)
        {
            _material = Materials[i];
        }
    }
}
