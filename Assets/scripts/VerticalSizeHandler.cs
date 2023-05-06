using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSizeHandler : MonoBehaviour // создать границы для лазеров и использовать на них
{
    public GameObject TopBorder;
    public GameObject Background;
    // Start is called before the first frame update
    void Start()
    {
        PlaceTopBorder();
    }

    private void PlaceTopBorder()
    {
        GameObject topbar = GameObject.Find("BackgroundTopBar");

        if (topbar != null)
        {
            RectTransform rectTransform = topbar.GetComponent<RectTransform>();
            Vector3 backgroundBottomInWorldCoords = rectTransform.TransformPoint(new Vector3(0, -rectTransform.rect.height / 2f, 0));
            Vector3 inGameCoord = Camera.main.ScreenToWorldPoint(backgroundBottomInWorldCoords);

            TopBorder.transform.position = new Vector3(inGameCoord.x, inGameCoord.y, TopBorder.transform.position.z);
        }
#if DEBUG
        Debug.Log(topbar?.name);
#endif
    }
}
