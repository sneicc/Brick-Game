using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBorderHandler : MonoBehaviour 
{
    public GameObject TopBorder;
    public GameObject BottomBorder;
    void Start()
    {
        PlaceTopBorder();
        PlaceBottomBorder();
    }

    private void PlaceTopBorder()
    {
        GameObject topbar = GameObject.Find("BackgroundTopBar");

        if (topbar != null)
        {
            RectTransform rectTransform = topbar.GetComponent<RectTransform>();
            Vector3 backgroundBottomInWorldCoords = rectTransform.TransformPoint(new Vector3(0, rectTransform.rect.yMin, 0));
            Vector3 inGameCoord = Camera.main.ScreenToWorldPoint(backgroundBottomInWorldCoords);

            TopBorder.transform.position = new Vector3(inGameCoord.x, inGameCoord.y, TopBorder.transform.position.z);
        }
    }

    private void PlaceBottomBorder()
    {
        GameObject bottombar = GameObject.Find("BackgroundBottomBar");

        if (bottombar != null)
        {
            RectTransform rectTransform = bottombar.GetComponent<RectTransform>();
            Vector3 backgroundTopInWorldCoords = rectTransform.TransformPoint(new Vector3(0, rectTransform.rect.yMax, 0));
            Vector3 inGameCoord = Camera.main.ScreenToWorldPoint(backgroundTopInWorldCoords);

            BottomBorder.transform.position = new Vector3(inGameCoord.x, inGameCoord.y, BottomBorder.transform.position.z);
        }
    }
}
