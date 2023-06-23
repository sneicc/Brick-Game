using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopManager : MonoBehaviour, ISaveable
{
    [SerializeField]
    private Button[] _buttons;
    [SerializeField]
    private int[] _prices;
    private bool[] _isBought;

    private void Start()
    {
        if (_isBought == null || _isBought.Length != _buttons.Length)
        {
            _isBought = new bool[_buttons.Length];
        }

        for (int i = 0; i < _isBought.Length; i++)
        {
            if (_isBought[i] == true)
            {
                Button button = _buttons[i];
                int skinIndex = i;
                button.onClick.AddListener(() => EquipSkin(skinIndex, button));
                SetEquipedStatus(button);
            }
            else
            {
                BuySkin(i);
            }
        }
    }

    private void EquipSkin(int skinIndex, Button button)
    {
        int currentIndex = SkinManager.Instance.SkinIndex;
        RemoveEquipedStatus(currentIndex);

        SetEquipedStatus(button);
        SkinManager.Instance.SetSkin(skinIndex);
    }

    private void RemoveEquipedStatus(int currentIndex)
    {
        _buttons[currentIndex].enabled = true;
        _buttons[currentIndex].GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
    }

    private static void SetEquipedStatus(Button button)
    {
        button.enabled = false;
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Equiped";
    }

    private void BuySkin(int skinIndex)
    {
        if (GameManager.RemoveCoins(_prices[skinIndex]))
        {
            Button button = _buttons[skinIndex];

            EquipSkin(skinIndex, button);
            _isBought[skinIndex] = true;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => EquipSkin(skinIndex, button));
        }
    }

    public void Save(SaveData saveData)
    {
        saveData.BoughtSkins = _isBought;
    }

    public void Load(SaveData saveData)
    {
        _isBought = saveData.BoughtSkins;
    }
}
