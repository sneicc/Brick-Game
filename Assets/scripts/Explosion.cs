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
    public int ExplosionTime = 5; 
    public int ExplosionAmount = 10;
    public int ExplosionPrice = 10;

    public float[] ExplosionUpgrade = { 10, 15, 20, 25, 30 };
    public int[] ExplosionUpgradePrice = { 100, 350, 700, 1200, 1900 };
    public int ExplosionUpgradeIndex = 0;

    public float ExplosionRadius = 0.7f;

    private bool waitingForClick = false;

    private ColorBlock _colorBuffer;

    public GameObject ExplosionVFX;
    void Awake()
    {
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
        _colorBuffer = _Button.colors;  
    }

    public void OnButtonClick()
    {
        if(Amount > 0) ChangeState();
    }

    private void ChangeState()
    {
        var colors = _Button.colors;

        if (waitingForClick)
        {
            _Button.colors = _colorBuffer;
            waitingForClick = false;
            GameManager.ResumeGame();
        }
        else
        {                       
            colors.normalColor = colors.selectedColor = colors.highlightedColor = colors.pressedColor;
            _Button.colors = colors;
            waitingForClick = true;
            GameManager.PauseGame();
        }       
    }

    private void Update()
    {
        if (!waitingForClick) return;
        if (!Input.GetMouseButtonDown(0)) return;
        if (EventSystem.current.IsPointerOverGameObject() &&
            EventSystem.current.currentSelectedGameObject != null &&
            EventSystem.current.currentSelectedGameObject.layer == LayerMask.NameToLayer("UI")) return;//Выход, если курсор над элементом UI НЕ РАБОТАЕТ !!!
        if (!Spend()) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;               
        if (Physics.Raycast(ray, out hit))
        {
            Instantiate(ExplosionVFX, new Vector3(hit.point.x, hit.point.y, hit.point.z - 1f), new Quaternion());
            Collider[] colliders = Physics.OverlapSphere(hit.point, ExplosionRadius);
            MakeDamage(colliders);
        }
        ChangeState();
    }

    private void MakeDamage(Collider[] colliders)
    {
        int currentDamage = (int)UpgradeBonus[UpgradeIndex];

        foreach (var item in colliders)
        {
            if (item.CompareTag("Brick"))
            {
                item.GetComponent<Bricks>().Hit(currentDamage);
            }
        }
    }
}
