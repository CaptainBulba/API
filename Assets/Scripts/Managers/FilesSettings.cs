using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileSettings : MonoBehaviour
{
    private string filePreffix;
    private string fileName;
    private int fileNumber = 0;
    private string fileSuffix;

    public void SetFileSettings(string prefix, string name, string suffix)
    {
        filePreffix = prefix;
        fileName = name;
        fileSuffix = suffix;
    }

    public string GetFileSettings()
    {
        return filePreffix + fileName + fileNumber + fileSuffix;
    }

    public string GetFilePreffix()
    {
        return filePreffix;
    }

    public int GetFileNumber()
    {
        return fileNumber;
    }

    public void AddFileNumber()
    {
        fileNumber++;
    }
}
