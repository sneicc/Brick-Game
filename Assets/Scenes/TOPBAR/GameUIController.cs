using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public static GameUIController Instance;

    protected Animator AnimatorBTN;

    public Button MainButton;

    public Sprite MainButtonSpriteMENU;
    public Sprite MainButtonSpriteCROSS;

    public TextMeshProUGUI COINS_COUNT;
    public TextMeshProUGUI DIAMOND_COUNT;

    protected static bool ButtonSprite = true;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;

        MainButton.onClick.AddListener(MainClick);
        AnimatorBTN = MainButton.GetComponent<Animator>();

        if (ButtonSprite == true)
        {
            MainButton.image.sprite = MainButtonSpriteMENU;
        }
        else
        {
            MainButton.image.sprite = MainButtonSpriteCROSS;
        }

        GameManager.CoinsChanged += OnCoinsChanged;
        GameManager.DiamondChanged += OnDiamondChanged;

        COINS_COUNT.text = GameManager.Coins.ToString();
        DIAMOND_COUNT.text = GameManager.Daimonds.ToString();

        DATA_HOLDER.currentScene = SceneManager.GetActiveScene();
    }

    protected virtual void OnDiamondChanged()
    {
        DIAMOND_COUNT.text = GameManager.Daimonds.ToString();
    }

    protected virtual void OnCoinsChanged()
    {
        COINS_COUNT.text = GameManager.Coins.ToString();
    }

    public void MainClick()
    {
        // 1 - magazine, 0 - levelmap
        //AnimatorBTN.SetTrigger("Test");
        LoadScenes();


        //StartCoroutine(AnimationCourutine());
    }

    IEnumerator AnimationCourutine()
    {
        yield return new WaitForSecondsRealtime(1.2f);

        LoadScenes();
    }

    private static void LoadScenes()
    {

        if (DATA_HOLDER.currentScene.name == "TOPBAR" && DATA_HOLDER.IsMagazineMain == true)
        {
            GameManager.LoadMainMenu();
            ButtonSprite = true;
        }
        else if (DATA_HOLDER.currentScene.name == "LevelMap")
        {
            SceneManager.LoadScene("TOPBAR", LoadSceneMode.Single);
            SceneManager.LoadScene("MAGAZINE", LoadSceneMode.Additive);           
            ButtonSprite = false;
        }
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        GameManager.CoinsChanged -= OnCoinsChanged;
        GameManager.DiamondChanged -= OnDiamondChanged;

        MainButton.onClick.RemoveAllListeners();

        Instance = null;
    }
}
