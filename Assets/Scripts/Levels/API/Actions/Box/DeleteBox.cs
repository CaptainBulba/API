using UnityEngine;

public class DeleteBox : IUserAction
{
    private GameObject box;

    public DeleteBox(GameObject box)
    {
        this.box = box;
    }

    public void Run()
    {
        box.GetComponent<Box>().DeleteObject();
    }
}