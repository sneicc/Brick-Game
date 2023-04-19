using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMapControl : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        SceneManager.LoadScene(5, LoadSceneMode.Additive);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
