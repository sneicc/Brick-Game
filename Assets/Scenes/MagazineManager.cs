using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagazineManager : MonoBehaviour
{
    public Canvas Skins;
    public Canvas Updates;
    public Canvas ValueStore;
    public Canvas main;

    public GameObject modifier1;
    public Image[] ModifierSprites1;

    public GameObject modifier2;
    public Image[] ModifierSprites2;

    public GameObject modifier3;
    public Image[] ModifierSprites3;

    public GameObject modifier4;
    public Image[] ModifierSprites4;

    public GameObject modifier5;
    public Image[] ModifierSprites5;


    public Button ButtonUR;
    public Button ButtonUL;
    public Button ButtonMid;
    public Button ButtonLow;
    // public Button ButtonMAIN;

    //public TextMeshProUGUI COINS_COUNT;
    //public TextMeshProUGUI DIAMOND_COUNT;


    public Button PowerUPButton;
    public TextMeshProUGUI PowerUpLVLText;

    //public Sprite MainButtonSpriteMENU;
    //public Sprite MainButtonSpriteCROSS;


    //TEST
    private int count;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        modifier1 = new GameObject();
        modifier2 = new GameObject();
        modifier3 = new GameObject();
        modifier4 = new GameObject();
        modifier5 = new GameObject();
    }

    void Start()
    {
        //Canvases
        Skins.enabled = false;
        Updates.enabled = false;
        ValueStore.enabled = false;
        main.enabled = true;

        for (int i = 0; i < ModifierSprites1.Length; i++)
        {
            ModifierSprites1[i].enabled = false;
            ModifierSprites2[i].enabled = false;
            ModifierSprites3[i].enabled = false;
            ModifierSprites4[i].enabled = false;
            ModifierSprites5[i].enabled = false;
        }


        //Button Listeners init

        ButtonUR.onClick.AddListener(UR);
        ButtonUL.onClick.AddListener(UL);
        ButtonMid.onClick.AddListener(Mid);
        ButtonLow.onClick.AddListener(Low);
        // ButtonMAIN.onClick.AddListener(Main);

        PowerUPButton.onClick.AddListener(PowerUP);

        DATA_HOLDER.IsMagazineMain = true;


        //COINS_COUNT.text = "9999";
        //DIAMOND_COUNT.text = "9999";
        count = 0;
    }

    private void OnTestClickedModifier()
    {
        Debug.Log("PASSED");

    }

    public void PowerUP()
    {
        count++;
        //PowerUpLVLText.text = $"{count}";
    }

    public void UR()
    {
        main.enabled = false;
        Updates.enabled = true;
        //ButtonMAIN.image.sprite = MainButtonSpriteCROSS;
        DATA_HOLDER.IsMagazineMain = false;
    }

    public void UL()
    {
        main.enabled = false;
        Skins.enabled = true;
        // ButtonMAIN.image.sprite = MainButtonSpriteCROSS;
        DATA_HOLDER.IsMagazineMain = false;

    }

    public void Mid()
    {
        Debug.Log("as;lkdjsa;ldk");
        DATA_HOLDER.IsMagazineMain = false;
    }

    public void Low()
    {
        main.enabled = false;
        ValueStore.enabled = true;
        DATA_HOLDER.IsMagazineMain = false;
    }

    public void BackToMainMagazine()
    {
        main.enabled = true;
        Updates.enabled = false;
        Skins.enabled = false;
        // ButtonMAIN.image.sprite = MainButtonSpriteMENU;
        DATA_HOLDER.IsMagazineMain = true;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse4))
        {
            main.enabled = true;
            Updates.enabled = false;
            Skins.enabled = false;
            // ButtonMAIN.image.sprite = MainButtonSpriteMENU;
            DATA_HOLDER.IsMagazineMain = true;
        }




    }
}
