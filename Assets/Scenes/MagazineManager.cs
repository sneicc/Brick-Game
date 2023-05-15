using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagazineManager : MonoBehaviour
{
    private const int _bonusOffset = 1;

    public Canvas Skins;
    public Canvas Upgrades;
    public Canvas CurrencyShop;
    public Canvas Skills;
    public Canvas Settings;
    public Canvas main;

    public Image[] DamageSprites;
    public Image[] DamageModSprites;
    public Image[] SpeedModSprites;
    public Image[] BallDoublerSprites;
    public Image[] ExplosionSprites;
    public Image[] PlatformSizeSprites;
    public Image[] PlatformSpeedSprites;


    public Button ButtonUpgrades;
    public Button ButtonSkins;
    public Button ButtonSkills;
    public Button ButtonCurrencyShop;
    public Button ButtonSettings;

    //----------------
    public Button BallDamageUpgrade;
    public Button DamageMultiplier;
    public Button SpeedMultiplier;
    public Button BallDoubler;
    public Button Exploson;
    public Button PlatformSize;
    public Button PlatformSpeed;
    //----------------

    //Upgrade text
    //----------------
    public TextMeshProUGUI BallDamagePrice;
    public TextMeshProUGUI DamageMultiplierPrice;
    public TextMeshProUGUI SpeedMultiplierPrice;
    public TextMeshProUGUI BallDoublerPrice;
    public TextMeshProUGUI ExplosionPrice;
    public TextMeshProUGUI PlatformSizePrice;
    public TextMeshProUGUI PlatformSpeedPrice;

    public TextMeshProUGUI[] BallDamageBonus;
    public TextMeshProUGUI[] DamageMultiplierBonus;
    public TextMeshProUGUI[] SpeedMultiplierBonus;
    public TextMeshProUGUI[] BallDoublerBonus;
    public TextMeshProUGUI[] ExplosionBonus;
    public TextMeshProUGUI[] PlatformSizeBonus;
    public TextMeshProUGUI[] PlatformSpeedBonus;

    //----------------

    //TEST
    private int count;


    private void Awake()
    {
        ButtonUpgrades.onClick.AddListener(OnUpgrades);
        ButtonSkins.onClick.AddListener(OnSkins);
        ButtonSkills.onClick.AddListener(OnSkills);
        ButtonCurrencyShop.onClick.AddListener(OnCurrencyShop);
        ButtonSettings.onClick.AddListener(OnSettings);

        BallDamageUpgrade.onClick.AddListener(OnDamageUpgrade);
        DamageMultiplier.onClick.AddListener(OnDamageMultiplierUpgrade);
        SpeedMultiplier.onClick.AddListener(OnSpeedMultiplierUpgrade);
        BallDoubler.onClick.AddListener(OnDoublerUpgrade);
        Exploson.onClick.AddListener(OnExplosionUpgrade);
        PlatformSize.onClick.AddListener(OnPlatformSizeUpgrade);
        PlatformSpeed.onClick.AddListener(OnPlatformSpeedUpgrade);
    }

    private void OnPlatformSpeedUpgrade()
    {
        throw new NotImplementedException();
    }

    private void OnPlatformSizeUpgrade()
    {
        throw new NotImplementedException();
    }

    private void OnExplosionUpgrade()
    {
        throw new NotImplementedException();
    }

    private void OnDoublerUpgrade()
    {
        throw new NotImplementedException();
    }

    private void OnSpeedMultiplierUpgrade()
    {
        throw new NotImplementedException();
    }

    private void OnDamageMultiplierUpgrade()
    {
        DamageModifier.Instance.Upgrade();
        DamageMultiplierPrice.text = DamageModifier.Instance.NextUpgradePrice.ToString();
        //DamageMultiplierBonus.text = DamageModifier.Instance.NextUpgradeBonus.ToString();

        for (int i = 0; i < DamageModifier.Instance.CurrentUpgradeIndex; i++)
        {
            DamageModSprites[i].enabled = true;
        }
    }

    private void OnDamageUpgrade()
    {
        Debug.Log("work");
    }

    void Start()
    {

        //for (int i = 0; i < DamageSprites.Length; i++)
        //{
        //    DamageSprites[i].enabled = false;
        //    DamageModSprites[i].enabled = false;
        //    SpeedModSprites[i].enabled = false;
        //    BallDoublerSprites[i].enabled = false;
        //    ExplosionSprites[i].enabled = false;
        //    PlatformSizeSprites[i].enabled = false;
        //    PlatformSpeedSprites[i].enabled = false;
        //}

        SetSpritesBonusesAndPrice(DamageModifier.Instance, DamageModSprites, DamageMultiplierBonus, DamageMultiplierPrice);
        SetSpritesBonusesAndPrice(SpeedAndImmortalModifier.Instance, SpeedModSprites, SpeedMultiplierBonus, SpeedMultiplierPrice);
        SetSpritesBonusesAndPrice(DoublingAllBalls.Instance, BallDoublerSprites, BallDoublerBonus, BallDoublerPrice);
        SetSpritesBonusesAndPrice(Explosion.Instance, ExplosionSprites, ExplosionBonus, ExplosionPrice);

        //добавить остальные

        DATA_HOLDER.IsMagazineMain = true;
    }

    private void SetSpritesBonusesAndPrice(Modifier modifier, Image[] sprites,TextMeshProUGUI[] bonusText, TextMeshProUGUI price)
    {
        var bonuses = modifier.UpgradeBonuses;
        for (int i = 0; i < modifier.CurrentUpgradeIndex; i++)
        {
            sprites[i].enabled = true;
        }
        for (int i = 0; i < bonusText.Length; i++)
        {
            bonusText[i].text = '+' + bonuses[i + _bonusOffset].ToString();
        }
        price.text = modifier.NextUpgradePrice.ToString();
    }

    private void OnTestClickedModifier()
    {
        Debug.Log("PASSED");
    }

    public void PowerUP()
    {
        count++;
        //PowerUpLVLText.text = $"{count}";
    }

    public void OnUpgrades()
    {
        main.enabled = false;
        Upgrades.gameObject.SetActive(true);
        //ButtonMAIN.image.sprite = MainButtonSpriteCROSS;
        DATA_HOLDER.IsMagazineMain = false;
    }

    public void OnSkins()
    {
        main.enabled = false;
        Skins.gameObject.SetActive(true);
        // ButtonMAIN.image.sprite = MainButtonSpriteCROSS;
        DATA_HOLDER.IsMagazineMain = false;

    }

    public void OnSkills()
    {
        Debug.Log("IM SKILLS BUTTON!!!");
        DATA_HOLDER.IsMagazineMain = false;
    }

    public void OnCurrencyShop()
    {
        main.enabled = false;
        CurrencyShop.gameObject.SetActive(true);
        DATA_HOLDER.IsMagazineMain = false;
    }

    public void BackToMainMagazine()
    {
        main.enabled = true;
        Upgrades.gameObject.SetActive(false);
        Skins.gameObject.SetActive(false);
        // ButtonMAIN.image.sprite = MainButtonSpriteMENU;
        DATA_HOLDER.IsMagazineMain = true;
    }

    public void OnSettings()
    {
        Debug.Log("IM SETTINGS BUTTON!!!");
        DATA_HOLDER.IsMagazineMain = false;
    }

    private void OnDestroy()
    {
        ButtonUpgrades.onClick.RemoveAllListeners();
        ButtonSkins.onClick.RemoveAllListeners();
        ButtonSkills.onClick.RemoveAllListeners();
        ButtonCurrencyShop.onClick.RemoveAllListeners();
    }
}
