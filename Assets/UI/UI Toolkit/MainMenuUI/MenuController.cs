using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


[RequireComponent(typeof(UIDocument))]

public class MenuController : MonoBehaviour
{

    private VisualElement TopBarVisElement;
    private VisualElement SettingsVisualElement;
    private VisualElement MenuContainer;




    private bool IsOpen = false;
    private bool IsSettingsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        TopBarVisElement = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("TopBarContainer");
        MenuContainer = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("MenuContainer");
        SettingsVisualElement = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("SettingsContainer");


        Button MainButton = TopBarVisElement.Q<Button>("MainButton");

        Button ExitButton = MenuContainer.Q<Button>("ExitButton");
        Button MagazineButton = MenuContainer.Q<Button>("MagazineButton");

        Button SettingsButton = SettingsVisualElement.Q<Button>("SettingsButton");
        Button MusicButton = SettingsVisualElement.Q<Button>("MusicButton");
        Button SoundButton = SettingsVisualElement.Q<Button>("SoundButton");


        MainButton.clicked += delegate { OnClick(); };
        MagazineButton.clicked += delegate { OnMagazineClick(); };
        SettingsButton.clicked += delegate { OnSettingClick(); };
        MusicButton.clicked += delegate { { OnMusicClick(); } };
        SoundButton.clicked += delegate { { OnSoundClick(); } };
        ExitButton.clicked += delegate { { OnExitClick(); } };

        MenuContainer.Q("SettingsButton").style.display = DisplayStyle.None;
        MenuContainer.Q("ExitButton").style.display = DisplayStyle.None;
        MenuContainer.Q("MagazineButton").style.display = DisplayStyle.None;
        MusicButton.style.display= DisplayStyle.None;
        SoundButton.style.display = DisplayStyle.None;

        Debug.Log("TopBarVE / SettingsVE child count");
        Debug.Log(TopBarVisElement.childCount);
        Debug.Log(SettingsVisualElement.childCount);


    }



    private void OnClick()
    {
        if (!IsOpen)
        {
            SettingsVisualElement.Q("SettingsButton").style.display = DisplayStyle.Flex;
            MenuContainer.Q("ExitButton").style.display = DisplayStyle.Flex;
            MenuContainer.Q("MagazineButton").style.display = DisplayStyle.Flex;
            IsOpen = true;
        }
        else
        {
            SettingsVisualElement.Q("SettingsButton").style.display = DisplayStyle.None;
            MenuContainer.Q("ExitButton").style.display = DisplayStyle.None;
            MenuContainer.Q("MagazineButton").style.display = DisplayStyle.None;
            IsOpen = false;
            IsSettingsOpen= true;
            OnSettingClick();
        }
    }

    private void OnSettingClick()
    {
        if (!IsSettingsOpen)
        {

            SettingsVisualElement.Q("MusicButton").style.display = DisplayStyle.Flex;
            SettingsVisualElement.Q("SoundButton").style.display = DisplayStyle.Flex;
            IsSettingsOpen = true;
        }
        else
        {
            SettingsVisualElement.Q("MusicButton").style.display = DisplayStyle.None;
            SettingsVisualElement.Q("SoundButton").style.display = DisplayStyle.None;
            IsSettingsOpen = false;
        }
    }

    private void OnMagazineClick()
    {
        throw new NotImplementedException();
    }

    private void OnSoundClick()
    {
        throw new NotImplementedException();
    }

    private void OnMusicClick()
    {
        throw new NotImplementedException();
    }

    private void OnExitClick()
    {
        throw new NotImplementedException();
    }

}
