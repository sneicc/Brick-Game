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

        ButtonOne.clicked += OnButtonOneClick;
        ButtonTwo.clicked += OnButtonTwoClick;
        ButtonThree.clicked += OnButtonThreeClick;
        ButtonFour.clicked += OnButtonFourClick;
    }

    private void OnButtonFourClick()
    {
<<<<<<< Updated upstream:Assets/UI/UI Toolkit/InGameUI/LowerContainerController.cs
        throw new NotImplementedException();
=======
        Debug.Log("OnButtonFourClick");
>>>>>>> Stashed changes:Assets/UI/InGameUI/LowerContainerController.cs
    }

    private void OnButtonThreeClick()
    {
<<<<<<< Updated upstream:Assets/UI/UI Toolkit/InGameUI/LowerContainerController.cs
        throw new NotImplementedException();
=======
        Debug.Log("OnButtonThreeClick");
>>>>>>> Stashed changes:Assets/UI/InGameUI/LowerContainerController.cs
    }

    private void OnButtonTwoClick()
    {
<<<<<<< Updated upstream:Assets/UI/UI Toolkit/InGameUI/LowerContainerController.cs
        throw new NotImplementedException();
=======
        Debug.Log("OnButtonTwoClick");
>>>>>>> Stashed changes:Assets/UI/InGameUI/LowerContainerController.cs
    }

    private void OnButtonOneClick()
    {
<<<<<<< Updated upstream:Assets/UI/UI Toolkit/InGameUI/LowerContainerController.cs
        throw new NotImplementedException();
=======
        Debug.Log("OnButtonOneClick");
>>>>>>> Stashed changes:Assets/UI/InGameUI/LowerContainerController.cs
    }
}
