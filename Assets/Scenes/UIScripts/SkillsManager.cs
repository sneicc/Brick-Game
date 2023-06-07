using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillsManager : MonoBehaviour
{
    [Header("Buy buttons")]
    [SerializeField]
    private Button DamageButton;
    [SerializeField]
    private Button SpeedButton;
    [SerializeField]
    private Button DoublingButton;
    [SerializeField]
    private Button ExplosionButton;

    [Space]

    [Header("Amount text")]
    [SerializeField]
    private TextMeshProUGUI DamageAmount;
    [SerializeField]
    private TextMeshProUGUI SpeedAmount;
    [SerializeField]
    private TextMeshProUGUI DoublingAmount;
    [SerializeField]
    private TextMeshProUGUI ExplosionAmount;

    [Space]

    [Header("Price text")]
    [SerializeField]
    private TextMeshProUGUI DamagePrice;
    [SerializeField]
    private TextMeshProUGUI SpeedPrice;
    [SerializeField]
    private TextMeshProUGUI DoublingPrice;
    [SerializeField]
    private TextMeshProUGUI ExplosionPrice;

    private void Awake()
    {
        DamagePrice.text = DamageModifier.Instance.Price.ToString();
        SpeedPrice.text = SpeedAndImmortalModifier.Instance.Price.ToString();
        DoublingPrice.text = DoublingAllBalls.Instance.Price.ToString();
        ExplosionPrice.text = ExplosionModifier.Instance.Price.ToString();

        SetAmountText(DamageAmount, DamageModifier.Instance);
        SetAmountText(SpeedAmount, SpeedAndImmortalModifier.Instance);
        SetAmountText(DoublingAmount, DoublingAllBalls.Instance);
        SetAmountText(ExplosionAmount, ExplosionModifier.Instance);

        DamageButton.onClick.AddListener(BuyDamage);
        SpeedButton.onClick.AddListener(BuySpeed);
        DoublingButton.onClick.AddListener(BuyDoubling);
        ExplosionButton.onClick.AddListener(BuyExplosion);
    }

    private void SetAmountText(TextMeshProUGUI text, Modifier modifier)
    {
        text.text = modifier.Amount.ToString() + "/10";
    }

    private void BuyDamage()
    {
        DamageModifier.Instance.Buy();
        SetAmountText(DamageAmount, DamageModifier.Instance);
    }

    private void BuySpeed()
    {
        SpeedAndImmortalModifier.Instance.Buy();
        SetAmountText(SpeedAmount, SpeedAndImmortalModifier.Instance);
    }

    private void BuyDoubling()
    {
        DoublingAllBalls.Instance.Buy();
        SetAmountText(DoublingAmount, DoublingAllBalls.Instance);
    }

    private void BuyExplosion()
    {
        ExplosionModifier.Instance.Buy();
        SetAmountText(ExplosionAmount, ExplosionModifier.Instance);
    }

    private void OnDestroy()
    {
        DamageButton.onClick.RemoveAllListeners();
        SpeedButton.onClick.RemoveAllListeners();
        DoublingButton.onClick.RemoveAllListeners();
        ExplosionButton.onClick.RemoveAllListeners();
    }

}
