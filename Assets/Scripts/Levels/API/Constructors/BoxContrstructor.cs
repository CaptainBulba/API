using System;

[Serializable]
public class BoxConstructor
{
    public int id;
    private static int idCounter = 0;
    public string x;
    public string y;
    public bool activeFlag = true;

    public BoxConstructor(string x, string y)
    {
        id = GenerateNewId();
        this.x = x;
        this.y = y;
    }

    private int GenerateNewId()
    {
        return idCounter++;
    }

}
