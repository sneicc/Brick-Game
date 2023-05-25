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

    public Button ButtonUpgrades;
    public Button ButtonSkins;
    public Button ButtonSkills;
    public Button ButtonCurrencyShop;
    public Button ButtonSettings;
    //----------------

    private void Awake()
    {
        ButtonUpgrades.onClick.AddListener(OnUpgrades);
        ButtonSkins.onClick.AddListener(OnSkins);
        ButtonSkills.onClick.AddListener(OnSkills);
        ButtonCurrencyShop.onClick.AddListener(OnCurrencyShop);
        ButtonSettings.onClick.AddListener(OnSettings);

        GameUIController.Instance.MainButton.onClick.AddListener(BackToMainMagazine);
    }
   
    void Start()
    {   
        DATA_HOLDER.IsMagazineMain = true;
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
        main.enabled = false;
        Skills.gameObject.SetActive(true);
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
        Settings.gameObject.SetActive(false);
        Skills.gameObject.SetActive(false);
        CurrencyShop.gameObject.SetActive(false);
        // ButtonMAIN.image.sprite = MainButtonSpriteMENU;
        DATA_HOLDER.IsMagazineMain = true;
    }

    public void OnSettings()
    {
        main.enabled = false;
        Settings.gameObject.SetActive(true);
        DATA_HOLDER.IsMagazineMain = false;
    }

    private void OnDestroy()
    {
        ButtonUpgrades.onClick.RemoveAllListeners();
        ButtonSkins.onClick.RemoveAllListeners();
        ButtonSkills.onClick.RemoveAllListeners();
        ButtonCurrencyShop.onClick.RemoveAllListeners();

        GameUIController.Instance.MainButton.onClick.RemoveAllListeners();
    }
}
