using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI textObject;
    public TextFunctions textFunctions;

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

    public TextMeshProUGUI GetTextObject()
    {
        return textObject;
    }

    public TextFunctions GetTextFunctions()
    {
        return textFunctions;
    }
}
