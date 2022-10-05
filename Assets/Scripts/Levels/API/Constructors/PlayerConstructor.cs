using System;

[Serializable]
public class PlayerConstructor
{
    public bool success = true;
    private static int idCounter = 0;
    public string name { get; set; }
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
