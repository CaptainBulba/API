using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApiController : MonoBehaviour
{
    [SerializeField] private GameObject screenButtons;
    [SerializeField] private Pipeman pipeman;

    private GameObject playerObject;

    private PlayerEndpoints playerEndpoints;
    private QuestManager questManager;

    [HideInInspector] public List<IUserAction> actions = new List<IUserAction>();

    public static ApiController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Re-Initializing", this);
        
        GameObject playerDecoy = GameObject.FindWithTag("Player Decoy");

        if(playerDecoy != null)
        {
            playerObject.transform.position = playerDecoy.transform.position;
            Destroy(playerDecoy);
        }
    }

    private void Start()
    {
        playerEndpoints = GetComponent<PlayerEndpoints>();
        questManager = GetComponent<QuestManager>();
    }

    public Pipeman GetPipeman()
    {
        return pipeman;
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
