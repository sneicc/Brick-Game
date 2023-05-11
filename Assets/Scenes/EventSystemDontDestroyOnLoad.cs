using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemDontDestroyOnLoad : MonoBehaviour
{
    public static EventSystemDontDestroyOnLoad Instance;
    private void Awake()
    {
        if(Instance is not null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
