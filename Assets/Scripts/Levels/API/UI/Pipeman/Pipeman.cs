using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class Pipeman : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI responseText;
    [SerializeField] private TMP_InputField bodyText;
    [SerializeField] private TMP_InputField endpointText;
    [SerializeField] private TMP_Dropdown endpointType;

    [SerializeField] private GameObject paramTab;
    [SerializeField] private GameObject authTab;
    [SerializeField] private GameObject bodyTab;

    [SerializeField] private TMP_InputField[] paramKeys;
    [SerializeField] private TMP_InputField[] paramValues;

    [SerializeField] private TMP_InputField tokenInput;

    [SerializeField] private GameObject topMenu;

    private ApiController apiController;

    private GameObject[] tabs;

    private PlayerEndpoints playerEndpoints;
    private ButtonEndpoints buttonEndpoints;
    private BoxEndpoints boxEndpoints;

    private void Start()
    {
        apiController = ApiController.Instance;
        tabs = new GameObject[] { paramTab, authTab, bodyTab };
        playerEndpoints = apiController.GetPlayerEndpoints();
        buttonEndpoints = apiController.GetButtonEndpoints();
        boxEndpoints = apiController.GetBoxEndpoints();
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

        Debug.Log(fullEndpoint);

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
            case EndpointConstants.getAllBoxes:
                boxEndpoints.GetAllBoxes();
                break;
            case EndpointConstants.getBox:
                boxEndpoints.GetBox();
                break;
            case EndpointConstants.putBox:
                boxEndpoints.PutBox(body);
                break;
            case EndpointConstants.postBox:
                boxEndpoints.PostBox(body);
                break;
            default:
                ChangeResponse("Endpoint does not exist");
                break;
        } 
    }

    public Dictionary<string, string> GetParameters()
    {
        Dictionary<string, string> parameters = new Dictionary<string, string>();

        for (int i = 0; i < paramKeys.Length; i++)
        {
            parameters.Add(paramKeys[i].text, paramValues[i].text);
        }
        return parameters;
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
