using System;

[Serializable]
public class ButtonConstructor
{
    public string name = "Top secret button";
    public string x;
    public string y;
    public bool pressed = false;

    public ButtonConstructor(string x, string y)
    {
        this.x = x;
        this.y = y;
    }
}
