using UnityEngine;

public class CreatePlayer : IUserAction 
{
    private GameObject player;
    private string name;
    private int x;
    private int y;

    public CreatePlayer(GameObject player, string name, int x, int y)
    {
        this.player = player;
        this.name = name;
        this.x = x;
        this.y = y;
    }

    public void Run()
    {
        player.GetComponent<Player>().ActivatePlayer(name, x, y);
        ApiController.Instance.SetPlayer(player);
    }
}