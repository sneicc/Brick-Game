using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformaMove : MonoBehaviour
{

    private Vector3 newPosition;
    public double FieldLeftBorder;
    public double FieldRightBorder;

    void Start()
    {
        FieldLeftBorder = 16.1;
        FieldRightBorder = 19.3;
        newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        if(transform.position.x >= FieldLeftBorder)
            if (Input.GetKey(KeyCode.A))
            {
                newPosition = new Vector3(transform.position.x - 0.05f, transform.position.y, transform.position.z);
            }
        if(transform.position.x <= FieldRightBorder)
            if (Input.GetKey(KeyCode.D))
            {
                newPosition = new Vector3(transform.position.x + 0.05f, transform.position.y, transform.position.z);
            }
        
        transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Ball ball = collision.gameObject.GetComponent<Ball>;
    }

}
