using System.Collections.Generic;
using UnityEngine;

public class ApiController : MonoBehaviour
{
    [SerializeField] private Pipeman pipemanController;
    [SerializeField] private GameObject screenButtons;

    private GameObject playerObject;

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

    public GameObject GetScreenButtons()
    {
        return screenButtons;
    }

    public PlayerEndpoints GetPlayerEndpoints()
    {
        return playerEndpoints;
    }

    public GameObject GetPlayer()
    {
        return playerObject;
    }

    public void SetPlayer(GameObject player)
    {
        if (playerObject == null)
            playerObject = player;
    }
}
