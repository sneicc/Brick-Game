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
        for (int i = 0; i < _daimonds.Length; i++)
        {
            bool isCollected = PlayerPrefs.GetInt(_daimonds[i].gameObject.name, 0) == 1;
            if (isCollected) 
            { 
                Destroy(_daimonds[i].gameObject);
                _daimonds[i] = null;
            }
        }
    }

    private void SaveDaimonds()
    {
        foreach (var daimond in _daimonds)
        {
            if (daimond is null) continue;
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
