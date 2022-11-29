using UnityEngine;

public class MoveBox : IUserAction
{
    private GameObject box;
    private int x;
    private int y;

    public MoveBox(GameObject box, int x, int y)
    {
        this.box = box;
        this.x = x;
        this.y = y;
    }

    public void Run()
    {
        box.GetComponent<Box>().CoroutineMoveBox(x, y);
    }
}