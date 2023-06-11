using UnityEngine;
using UnityEngine.UI;

public class SkinButtonConnector : MonoBehaviour
{
    [SerializeField]
    private Button[] Buttons;

    private void Awake()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            int skinIndex = i;
            Buttons[i].onClick.AddListener(() => SkinManager.Instance.SetSkin(skinIndex));
        }
    }
}
