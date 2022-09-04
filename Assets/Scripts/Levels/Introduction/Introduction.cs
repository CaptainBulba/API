using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    public LevelController levelController;

    private string filePreffix = "Introduction/";
    private string fileName = "intro_";
    private int fileNumber = 0;
    private string fileSuffix = ".txt";

    private int filesAmount;

    private void Start()
    {
        levelController = LevelController.Instance;
        filesAmount = levelController.GetTextFunctions().CheckFilesNumber(filePreffix);
        Debug.Log(filesAmount);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(fileNumber != filesAmount)
            {
                string filePath = filePreffix + fileName + fileNumber + fileSuffix;
                levelController.GetTextFunctions().DisplayText(filePath);
                fileNumber++;
            }
            else
            {
                Debug.Log("Over");
            }
        }
    }
}
