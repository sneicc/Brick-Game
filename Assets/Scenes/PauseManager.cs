using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private Button ExitButton;
    [SerializeField]
    private Canvas PauseCanvas;
    private void Awake()
    {
        PauseCanvas.gameObject.SetActive(false);
        LevelUIController.Instance.PauseButton.onClick.AddListener(OpenPauseMenu);
        ExitButton.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        GameManager.LoadMainMenu();
    }

    private void OpenPauseMenu()
    {
        GameManager.PauseGame();
        PauseCanvas.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        LevelUIController.Instance.PauseButton.onClick.RemoveListener(OpenPauseMenu);
    }
}
