using System.Collections.Generic;
using UnityEngine;

public class ApiController : MonoBehaviour
{
    [SerializeField] private Pipeman pipemanController;
    [SerializeField] private GameObject screenButtons;

    private GameObject playerObject;

    private PlayerEndpoints playerEndpoints;
    private QuestManager questManager;

    [HideInInspector] public List<IUserAction> actions = new List<IUserAction>();

    private void Start()
    {
        playerEndpoints = GetComponent<PlayerEndpoints>();
        questManager = GetComponent<QuestManager>();
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

    public QuestManager GetQuestManager()
    {
        return questManager;
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
