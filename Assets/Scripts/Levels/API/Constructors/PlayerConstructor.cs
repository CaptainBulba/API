using System;

[Serializable]
public class PlayerConstructor
{
    public bool success = true;
    public string name;
    public string coordinateX;
    public string coordinateY;
    public bool activeFlag;

    public PlayerConstructor(string name, string coordinateX, string coordinateY)
    {
        this.name = name;
        this.coordinateX = coordinateX;
        this.coordinateY = coordinateY;
        activeFlag = true;
    }
}
