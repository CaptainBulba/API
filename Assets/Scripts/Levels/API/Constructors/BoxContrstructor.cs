using System;
using UnityEngine;

[Serializable]
public class BoxConstructor
{
    public int id;
    private static int idCounter = 0;
    public string x;
    public string y;
    public bool activeFlag = true;
    private GameObject boxObject;

    public BoxConstructor(GameObject boxObject, string x, string y)
    {
        id = GenerateNewId();
        this.x = x;
        this.y = y;
        this.boxObject = boxObject;
    }

    private int GenerateNewId()
    {
        return idCounter++;
    }

    public GameObject GetBoxObject()
    {
        return boxObject;
    }

}
