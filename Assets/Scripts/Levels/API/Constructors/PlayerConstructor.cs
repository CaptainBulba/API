using System;

[Serializable]
public class PlayerConstructor
{
    public bool success = true;
    public string name;
    public string x;
    public string y;
    public bool activeFlag = true;

    public PlayerConstructor(string name, string x, string y)
    {
        this.name = name;
        this.x = x;
        this.y = y;
    }
}
