using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMod : MonoBehaviour
{
    public float Speed  = 7;
    public float Duration = 3;
    private float ObjectMainSpeed;

    private BallB BallB;
    private Renderer Renderer;
    private Collider Collider;

    private void Start()
    {
        Renderer = GetComponent<Renderer>();
        Collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameBall"))
        {
            PreventAllRemoveMod();

            BallB = other.gameObject.GetComponent<BallB>();
            ObjectMainSpeed = BallB.MainSpeed;
            BallB.BounceSpeed = Speed;
            BallB.RB.velocity = BallB.RB.velocity.normalized * Speed;

            Invoke(nameof(RemoveMod), Duration);
            Collider.enabled = false;
            Renderer.enabled = false;

        }
    }

    private void RemoveMod()
    {      
        BallB.BounceSpeed = ObjectMainSpeed;
        BallB.RB.velocity = BallB.RB.velocity.normalized * ObjectMainSpeed;
        Destroy(gameObject);
    }

    public void PreventRemoveMod()
    {
        CancelInvoke(nameof(RemoveMod));
        if(!Renderer.enabled) Destroy(gameObject); 
    }

    private void PreventAllRemoveMod()
    {
        var speedMods = GameObject.FindGameObjectsWithTag("SpeedMod");
        foreach (var speedMod in speedMods)
        {
            speedMod.gameObject.GetComponent<SpeedMod>().PreventRemoveMod();
        }
    }






}
