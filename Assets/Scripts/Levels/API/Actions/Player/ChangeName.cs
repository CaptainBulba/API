using UnityEngine;

public class ChangeName : IUserAction
{
    private GameObject player;
    private string name;

    public ChangeName(GameObject player, string name)
    {
        this.player = player;
        this.name = name;
    }

    public void Run()
    {
        player.GetComponent<Player>().CoroutineChangeName(name);
    }
}