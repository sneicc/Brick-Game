using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    private const int _bonusOffset = 1;

    [Header("Upgrade Buttons")]
    public Button BallDamageUpgrade;
    public Button DamageMultiplier;
    public Button SpeedMultiplier;
    public Button BallDoubler;
    public Button Explosion;
    public Button PlatformSize;
    public Button PlatformSpeed;

    [Space]

    [Header("Upgrade Prices")]
    public TextMeshProUGUI BallDamagePrice;
    public TextMeshProUGUI DamageMultiplierPrice;
    public TextMeshProUGUI SpeedMultiplierPrice;
    public TextMeshProUGUI BallDoublerPrice;
    public TextMeshProUGUI ExplosionPrice;
    public TextMeshProUGUI PlatformSizePrice;
    public TextMeshProUGUI PlatformSpeedPrice;

    [Space]

    [Header("Upgrade Bonuses")]
    public TextMeshProUGUI[] BallDamageBonus;
    public TextMeshProUGUI[] DamageMultiplierBonus;
    public TextMeshProUGUI[] SpeedMultiplierBonus;
    public TextMeshProUGUI[] BallDoublerBonus;
    public TextMeshProUGUI[] ExplosionBonus;
    public TextMeshProUGUI[] PlatformSizeBonus;
    public TextMeshProUGUI[] PlatformSpeedBonus;

    [Space]

    [Header("Upgrade Sprites")]
    public Image[] DamageSprites;
    public Image[] DamageModSprites;
    public Image[] SpeedModSprites;
    public Image[] BallDoublerSprites;
    public Image[] ExplosionSprites;
    public Image[] PlatformSizeSprites;
    public Image[] PlatformSpeedSprites;

    private void Awake()
    {
        BallDamageUpgrade.onClick.AddListener(OnDamageUpgrade);
        DamageMultiplier.onClick.AddListener(OnDamageMultiplierUpgrade);
        SpeedMultiplier.onClick.AddListener(OnSpeedMultiplierUpgrade);
        BallDoubler.onClick.AddListener(OnDoublerUpgrade);
        Explosion.onClick.AddListener(OnExplosionUpgrade);
        //PlatformSize.onClick.AddListener(OnPlatformSizeUpgrade);
        //PlatformSpeed.onClick.AddListener(OnPlatformSpeedUpgrade);
    }

    private void Start()
    {
        SetSpritesBonusesAndPrice(DamageModifier.Instance, DamageModSprites, DamageMultiplierBonus, DamageMultiplierPrice);
        SetSpritesBonusesAndPrice(SpeedAndImmortalModifier.Instance, SpeedModSprites, SpeedMultiplierBonus, SpeedMultiplierPrice);
        SetSpritesBonusesAndPrice(DoublingAllBalls.Instance, BallDoublerSprites, BallDoublerBonus, BallDoublerPrice);
        SetSpritesBonusesAndPrice(global::Explosion.Instance, ExplosionSprites, ExplosionBonus, ExplosionPrice);
        SetSpritesBonusesAndPrice(BallDamageManager.Instance, DamageSprites, BallDamageBonus, BallDamagePrice);
        //добавить остальные
    }

    private void OnDamageUpgrade()
    {
        Upgrade(BallDamageManager.Instance, DamageSprites, BallDamagePrice);
    }

    private void OnPlatformSpeedUpgrade()
    {
        //throw new NotImplementedException();
    }

    private void OnPlatformSizeUpgrade()
    {
        //throw new NotImplementedException();
    }

    private void OnExplosionUpgrade()
    {
        Upgrade(global::Explosion.Instance, ExplosionSprites, ExplosionPrice);
    }

    private void OnDoublerUpgrade()
    {
        Upgrade(DoublingAllBalls.Instance, BallDoublerSprites, BallDoublerPrice);
    }

    private void OnSpeedMultiplierUpgrade()
    {
        Upgrade(SpeedAndImmortalModifier.Instance, SpeedModSprites, SpeedMultiplierPrice);
    }

    private void OnDamageMultiplierUpgrade()
    {
        Upgrade(DamageModifier.Instance, DamageModSprites, DamageMultiplierPrice);
    }

    private void Upgrade(Upgradable modifier, Image[] images, TextMeshProUGUI price)
    {
        modifier.Upgrade();
        price.text = modifier.NextUpgradePrice.ToString();

        for (int i = 0; i < modifier.CurrentUpgradeIndex; i++)
        {
            images[i].enabled = true;
        }
    }    

    private void SetSpritesBonusesAndPrice(Upgradable modifier, Image[] sprites, TextMeshProUGUI[] bonusText, TextMeshProUGUI price)
    {
        var bonuses = modifier.UpgradeBonuses;
        for (int i = 0; i < modifier.CurrentUpgradeIndex; i++)
        {
            sprites[i].enabled = true;
        }
        for (int i = 0; i < bonusText.Length; i++)
        {
            bonusText[i].text = bonuses[i + _bonusOffset].ToString();
        }
        price.text = modifier.NextUpgradePrice.ToString();
    }
}
