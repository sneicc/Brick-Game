using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public interface IModifier 
{
    void Activate();
    void Upgrade();
    void Disable();
}
