using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MagazineManager : MonoBehaviour
{
    public Canvas Skins;
    public Canvas Updates;
    public Canvas main;

    public Button ButtonUR;
    public Button ButtonUL;
    public Button ButtonMid;
    public Button ButtonLow;
    public Button ButtonMAIN;

    public TextMeshProUGUI COINS_COUNT;
    public TextMeshProUGUI DIAMOND_COUNT;


    public Button PowerUPButton;
    public TextMeshProUGUI PowerUpLVLText;

    private int count;


    void Start()
    {
        Skins.enabled = false;
        Updates.enabled = false;

        ButtonUR.onClick.AddListener(UR);
        ButtonUL.onClick.AddListener(UL);
        ButtonMid.onClick.AddListener(Mid);
        ButtonLow.onClick.AddListener(Low);
        ButtonMAIN.onClick.AddListener(Main);

        PowerUPButton.onClick.AddListener(PowerUP);


        COINS_COUNT.text = "9999";
        DIAMOND_COUNT.text = "9999";
        count = 0;
    }

    void PowerUP()
    {
        count++;
        PowerUpLVLText.text = $"{count}";
    }

    void UR()
    {
        main.enabled = false;
        Updates.enabled = true;
    }

    void UL()
    {
        main.enabled = false;
        Skins.enabled = true;
    }

    void Mid()
    {

    }

    void Low()
    {

    }

    void Main()
    {
        main.enabled = true;
        Updates.enabled = false;
        Skins.enabled = false;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse4))
        {
            main.enabled = true;
            Updates.enabled = false;
            Skins.enabled = false;
        }




    }
}
