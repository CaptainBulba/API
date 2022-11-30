using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApiController : MonoBehaviour
{
    private string apiToken = "topsecret";

    [SerializeField] private GameObject topMenu;
    [SerializeField] private Pipeman pipeman;
    private GameObject button;

    private GameObject playerObject;

    private PlayerEndpoints playerEndpoints;
    private ButtonEndpoints buttonEndpoints;
    private BoxEndpoints boxEndpoints;

    private QuestManager questManager;
    private User user;

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
        SetPlayer();
        SetButton();
        SetTopMenu();
        SetPipeman();
    }

    private void Start()
    {
        playerEndpoints = GetComponent<PlayerEndpoints>();
        questManager = GetComponent<QuestManager>();
        buttonEndpoints = GetComponent<ButtonEndpoints>();
        boxEndpoints = GetComponent<BoxEndpoints>();
        user = GetComponent<User>();
        user.currentState = User.States.Playing;
    }

    private void SetPipeman()
    {
        pipeman = FindObjectOfType<Pipeman>();
    }

    private void SetTopMenu()
    { 
        topMenu = FindObjectOfType<TopMenu>().gameObject;
    }

    private void SetButton()
    {
        button = GameObject.FindWithTag(ObjectsTags.Button.ToString());
    }

    private void SetPlayer()
    {
        GameObject playerDecoy = GameObject.FindWithTag(ObjectsTags.PlayerDecoy.ToString());

        if (playerDecoy != null && playerObject != null)
        {
            playerObject.GetComponent<Player>().DecoySwitch(playerDecoy);
            Destroy(playerDecoy);
        }
    }

    public BoxEndpoints GetBoxEndpoints()
    {
        return boxEndpoints;
    }

    public User GetUser()
    {
        return user;
    }

    public Pipeman GetPipeman()
    {
        return pipeman;
    }

    public GameObject GetTopMenuObject()
    {
        return topMenu;
    }

    public PlayerEndpoints GetPlayerEndpoints()
    {
        return playerEndpoints;
    }

    public ButtonEndpoints GetButtonEndpoints()
    {
        return buttonEndpoints;
    }

    public QuestManager GetQuestManager()
    {
        return questManager;
    }

    public GameObject GetPlayer()
    {
        return playerObject;
    }

    public GameObject GetButtonObject()
    {
        return button;
    }
    public void SetPlayer(GameObject player)
    {
        if (playerObject == null)
            playerObject = player;
    }

    public string GetToken()
    {
        return apiToken;
    }
}
