using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Diagnostics;
//using Debug = UnityEngine.Debug;

public class MenuMovement : MonoBehaviour
{
    public float dragSpeed = 1;
    public Vector3 dragOrigin;

    public Vector3 xPos;
    public float yPos;

    private Camera Camera;

    private void Awake()
    {
        Camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(0)) return;

            Vector3 pos = Camera.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(0, 0, pos.y * dragSpeed);

            Camera.transform.Translate(move, Space.World);

            yPos = Mathf.Clamp(Camera.transform.position.z, -20f, 1f);
            Camera.transform.position = new Vector3(0, 0, yPos);
        }


        if (Input.GetMouseButtonUp(0) == true)
        {

        }
    }

    //public static long GetMicr()
    //{
    //    double TimeStamp = Stopwatch.GetTimestamp();
    //    double ms = 1_000_000.0*TimeStamp/Stopwatch.Frequency;

    //    return (long)ms;
    //}

}
