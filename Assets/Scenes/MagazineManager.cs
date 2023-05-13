using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagazineManager : MonoBehaviour
{
    public Canvas Skins;
    public Canvas Upgrades;
    public Canvas CurrencyShop;
    public Canvas Skills;
    public Canvas Settings;
    public Canvas main;

    public GameObject modifier1;
    public Image[] ModifierSprites1;

    public GameObject modifier2;
    public Image[] ModifierSprites2;

    public GameObject modifier3;
    public Image[] ModifierSprites3;

    public GameObject modifier4;
    public Image[] ModifierSprites4;

    public GameObject modifier5;
    public Image[] ModifierSprites5;

    public GameObject modifier6;
    public Image[] ModifierSprites6;

    public GameObject modifier7;
    public Image[] ModifierSprites7;


    public Button ButtonUpgrades;
    public Button ButtonSkins;
    public Button ButtonSkills;
    public Button ButtonCurrencyShop;
    public Button ButtonSettings;
    // public Button ButtonMAIN;

    //public TextMeshProUGUI COINS_COUNT;
    //public TextMeshProUGUI DIAMOND_COUNT;


    //public Button PowerUPButton;
    //public TextMeshProUGUI PowerUpLVLText;

    //public Sprite MainButtonSpriteMENU;
    //public Sprite MainButtonSpriteCROSS;

    //Upgrade buttons
    //----------------
    public Button BallDamageUpgrade;
    public Button DamageMultiplier;
    public Button SpeedMultiplier;
    public Button BallDoubler;
    public Button Exploson;
    public Button PlatformSize;
    public Button PlatformSpeed;
    //----------------

    //TEST
    private int count;


    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);

        modifier1 = new GameObject();
        modifier2 = new GameObject();
        modifier3 = new GameObject();
        modifier4 = new GameObject();
        modifier5 = new GameObject();

        ButtonUpgrades.onClick.AddListener(OnUpgrades);
        ButtonSkins.onClick.AddListener(OnSkins);
        ButtonSkills.onClick.AddListener(OnSkills);
        ButtonCurrencyShop.onClick.AddListener(OnCurrencyShop);
        ButtonSettings.onClick.AddListener(OnSettings);

        //BallDamageUpgrade.onClick.AddListener(OnDamageUpgrade);
        //DamageMultiplier.onClick.AddListener(OnDamageMultiplierUpgrade);
        //SpeedMultiplier.onClick.AddListener(OnSpeedMultiplierUpgrade);
        //BallDoubler.onClick.AddListener(OnDoublerUpgrade);
        //Exploson.onClick.AddListener(OnExplosionUpgrade);
        //PlatformSize.onClick.AddListener(OnPlatformSizeUpgrade);
        //PlatformSpeed.onClick.AddListener(OnPlatformSpeedUpgrade);
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
        throw new NotImplementedException();
    }

    private void OnDamageUpgrade()
    {
        throw new NotImplementedException();
    }

    void Start()
    {

        for (int i = 0; i < ModifierSprites1.Length; i++)
        {
            ModifierSprites1[i].enabled = false;
            ModifierSprites2[i].enabled = false;
            ModifierSprites3[i].enabled = false;
            ModifierSprites4[i].enabled = false;
            ModifierSprites5[i].enabled = false;
        }


        //Button Listeners init


        // ButtonMAIN.onClick.AddListener(Main);

        //PowerUPButton.onClick.AddListener(PowerUP);

        DATA_HOLDER.IsMagazineMain = true;


        //COINS_COUNT.text = "9999";
        //DIAMOND_COUNT.text = "9999";
        count = 0;
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

        //PowerUPButton.onClick.RemoveAllListeners();
    }
}
