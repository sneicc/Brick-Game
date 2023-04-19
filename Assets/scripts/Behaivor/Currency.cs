using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Currency : MonoBehaviour
{
    public int Cost;
    public TextMeshProUGUI Text;
    public GameObject Model;

    void Start()
    {
        Text.text = string.Empty;
        Cost += (int)(Cost * Random.Range(0, GameManager.Luck));
    }

    protected void DesplayCostAndDestroy()
    {
        Model.SetActive(false);
        Text.text = $"+{Cost}";
        Invoke(nameof(MyDestroy), 1);
    }

    private void MyDestroy()
    {
        Destroy(gameObject);
    }
}
