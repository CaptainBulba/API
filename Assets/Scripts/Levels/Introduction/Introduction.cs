using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Introduction : FileSettings
{
    private LevelController levelController;

    [SerializeField] private TextMeshProUGUI textObject;

    private int filesAmount;

    private void Start()
    {
        levelController = LevelController.Instance;
        SetFileSettings("Introduction/", "intro_", ".txt");
        filesAmount = levelController.GetTextFunctions().CheckFilesNumber(GetFilePreffix());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(GetFileNumber() != filesAmount)
            {
                textObject.text = levelController.GetTextFunctions().LoadText(GetFileSettings());
                AddFileNumber();
            }
            else
            {
                Debug.Log("Over");
            }
        }
    }
}
