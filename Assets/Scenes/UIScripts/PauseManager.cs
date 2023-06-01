using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private Button ExitButton;
    [SerializeField]
    private Button ResumeButton;
    [SerializeField]
    private Canvas PauseCanvas;
    private void Awake()
    {
        PauseCanvas.gameObject.SetActive(false);
        LevelUIController.Instance.PauseButton.onClick.AddListener(OpenPauseMenu);
        ExitButton.onClick.AddListener(Exit);
        ResumeButton.onClick.AddListener(Resume);
    }

    private void Resume()
    {
        PauseCanvas.gameObject.SetActive(false);
        GameManager.ResumeGame();      
    }

    private void Exit()
    {
        GameManager.RemoveAllListeners();
        GameManager.LoadMainMenu();
    }

    private void OpenPauseMenu()
    {
        PauseCanvas.gameObject.SetActive(true);
        GameManager.PauseGame();      
    }

    private void OnDestroy()
    {
        LevelUIController.Instance?.PauseButton.onClick.RemoveAllListeners();
        ExitButton.onClick.RemoveAllListeners();
        ResumeButton.onClick.RemoveAllListeners();
    }
}
