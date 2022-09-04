using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextFile : MonoBehaviour
{
    void ReadString()
    {
        string path = "Assets/Texts/test.txt";
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

    private void Start()
    {
        ReadString();
    }
}
