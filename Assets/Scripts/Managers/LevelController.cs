using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }
    private TextFunctions textFunctions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        textFunctions = GetComponent<TextFunctions>();
    }

    public TextFunctions GetTextFunctions()
    {
        return textFunctions;
    }
}
