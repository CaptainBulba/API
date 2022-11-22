using UnityEngine;

public class CreateBox : IUserAction
{
    private GameObject box;
    private int x;
    private int y;

    public CreateBox(GameObject box, int x, int y)
    {
        this.box = box;
        this.x = x;
        this.y = y;
    }

    public void Run()
    {
        box.GetComponent<Box>().ActivateBox(x, y);
    }
}