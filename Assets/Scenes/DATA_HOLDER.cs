using UnityEngine;
using Scene = UnityEngine.SceneManagement.Scene;

public class DATA_HOLDER : MonoBehaviour
{

    DATA_HOLDER instance;
    private void Awake()
    {
        if (instance is not null) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    public static bool IsMagazineMain;
    public static Scene currentScene;
}
