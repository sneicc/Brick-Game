using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TOPBAR_MAINBUTTON : MonoBehaviour
{

    public Animator animation;

    public Button MainButton;

    public Sprite MainButtonSpriteMENU;
    public Sprite MainButtonSpriteCROSS;

    private Scene _currentScene;

    private static bool ButtonSprite = true;

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

        animation.SetTrigger("StartAnimation");
        StartCoroutine(AnimationCourutine());

        if (DATA_HOLDER.currentScene.buildIndex == 1 && DATA_HOLDER.IsMagazineMain == false)
        {
            SceneManager.LoadScene("MAGAZINE", LoadSceneMode.Single);
            SceneManager.LoadScene("TOPBAR", LoadSceneMode.Additive);
        }
        else if (DATA_HOLDER.currentScene.buildIndex == 1)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
            ButtonSprite = true;
        }
        else if(DATA_HOLDER.currentScene.buildIndex == 0)
        {
            SceneManager.LoadScene("MAGAZINE", LoadSceneMode.Single);
            SceneManager.LoadScene("TOPBAR", LoadSceneMode.Additive);
            ButtonSprite = false;
        }
    }

    IEnumerator AnimationCourutine()
    {
        yield return new WaitForSeconds(10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
