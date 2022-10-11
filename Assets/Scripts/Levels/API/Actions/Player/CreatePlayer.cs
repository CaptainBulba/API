using UnityEngine;

public class CreatePlayer : IUserAction 
{
    private GameObject player;

    public CreatePlayer(GameObject player)
    {
        this.player = player;
    }

    public void Run()
    {
        player.GetComponent<Player>().ActivatePlayer();
    }
}