using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRatio : MonoBehaviour
{
    public GameObject Background;
    void Awake()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        var renderer = Background.GetComponent<Renderer>();
        float targetRatio = renderer.bounds.size.x / renderer.bounds.size.y;

        if (screenRatio >= targetRatio)
        {
           Camera.main.orthographicSize = renderer.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = renderer.bounds.size.y / 2 * differenceInSize;
        }
    }
}
