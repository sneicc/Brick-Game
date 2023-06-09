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

    private RemoveCoinsStrategy _removeCoinsStrategy = new RemoveCoinsStrategy();
    private RemoveDaimondsStrategy _removeDaimondsStrategy = new RemoveDaimondsStrategy();

    private void Awake()
    {
        BallDamageUpgrade.onClick.AddListener(OnDamageUpgrade);
        DamageMultiplier.onClick.AddListener(OnDamageMultiplierUpgrade);
        SpeedMultiplier.onClick.AddListener(OnSpeedMultiplierUpgrade);
        BallDoubler.onClick.AddListener(OnDoublerUpgrade);
        Explosion.onClick.AddListener(OnExplosionUpgrade);
        PlatformSpeed.onClick.AddListener(OnPlatformSpeedUpgrade);
    }

    private void Start()
    {
        SetSpritesBonusesAndPrice(DamageModifier.Instance, DamageModSprites, DamageMultiplierBonus, DamageMultiplierPrice);
        SetSpritesBonusesAndPrice(SpeedAndImmortalModifier.Instance, SpeedModSprites, SpeedMultiplierBonus, SpeedMultiplierPrice);
        SetSpritesBonusesAndPrice(DoublingAllBalls.Instance, BallDoublerSprites, BallDoublerBonus, BallDoublerPrice);
        SetSpritesBonusesAndPrice(global::ExplosionModifier.Instance, ExplosionSprites, ExplosionBonus, ExplosionPrice);
        SetSpritesBonusesAndPrice(BallDamageManager.Instance, DamageSprites, BallDamageBonus, BallDamagePrice);
        SetSpritesBonusesAndPrice(PlatformSpeedManager.Instance, PlatformSpeedSprites, PlatformSpeedBonus, PlatformSpeedPrice);

        int value = 20;
        for (int i = 0; i < PlatformSpeedBonus.Length; i++)
        {
            PlatformSpeedBonus[i].text = value.ToString();
            value += 20;
        }
    }

    private void OnDamageUpgrade()
    {
        Upgrade(BallDamageManager.Instance, DamageSprites, BallDamagePrice, _removeDaimondsStrategy);
    }

    private void OnPlatformSpeedUpgrade()
    {
        Upgrade(PlatformSpeedManager.Instance, PlatformSpeedSprites, PlatformSpeedPrice, _removeDaimondsStrategy);
    }

    private void OnExplosionUpgrade()
    {
        Upgrade(global::ExplosionModifier.Instance, ExplosionSprites, ExplosionPrice, _removeCoinsStrategy);
    }

    private void OnDoublerUpgrade()
    {
        Upgrade(DoublingAllBalls.Instance, BallDoublerSprites, BallDoublerPrice, _removeCoinsStrategy);
    }

    private void OnSpeedMultiplierUpgrade()
    {
        Upgrade(SpeedAndImmortalModifier.Instance, SpeedModSprites, SpeedMultiplierPrice, _removeCoinsStrategy);
    }

    private void OnDamageMultiplierUpgrade()
    {
        Upgrade(DamageModifier.Instance, DamageModSprites, DamageMultiplierPrice, _removeCoinsStrategy);
    }

    private void Upgrade(Upgradable modifier, Image[] sprites, TextMeshProUGUI price, IResourceRemovalStrategy removalStrategy)
    {
        modifier.Upgrade(removalStrategy);
        price.text = modifier.NextUpgradePrice.ToString();

        SetSprites(modifier, sprites);
    }

    private void SetSpritesBonusesAndPrice(Upgradable modifier, Image[] sprites, TextMeshProUGUI[] bonusText, TextMeshProUGUI price)
    {
        SetSprites(modifier, sprites);

        var bonuses = modifier.UpgradeBonuses;
        for (int i = 0; i < bonusText.Length; i++)
        {
            bonusText[i].text = bonuses[i + _bonusOffset].ToString();
        }

        int currentPrice = modifier.NextUpgradePrice;
        if(currentPrice <= 0)
        {
            price.text = "MAX";
        }
        else
        {
            price.text = currentPrice.ToString();
        }
        
    }

    private static void SetSprites(Upgradable modifier, Image[] sprites)
    {
        for (int i = 0; i < modifier.CurrentUpgradeIndex; i++)
        {
            sprites[i].enabled = true;
        }
    }
}
