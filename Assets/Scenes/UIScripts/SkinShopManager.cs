using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopManager : MonoBehaviour
{
    [SerializeField]
    private Button[] _buttons;
    [SerializeField]
    private int[] _prices;

    private void Start()
    {
        var isBought = SkinManager.Instance.IsBought;

        if (isBought == null || isBought.Length != _buttons.Length)
        {
            isBought = new bool[_buttons.Length];
            isBought[0] = true;
            SkinManager.Instance.IsBought = isBought;
        }

        for (int i = 0; i < isBought.Length; i++)
        {
            Button button = _buttons[i];
            int skinIndex = i;

            if (isBought[i] == true)
            {               
                button.onClick.AddListener(() => EquipSkin(skinIndex, button));
                RemoveEquipedStatus(button);
            }
            else
            {
                button.onClick.AddListener(() => BuySkin(skinIndex));                
            }
        }

        int currentIndex = SkinManager.Instance.SkinIndex;
        Button currentEquipedButton = _buttons[currentIndex];
        SetEquipedStatus(currentEquipedButton);
    }

    private void EquipSkin(int skinIndex, Button button)
    {
        int currentIndex = SkinManager.Instance.SkinIndex;
        Button currentEquipedButton = _buttons[currentIndex];
        RemoveEquipedStatus(currentEquipedButton);

        SetEquipedStatus(button);
        SkinManager.Instance.SetSkin(skinIndex);
    }

    private void RemoveEquipedStatus(Button button)
    {
        button.interactable = true;
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
    }

    private static void SetEquipedStatus(Button button)
    {
        button.interactable = false;
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Equiped";
    }

    private void BuySkin(int skinIndex)
    {
        if (GameManager.RemoveCoins(_prices[skinIndex]))
        {
            Button button = _buttons[skinIndex];

            EquipSkin(skinIndex, button);
            SkinManager.Instance.IsBought[skinIndex] = true;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => EquipSkin(skinIndex, button));
        }
    }
}
