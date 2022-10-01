using System;

[Serializable]
public class PlayerConstructor
{
    public bool success = true;
    public int id;
    private static int idCounter = 0;
    public string name { get; set; }
    public string xCord;
    public string yCord;
    public bool activeFlag;

    public PlayerConstructor(string name, string xCord, string yCord, bool includeId)
    {
        this.name = name;
        this.xCord = xCord;
        this.yCord = yCord;
        activeFlag = true;

        if (includeId)
            this.id = GenerateNewId();
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
