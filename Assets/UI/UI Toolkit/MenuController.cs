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

    private VisualElement VisualElement;
    private VisualElement SettingsVisualElement;




    private bool IsOpen = false;
    private bool IsSettingsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        VisualElement = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("MenuContainer");
        SettingsVisualElement = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("SettingsContainer");



        Button SettingsButton = SettingsVisualElement.Q<Button>("SettingsButton");
        Button MusicButton = SettingsVisualElement.Q<Button>("MusicButton");
        Button SoundButton = SettingsVisualElement.Q<Button>("SoundButton");



        SettingsButton.clicked += delegate { OnSettingClick(); };

        Button MainButton = VisualElement.Q<Button>("MainButton");
        MainButton.clicked += delegate { OnClick(); };

        SettingsVisualElement.Q("SettingsButton").style.display = DisplayStyle.None;
        VisualElement.Q("ExitButton").style.display = DisplayStyle.None;
        MusicButton.style.display= DisplayStyle.None;
        SoundButton.style.display = DisplayStyle.None;


        Debug.Log(VisualElement.childCount);

    }



    private void OnClick()
    {
        if (!IsOpen)
        {
            SettingsVisualElement.Q("SettingsButton").style.display = DisplayStyle.Flex;
            VisualElement.Q("ExitButton").style.display = DisplayStyle.Flex;
            IsOpen = true;
        }
        else
        {
            SettingsVisualElement.Q("SettingsButton").style.display = DisplayStyle.None;
            VisualElement.Q("ExitButton").style.display = DisplayStyle.None;
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

}
