using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMod : MonoBehaviour
{
    public float Speed  = 7;
    public float Duration = 3;
    private float ObjectMainSpeed;

    BallB BallB;

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
            gameObject.SetActive(false); //отключать рендерер
        }
    }

    private void RemoveMod()
    {      
        BallB.BounceSpeed = ObjectMainSpeed;
        Destroy(gameObject);
    }

    public void PreventRemoveMod()
    {
        CancelInvoke(nameof(RemoveMod));
        if(!gameObject.active) Destroy(gameObject); //проверять на наличие рендерера ???
    }

    private void PreventAllRemoveMod()
    {
        var speedMods = Resources.FindObjectsOfTypeAll<SpeedMod>(); //на теги
        foreach (var speedMod in speedMods)
        {
            speedMod.PreventRemoveMod();
        }
    }






}
