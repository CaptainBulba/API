using UnityEngine;
using UnityEngine.SceneManagement;

public class UiSceneSwitch : MonoBehaviour
{
    public static UiSceneSwitch Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
            Destroy(gameObject);
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
    }
}
