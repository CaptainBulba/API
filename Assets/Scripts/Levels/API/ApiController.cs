using System.Collections.Generic;
using UnityEngine;

public class ApiController : MonoBehaviour
{
    [SerializeField] private Pipeman pipemanController;
    private PlayerEndpoints playerEndpoints;

    [HideInInspector] public List<IUserAction> actions = new List<IUserAction>();

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
