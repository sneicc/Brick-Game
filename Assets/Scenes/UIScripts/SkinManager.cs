using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField]
    private Material[] Materials;

    public void SetSkin(int i)
    {
        if(i >= 0 && i < Materials.Length)
        {
            Ball2D.CustomSkin = Materials[i];
        }
    }
}
