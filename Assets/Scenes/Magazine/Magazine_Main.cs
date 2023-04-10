using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magazine_Main : MonoBehaviour
{

    public Button UpLeftButton;



    // Start is called before the first frame update
    void Start()
    {
        Button btn = UpLeftButton.GetComponent<Button>();
        btn.onClick.AddListener(ChangeScene);
    }

    private void ChangeScene()
    {
        Debug.Log("123" +
            "");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
