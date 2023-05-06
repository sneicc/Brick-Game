using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRatio : MonoBehaviour
{
    public GameObject Background;
    void Awake()
    {
        //var camera = Camera.main;
        //var renderer = Background.GetComponent<Renderer>();
        //var bounds = renderer.bounds;
        //float vertical = bounds.size.y;
        //float horizontal = bounds.size.x * camera.pixelHeight / camera.pixelWidth;

        //float size = Mathf.Max(horizontal, vertical) * 0.5f;
        //Vector3 center = bounds.center + new Vector3(0, 0, -10);

        //camera.orthographicSize = size;
        //camera.transform.position = center;
    }

    private void Start()
    {
        var camera = Camera.main;
        var renderer = Background.GetComponent<Renderer>();
        var bounds = renderer.bounds;

        float UIHight = GetUIHight();

        float vertical = bounds.size.y + UIHight;
        float horizontal = bounds.size.x * camera.pixelHeight / camera.pixelWidth;

        float size = Mathf.Max(horizontal, vertical) * 0.5f;
        Vector3 center = bounds.center + new Vector3(0, UIHight * 0.5f, -10);

        camera.orthographicSize = size;
        camera.transform.position = center;
    }

    private float GetUIHight()
    {
        GameObject topbar = GameObject.Find("BackgroundTopBar");
        RectTransform rectTransform = topbar.GetComponent<RectTransform>();
        Vector3 backgroundBottomInWorldCoords = rectTransform.TransformPoint(new Vector3(0, -rectTransform.rect.height / 2f, 0));
        float inGameCoord = Camera.main.ScreenToWorldPoint(backgroundBottomInWorldCoords).y;
        float topPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;

        return Mathf.Abs(inGameCoord - topPoint) * 0.83f;
    }
}