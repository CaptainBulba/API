using UnityEngine;

public class PressedButton : IUserAction
{
    private GameObject button;

    public PressedButton(GameObject button)
    {
        this.button = button;
    }

    public void Run()
    {
        button.GetComponent<LoadNextScene>().NextScene();
    }
}