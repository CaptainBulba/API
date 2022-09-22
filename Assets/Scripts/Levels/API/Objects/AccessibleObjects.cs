using System;

[Serializable]
public class AccessibleObjects 
{
    public bool success = true;
    public int id;
    private static int idCounter = 0;
    public string name;
    public int xCord;
    public int yCord;
    public bool activeFlag; 
    
    public AccessibleObjects(string name, int xCord, int yCord)
    {
        this.id = GenerateNewId();
        this.name = name;
        this.xCord = xCord;
        this.yCord = yCord;
        activeFlag = true;
    }

    private int GenerateNewId()
    {
        return idCounter++;
    }

    public int GetObjectId()
    {
        return id;
    }

    public string GetObjectName()
    {
        return name;
    }
}
