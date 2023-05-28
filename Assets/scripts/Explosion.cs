using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Explosion : Modifier
{
    public static Explosion Instance;
    /// <summary>
    /// Отличное от 0 значение приведёт к блокировке кнопки на X секунд после её нажатия
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
    private float ExplosionRadius = 0.7f;
    [SerializeField]
    private float ExplosionOffset = 0.1f;

    private bool _waitingForClick = false;

    private ColorBlock _colorBuffer;

    public GameObject ExplosionVFX;

#if DEBUG
    private RaycastHit _RH = new RaycastHit();
#endif
    void Awake()
    {
        if (Instance is not null) Destroy(gameObject);

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
            if(!GameManager.IsGameWin) GameManager.ResumeGame();
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
        //    EventSystem.current.currentSelectedGameObject.layer == LayerMask.NameToLayer("UI")) return;//Выход, если курсор над элементом UI НЕ РАБОТАЕТ !!!
        

        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;

        int layerMask = LayerMask.GetMask("Background");
        if (Physics.Raycast(ray, out hit, 1000f, layerMask))
        {
#if DEBUG
            _RH = hit;
#endif
            if (!Spend()) return;
            Instantiate(ExplosionVFX, new Vector3(hit.point.x, hit.point.y, hit.point.z - ExplosionOffset), Quaternion.Euler(0, 180, 0));
            Collider[] colliders = Physics.OverlapSphere(hit.point, ExplosionRadius);
            MakeDamage(colliders);
            ChangeState();
        }
    }

    private void MakeDamage(Collider[] colliders)
    {
        int currentDamage = (int)UpgradeBonus[UpgradeIndex];

        foreach (var item in colliders)
        {
            if (item.CompareTag("Brick"))
            {
                item.GetComponent<Brick>().Hit(currentDamage);
            }
        }
    }

    public override void Subscribe(Button button)
    {
        _button = button;
        _colorBuffer = _button.colors;
        button.onClick.AddListener(Activate);
    }

#if DEBUG
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawSphere(_RH.point, ExplosionRadius);
    }
#endif
}
