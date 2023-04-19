using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class DATA_HOLDER : MonoBehaviour
{

    DATA_HOLDER instance;
    private void Awake()
    {
        instance = this;
    }

    public static bool IsMagazineMain;
    public static Scene currentScene;
}
