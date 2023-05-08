using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour
{
    public int Time = 3;
    public GameObject Ball;
    private TextMeshProUGUI _text;
    private void Awake()
    {
        SceneManager.LoadScene("StartTimer", LoadSceneMode.Additive);        
    }

    private void Start()
    {
        var t = GameObject.FindGameObjectWithTag("StartTimer");
        _text = t.GetComponent<TextMeshProUGUI>();

        StartCoroutine(StartCounter(Time));
    }

    private IEnumerator StartCounter(int Time)
    {
        while (Time > 0)
        {
            _text.text = Time.ToString();

            yield return new WaitForSeconds(1);

            Time--;
        }

        _text.text = String.Empty;
        SpawnBall();
    }

    private void SpawnBall()
    {
        var mainBall = Instantiate(Ball, transform.position, transform.rotation);
        var ballParams = mainBall.GetComponent<BallB>();
        ballParams.IsClone = false;
        ballParams.IsImmortal = false;
    }
}
