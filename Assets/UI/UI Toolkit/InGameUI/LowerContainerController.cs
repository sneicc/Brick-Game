using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LowerContainerController : MonoBehaviour
{

    private VisualElement LowerContainer;

    void Start()
    {
        LowerContainer = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("LowerContainer");

        Button ButtonOne = LowerContainer.Q<Button>("ButtonOne");
        Button ButtonTwo = LowerContainer.Q<Button>("ButtonTwo");
        Button ButtonThree = LowerContainer.Q<Button>("ButtonThree");
        Button ButtonFour = LowerContainer.Q<Button>("ButtonFour");

        ButtonOne.clicked += delegate { { OnButtonOneClick(); } };
        ButtonTwo.clicked += delegate { { OnButtonTwoClick(); } };
        ButtonThree.clicked += delegate { { OnButtonThreeClick(); } };
        ButtonFour.clicked += delegate { { OnButtonFourClick(); } };
    }

    private void OnButtonFourClick()
    {
        Debug.Log("Clicked!");
    }

    private void OnButtonThreeClick()
    {
        Debug.Log("Clicked!");
    }

    private void OnButtonTwoClick()
    {
        Debug.Log("Clicked!");
    }

    private void OnButtonOneClick()
    {
        Debug.Log("Clicked!");
    }
}
