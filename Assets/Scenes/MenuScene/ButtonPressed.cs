using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPressed : MonoBehaviour
{

    private void OnMouseUpAsButton()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

}
