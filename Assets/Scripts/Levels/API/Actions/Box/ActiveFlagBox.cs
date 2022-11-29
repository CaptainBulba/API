using UnityEngine;

public class ActiveFlagBox : IUserAction
{
    private GameObject box;
    private bool activeFlag;

    public ActiveFlagBox(GameObject box, bool activeFlag)
    {
        this.activeFlag = activeFlag;
        this.box = box;
    }

    public void Run()
    {
        box.GetComponent<Box>().ActiveFlag(activeFlag);
    }
}