using UnityEngine;

public class MovePlayer : IUserAction
{
    private GameObject player;
    private float x;
    private float y;
    private float speed = 5f;

    public MovePlayer(GameObject player, float x, float y)
    {
        this.player = player;
        this.x = x;
        this.y = y;
    }

    public void Run()
    {
        player.GetComponent<Player>().SetTargetPosition(x, y);
    }
}