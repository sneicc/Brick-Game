using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour
{
    public static LevelUIController Instance { get; private set; }

    public Button PauseButton;
    public Button DamageButton;
    public Button SpeedButton;
    public Button DoublingButton;
    public Button ExplosionButton;

    [SerializeField]
    private TextMeshProUGUI DamageModAmount;
    [SerializeField]
    private TextMeshProUGUI SpeedModAmount;
    [SerializeField]
    private TextMeshProUGUI DoublingModAmount;
    [SerializeField]
    private TextMeshProUGUI ExplosionModAmount;

    public Image[] Hearts;
    public Color SpendHearth;
    public Color BonusHearth;

    private Color _mainColor;

    public TextMeshProUGUI COINS_COUNT;
    public TextMeshProUGUI DIAMOND_COUNT;

    private void Awake()
    {
        if(Instance is not null) Destroy(gameObject);
        Instance = this;

        _mainColor = Hearts[0].color;
        GameManager.LivesChanged += OnLivesChanged;

        DamageModifier.Instance.Subscribe(DamageButton);
        SpeedAndImmortalModifier.Instance.Subscribe(SpeedButton);
        DoublingAllBalls.Instance.Subscribe(DoublingButton);
        Explosion.Instance.Subscribe(ExplosionButton);

        DamageModifier.Instance.AmountChanged += OnDamageAmountChanged;
        SpeedAndImmortalModifier.Instance.AmountChanged += OnSpeedAmountChanged;
        DoublingAllBalls.Instance.AmountChanged += OnDoublingAmountChanged;
        Explosion.Instance.AmountChanged += OnExplosionAmountChanged;

        LevelManager.Instance.CoinsChanged += OnCoinsChanged;
        LevelManager.Instance.DaimondsChanged += OnDiamondChanged;

        COINS_COUNT.text = LevelManager.Instance.LevelCoins.ToString();
        DIAMOND_COUNT.text = LevelManager.Instance.LevelDaimonds.ToString();

        DamageModAmount.text = DamageModifier.Instance.Amount.ToString();
        SpeedModAmount.text = SpeedAndImmortalModifier.Instance.Amount.ToString();
        DoublingModAmount.text = DoublingAllBalls.Instance.Amount.ToString();
        ExplosionModAmount.text = Explosion.Instance.Amount.ToString();
    }
    protected void OnCoinsChanged()
    {
        COINS_COUNT.text = LevelManager.Instance.LevelCoins.ToString();
    }

    protected void OnDiamondChanged()
    {
        DIAMOND_COUNT.text = LevelManager.Instance.LevelDaimonds.ToString();
    }

    private void OnLivesChanged()
    {
        int lives = GameManager.Lives - 1;

        for (int i = 0; i < Hearts.Length; i++)
        {
            Hearts[i].color = _mainColor;
        }

        if (lives < 3)
        {
            for (int i = 2; i > lives; i--)
            {
                Hearts[i].color = SpendHearth;
            }
        }
        else
        {
            lives -= 2;
            for (int i = 0; i < lives; i++)
            {
                Hearts[i].color = BonusHearth;
            }
        }
     
    }

    private void OnDamageAmountChanged(int amount)
    {
        DamageModAmount.text = amount.ToString();
    }

    private void OnSpeedAmountChanged(int amount)
    {
        SpeedModAmount.text = amount.ToString();
    }

    private void OnDoublingAmountChanged(int amount)
    {
        DoublingModAmount.text = amount.ToString();
    }

    private void OnExplosionAmountChanged(int amount)
    {
        ExplosionModAmount.text = amount.ToString();
    }

    private void OnDestroy()
    {
        Instance = null;

        DamageModifier.Instance.Unsubscribe();
        SpeedAndImmortalModifier.Instance.Unsubscribe();
        DoublingAllBalls.Instance.Unsubscribe();
        Explosion.Instance.Unsubscribe();

        DamageModifier.Instance.AmountChanged -= OnDamageAmountChanged;
        SpeedAndImmortalModifier.Instance.AmountChanged -= OnSpeedAmountChanged;
        DoublingAllBalls.Instance.AmountChanged -= OnDoublingAmountChanged;
        Explosion.Instance.AmountChanged -= OnExplosionAmountChanged;

        LevelManager.Instance.CoinsChanged -= OnCoinsChanged;
        LevelManager.Instance.DaimondsChanged -= OnDiamondChanged;

        GameManager.LivesChanged -= OnLivesChanged;
    }    
}
