using UnityEngine;
public class DoublingAllBalls : Modifier, IModifier, ISaveable
{
    public static DoublingAllBalls Instance;

    [SerializeField]
    private int DoublingTime = 5;
    [SerializeField]
    private int DoublingAmount = 10;
    [SerializeField]
    private int DoublingPrice = 10;

    [SerializeField]
    private float[] DoublingUpgrade = { 1, 2, 2.5f, 3, 3.5f, 4 };
    [SerializeField]
    private int[] DoublingUpgradePrice = { 0 ,100, 350, 700, 1200, 1900 };
    [SerializeField]
    private int DoublingIndex = 0;

    private GameObject[] _clones;

    void Awake()
    {
        if(Instance != null) Destroy(gameObject);

        WorkingTime = DoublingTime;
        Amount = DoublingAmount;
        Price = DoublingPrice;

        UpgradeBonus = DoublingUpgrade;
        UpgradePrice = DoublingUpgradePrice;
        UpgradeIndex = DoublingIndex;

        Instance = this;
    }

    public override void Activate()
    {
        if (Spend())
        {
            int size = GameManager.Balls.Count;
            _clones = new GameObject[size];

            for (int i = 0; i < size; i++)
            {
                _clones[i] = BallCloner.CreateClone(GameManager.Balls[i].gameObject);
            }         
        }
    }
    public void Disable()
    {
        foreach (var ball in _clones)
        {
            if (!ReferenceEquals(ball, null)) Destroy(ball.gameObject);
        }
    }

    public void Save(SaveData saveData)
    {
        saveData.DoublingModIndex = UpgradeIndex;
        saveData.DoublingModAmount = Amount;
    }

    public void Load(SaveData saveData)
    {
        UpgradeIndex = saveData.DoublingModIndex;
        Amount = saveData.DoublingModAmount;
    }
}
