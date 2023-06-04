using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaimondManager : MonoBehaviour
{
    private Daimond[] _daimonds;
    private void Awake()
    {
        GameManager.GameWin += OnGameWin;
        _daimonds = FindObjectsOfType<Daimond>();
        LoadDiamonds();
    }

    private void OnGameWin(int _)
    {
        SaveDaimonds();
    }

    private void LoadDiamonds()
    {
        foreach (var daimond in _daimonds)
        {
            bool isCollected = PlayerPrefs.GetInt(daimond.gameObject.name, 0) == 1;
            if(isCollected) Destroy(daimond.gameObject);
        }
    }

    private void SaveDaimonds()
    {
        foreach (var daimond in _daimonds)
        {
            int isCollected = daimond.IsCollected ? 1 : 0;
            PlayerPrefs.SetInt(daimond.gameObject.name, isCollected);
        }

        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        GameManager.GameWin -= OnGameWin;
    }
}
