using UnityEngine;
using UnityEngine.UI;

public class ExplosionModifier : Modifier, ISaveable
{
    public static ExplosionModifier Instance;
    /// <summary>
    /// �������� �� 0 �������� ������� � ���������� ������ �� X ������ ����� � �������
    /// </summary>
    ///
    [SerializeField]
    private int ExplosionTime = 5;
    [SerializeField]
    private int ExplosionAmount = 10;
    [SerializeField]
    private int ExplosionPrice = 10;

    [SerializeField]
    private float[] ExplosionUpgrade = { 2, 10, 15, 20, 25, 30 };
    [SerializeField]
    private int[] ExplosionUpgradePrice = { 0, 100, 350, 700, 1200, 1900 };
    [SerializeField]
    private int ExplosionUpgradeIndex = 0;

    [SerializeField]
    private float ExplosionOffset = 0.1f;

    private bool _waitingForClick = false;

    private ColorBlock _colorBuffer;

#if DEBUG
    private RaycastHit _RH = new RaycastHit();
#endif
    void Awake()
    {
        if (Instance != null) Destroy(gameObject);

        WorkingTime = ExplosionTime;
        Amount = ExplosionAmount;
        Price = ExplosionPrice;

        UpgradeBonus = ExplosionUpgrade;
        UpgradePrice = ExplosionUpgradePrice;
        UpgradeIndex = ExplosionUpgradeIndex;

        Instance = this;
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Activate()
    {
        if (Amount > 0) ChangeState();      
    }

    private void ChangeState()
    {
        var colors = _button.colors;

        if (_waitingForClick)
        {
            _button.colors = _colorBuffer;
            _waitingForClick = false;
            GameManager.ResumeGame();
        }
        else
        {                       
            colors.normalColor = colors.selectedColor = colors.highlightedColor = colors.pressedColor;
            _button.colors = colors;
            _waitingForClick = true;
            GameManager.PauseGame();
        }       
    }

    private void Update()
    {
        if (!_waitingForClick) return;
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase != TouchPhase.Began) return;
        }
        else return;
        //if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) &&
        //    EventSystem.current.currentSelectedGameObject != null &&
        //    EventSystem.current.currentSelectedGameObject.layer == LayerMask.NameToLayer("UI")) return;//�����, ���� ������ ��� ��������� UI �� �������� !!!
        

        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;

        int layerMask = LayerMask.GetMask("Background");
        if (Physics.Raycast(ray, out hit, 1000f, layerMask))
        {
#if DEBUG
            _RH = hit;
#endif
            ChangeState();

            if (!Spend()) return;
            int currentDamage = (int)UpgradeBonus[UpgradeIndex];
            Vector3 position = new Vector3(hit.point.x, hit.point.y, hit.point.z - ExplosionOffset);

            Explosion.Instance.Explode(position, currentDamage);
        }
    }


    public override void Subscribe(Button button)
    {
        _button = button;
        _colorBuffer = _button.colors;
        button.onClick.AddListener(Activate);
    }

    public void Save(SaveData saveData)
    {
        saveData.ExplosionModIndex = UpgradeIndex;
        saveData.ExplosionModAmount = Amount;
    }

    public void Load(SaveData saveData)
    {
        UpgradeIndex = saveData.ExplosionModIndex;
        Amount = saveData.ExplosionModAmount;
    }

#if DEBUG
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawSphere(_RH.point, 0.7f);
    }
#endif
}
