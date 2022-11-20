using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pipeman : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI responseText;
    [SerializeField] private TMP_InputField bodyText;
    [SerializeField] private TMP_InputField endpointText;
    [SerializeField] private TMP_Dropdown endpointType;

    [SerializeField] private GameObject paramTab;
    [SerializeField] private GameObject authTab;
    [SerializeField] private GameObject bodyTab;

    [SerializeField] private TMP_InputField tokenInput;

    [SerializeField] private GameObject topMenu;

    private ApiController apiController;

    private GameObject[] tabs;

    private PlayerEndpoints playerEndpoints;
    private ButtonEndpoints buttonEndpoints;

    private void Start()
    {
        apiController = ApiController.Instance;
        tabs = new GameObject[] { paramTab, authTab, bodyTab };
        playerEndpoints = apiController.GetPlayerEndpoints();
        buttonEndpoints = apiController.GetButtonEndpoints();
    }

    public void ChangeResponse(string text)
    {
        responseText.text = text;
    }

    public void SendButton()
    {
        string type = endpointType.captionText.text;
        string endpoint = endpointText.text;
        string body = bodyText.text;

        string fullEndpoint = type[0] + type.Substring(1).ToLower() + endpoint;

        switch (fullEndpoint)
        {
            case EndpointConstants.getPlayer:
                playerEndpoints.GetPlayer();
                break;

            case EndpointConstants.putPlayer:
                playerEndpoints.PutPlayer(body);
                break;
            case EndpointConstants.postPlayer:
                playerEndpoints.PostPlayer(body);
                break;
            case EndpointConstants.getButton:
                buttonEndpoints.GetButton();
                break;
            case EndpointConstants.postButton:
                buttonEndpoints.PostButton(body);
                break;

            default:
                ChangeResponse("Endpoint does not exist");
                break;
        } 
    }

    private void HideAllOptions()
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            for (int n = 0; n < tabs[i].transform.childCount; n++)
            {
                tabs[i].transform.GetChild(n).gameObject.SetActive(false);
            }
        }
    }

    private void DisplayOption(GameObject option)
    {
        for (int i = 0; i < option.transform.childCount; i++)
        {
            option.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void ChangeTab(int tab)
    {
        TabEnums currentTab = (TabEnums)tab;

        HideAllOptions();
        
        switch (currentTab)
        {
            case TabEnums.Authorization:
                DisplayOption(authTab);
                break;
            case TabEnums.Parameters:
                DisplayOption(paramTab);
                break;
            case TabEnums.Body:
                DisplayOption(bodyTab);
                break;
        }
    }

    public void DisplayError(string key, Errors error)
    {
        string errorText = GetErrorCode(error) + " " + key + " " + ErrorDescriptions.GetErrorDescription(error);
        Error json = new Error(errorText);
        ChangeResponse(JsonConvert.SerializeObject(json, Formatting.Indented));
    }

    public static string GetErrorCode(Errors error)
    {
        return $"[ERR{(int)error:D3}]";
    }

    public void ClosePipeman()
    {
        gameObject.SetActive(false);
        topMenu.SetActive(true);

        if(apiController.actions.Count != 0)
        {
            foreach (var action in apiController.actions)
            {
                action.Run();
            }
            apiController.actions.Clear();
        }
    }

    public string GetTokenInput()
    {
        return tokenInput.text;
    }

    public void SetTokeInput(string token)
    {
        tokenInput.text = token;
    }
}
