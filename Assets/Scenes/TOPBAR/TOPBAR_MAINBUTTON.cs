using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TOPBAR_MAINBUTTON : MonoBehaviour
{

    public Button MainButton;

    public Sprite MainButtonSpriteMENU;
    public Sprite MainButtonSpriteCROSS;

    private Scene _currentScene;

    private static bool ButtonSprite = true;


    private void Awake()
    {

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {

        if (ButtonSprite == true)
        {
            MainButton.image.sprite = MainButtonSpriteMENU;
        }
        else
        {
            MainButton.image.sprite = MainButtonSpriteCROSS;
        }

        //MainButton.onClick.AddListener(MainClick);
        DATA_HOLDER.currentScene = SceneManager.GetActiveScene();
    }

    public void MainClick()
    {
        // 1 - magazine, 0 - levelmap


        if (DATA_HOLDER.currentScene.buildIndex == 1 && DATA_HOLDER.IsMagazineMain == false)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
            SceneManager.LoadScene(5, LoadSceneMode.Additive);
        }
        else if (DATA_HOLDER.currentScene.buildIndex == 1)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
            ButtonSprite = true;
        }
        else if(DATA_HOLDER.currentScene.buildIndex == 0)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
            SceneManager.LoadScene(5, LoadSceneMode.Additive);
            ButtonSprite = false;
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
