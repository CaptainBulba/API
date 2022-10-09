using UnityEngine;

public class ApiController : MonoBehaviour
{
    [SerializeField] private Pipeman pipemanController;
    private PlayerEndpoints playerEndpoints;

    private void Start()
    {
        playerEndpoints = GetComponent<PlayerEndpoints>();
    }

    public Pipeman GetPipeman()
    {
        return pipemanController;
    }

    public PlayerEndpoints GetPlayerEndpoints()
    {
        return playerEndpoints;
    }
}
