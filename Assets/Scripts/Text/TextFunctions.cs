using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextFunctions : MonoBehaviour
{
    private LevelController levelController;
    private string filePath = "Assets/Texts/";

    private void Start()
    {
        levelController = LevelController.Instance;
        //DisplayText("test.txt");
    }

    public void DisplayText(string fileName)
    {
        string file = filePath + fileName; 
        StreamReader reader = new StreamReader(file);
        levelController.GetTextObject().text = reader.ReadToEnd();
        reader.Close();
    }

    public int CheckFilesNumber(string folderName)
    {
        Debug.Log(filePath + folderName);
        int filesNumber = 0;
        var txtFiles = Directory.EnumerateFiles(filePath + folderName, "*.txt");
        
        foreach (string currentFile in txtFiles)
        {
            filesNumber++;
        }
        return filesNumber;
    }
}
