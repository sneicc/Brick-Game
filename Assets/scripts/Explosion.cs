using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Explosion : Modifier
{
    public static Explosion Instance;

    public int ExplosionTime = 0;
    public int ExplosionAmount = 10;
    public int ExplosionPrice = 10;

    public float[] ExplosionUpgrade = { 10, 15, 20, 25, 30 };
    public int[] ExplosionUpgradePrice = { 100, 350, 700, 1200, 1900 };
    public int ExplosionUpgradeIndex = 0;

    private float ExplosionRadius = 0.7f;

    private bool waitingForClick = false;

    public GameObject ExplosionVFX;
    public Button Button;
    private Color ColorBuffer;
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

    private void Start()
    {
        Button = gameObject.GetComponent<Button>();
    }


    public void OnButtonClick()
    {
        if(Amount > 0) ChangeState();
    }

    private void ChangeState()
    {
        var colors = Button.colors;

        if (waitingForClick)
        {
            colors.normalColor = colors.selectedColor = colors.highlightedColor = ColorBuffer;
            ColorBuffer = colors.pressedColor;
            waitingForClick = false;
        }
        else
        {           
            ColorBuffer = colors.normalColor;
            colors.normalColor = colors.selectedColor = colors.highlightedColor = colors.pressedColor;
            waitingForClick = true;
        }
        Button.colors = colors;
    }

    private void Update()
    {


        if (!waitingForClick) return;
        if (!Input.GetMouseButtonDown(0)) return;
        if (EventSystem.current.IsPointerOverGameObject() &&
            EventSystem.current.currentSelectedGameObject != null &&
            EventSystem.current.currentSelectedGameObject.layer == LayerMask.NameToLayer("UI")) return;//Выход, если курсор над элементом UI 
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
