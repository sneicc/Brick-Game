using UnityEngine;
using UnityEngine.UI;

public class LevelMapControl : MonoBehaviour
{
    public Button[] Buttons;
    [SerializeField]
    private RectTransform _buttonsContainer;
    private float _yPosition;

    private void Awake()
    {
        GameManager.ResumeGame();
    }
    void Start()
    {
        _yPosition = PlayerPrefs.GetFloat("ButtonsContainerY", 0);
        _buttonsContainer.localPosition = new Vector3(0, _yPosition, 0);
        ButtonInitialize();
        CloseLevels();
    }

    private void ButtonInitialize()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Button button = Buttons[i];

            int level = i;
            button.onClick.AddListener(() => GameManager.NewGame(level));
        }
    }

    private void CloseLevels()
    {
        int CurrentOpenedLevel = GameManager.CurrentOpenedLevel;
        for (int i = 0; i < Buttons.Length; i++)
        {
            if(i > CurrentOpenedLevel)
            {
                Buttons[i].interactable = false;
            }
        }
    }

    private void Update()
    {
        _yPosition = _buttonsContainer.localPosition.y;
    }

    private void OnDestroy()
    {
        //PlayerPrefs.SetFloat("ButtonsContainerX", _buttonsContainer.localPosition.x);
        PlayerPrefs.SetFloat("ButtonsContainerY", _yPosition);
        PlayerPrefs.Save();
    }

    public void TestQuit()
    {
        Application.Quit();
    }
}
