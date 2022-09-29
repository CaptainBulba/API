using UnityEngine;

public class ApiController : MonoBehaviour
{
    [SerializeField] private Pipeman pipemanController;

    public PlayerEndpoints playerEndpoints;

    public Pipeman GetPipeman()
    {
        return pipemanController;
    }

    public PlayerEndpoints GetPlayerEndpoints()
    {
        return playerEndpoints;
    }
}
