using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRatio : MonoBehaviour
{
    public GameObject Background;
    private void Start()
    {
        var camera = Camera.main;
        var renderer = Background.GetComponent<Renderer>();
        var bounds = renderer.bounds;

        float topUIHeight = GetTopUIHight();
        float bottomUIHeight = GetBottomUIHight();
        float totalUiHeight = topUIHeight + bottomUIHeight;

        float vertical = bounds.size.y + totalUiHeight;
        float horizontal = bounds.size.x * camera.pixelHeight / camera.pixelWidth;

        float size = Mathf.Max(horizontal, vertical) * 0.5f;
        Vector3 center = bounds.center + new Vector3(0, (topUIHeight - bottomUIHeight) * 0.5f, -10);

        camera.orthographicSize = size;
        camera.transform.position = center;
    }

    private float GetTopUIHight()
    {
        GameObject topbar = GameObject.Find("BackgroundTopBar");
        RectTransform rectTransform = topbar.GetComponent<RectTransform>();
        Vector3 backgroundBottomInWorldCoords = rectTransform.TransformPoint(new Vector3(0, rectTransform.rect.yMin, 0));
        float inGameCoord = Camera.main.ScreenToWorldPoint(backgroundBottomInWorldCoords).y;
        float topScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;

        return Vector3.Distance(new Vector3(0, inGameCoord, 0), new Vector3(0, topScreenPoint, 0));
    }

    private float GetBottomUIHight()
    {
        GameObject topbar = GameObject.Find("BackgroundBottomBar");
        RectTransform rectTransform = topbar.GetComponent<RectTransform>();
        Vector3 backgroundTopInWorldCoords = rectTransform.TransformPoint(new Vector3(0, rectTransform.rect.yMax, 0));
        float inGameCoord = Camera.main.ScreenToWorldPoint(backgroundTopInWorldCoords).y;
        float bottomScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(0,0, Camera.main.nearClipPlane)).y;

        return Vector3.Distance(new Vector3(0, bottomScreenPoint, 0), new Vector3(0, inGameCoord, 0));
    }


}