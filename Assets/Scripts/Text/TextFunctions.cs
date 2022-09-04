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

    public string LoadText(string fileName)
    {
        string text;
        string file = filePath + fileName;

        StreamReader reader = new StreamReader(file);
        text = reader.ReadToEnd();
        reader.Close();
        
        return text;
    }

    public int CheckFilesNumber(string folderName)
    {
        int filesNumber = 0;
        var txtFiles = Directory.EnumerateFiles(filePath + folderName, "*.txt");
        
        foreach (string currentFile in txtFiles)
        {
            filesNumber++;
        }
        return filesNumber;
    }
}
