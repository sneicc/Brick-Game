using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    [SerializeField]
    private Button OpenInfoButton;
    [SerializeField]
    private Button CloseInfoButton;

    [SerializeField]
    private Canvas InfoCanvas;
    private void Awake()
    {
        //InfoCanvas.gameObject.SetActive(false);
        OpenInfoButton.onClick.AddListener(OpenInfo);
        //CloseInfoButton.onClick.AddListener(CloseInfo);
    }

    private void OpenInfo()
    {
        Debug.Log("I am INFO button :)");
        //InfoCanvas.gameObject.SetActive(true);
    }

    private void CloseInfo()
    {
        InfoCanvas.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        OpenInfoButton.onClick.RemoveAllListeners();
        //CloseInfoButton.onClick.RemoveAllListeners();
    }
}
