using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Cost;
    public TextMeshProUGUI CoinText;
    public GameObject CoinModel;
    // Start is called before the first frame update
    void Start()
    {
        CoinText.text = string.Empty;
        Cost += (int)(Cost * Random.Range(0, GameManager.Luck));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameBall"))
        {
            GameManager.AddCoins(Cost);
            CoinModel.SetActive(false);
            CoinText.text = $"+{Cost}";
            Invoke(nameof(MyDestroy), 1);
        }       
    }

    private void MyDestroy()
    {
        Destroy(gameObject);
    }
}
