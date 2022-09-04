using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }

    [SerializeField] private Text textObject;
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

    public Text GetTextObject()
    {
        return textObject;
    }

    public TextFunctions GetTextFunctions()
    {
        return textFunctions;
    }
}
