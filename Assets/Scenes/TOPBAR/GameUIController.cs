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
        if(Instance is not null) Destroy(gameObject);
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


        //StartCoroutine(AnimationCourutine()); //может убрать ? или сделать анимацию раза в 2 быстрее, а то слишком долго жать надо или вообще поменять анимацию на увеличение размера кнопки а а а  а а а а а а а ???????? ?А?А??А ??А ?А? ?А??А ?А?А? ?А? А?А? ?А? ?А? А??А? А? ?А?А??А? ?А? ?А?А? ?А? ??А?А?А ??А ?А? А? А?? А??А? ?А ?А?А? ?А? А?А??А ?А? ?А? А? А??А? ?А? ?А?А ? А? А?А ?? А? А?А?А ? ?А?А ?? ?А?? А?А ?А?А?А?А?А?А??А?А??А?А?А?А??А?А?А??А?А?А??А?А??А?А?А??А?А?А??А?А?А??А?А?А??А?А??А?А?А?А??А?А??А?А?А?ААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААА  Мертвая змея не шипитНе щебечет дохлый щеголМертвый негр не идет играть в баскетбол Только мертвый негр не идет играть в баскетболАй - я - я - яй! Убили неграУбили негра. УбилиАй - я - я - яй! Ни за что ни про что, суки, замочилиАй - я - я - яй! Убили неграУбили негра. УбилиАй - я - я - яй! Ни за что ни про что, суки, замочилиРуки сложив на животТретий день не ест и не пьетНегр лежит и хип - хоп танцевать не идетТолько мертвый негр хип - хоп танцевать не идетАй - я - я - яй! Убили неграУбили негра. УбилиАй - я - я - яй! Ни за что ни про что,Ай-я - я - яй! Убили неграУбили негра. УбилиАй - я - я - яй! Ни за что ни про что, суки, замочилиА мама осталась однаМама привела колдунаОн ударил в тамтам и Билли встал и пошёлДаже мертвый негр услышал тамтам и пошёлНу и что, что зомбиЗато он встал и пошёлЗомби тоже могут, могут играть в баскетболАй - я - я - яй! Убили неграУбили негра. УбилиАй - я - я - яй! Ни за что ни про что, суки, замочилиАй - я - я - яй убили негра убили а потом отрешили и билиАй - я - я - яй убили негра ай - я - я - яй не за что не про чтоАй - я - я - яй убили негра убили негра убили а потом он встал и пошёл
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
