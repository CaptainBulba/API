using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pipeman : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI responseText;
    [SerializeField] private TMP_InputField bodyText;
    [SerializeField] private TMP_InputField endpointText;
    [SerializeField] private TMP_Dropdown endpointType;
    [SerializeField] private ScrollRect scrollRect;

    [SerializeField] private GameObject paramTab;
    [SerializeField] private GameObject authTab;
    [SerializeField] private GameObject bodyTab;

    private GameObject[] tabs;

    private void Start()
    {
        tabs = new GameObject[] { paramTab, authTab, bodyTab };
    }

    public void ChangeResponse(string text)
    {
        responseText.text = text;
        scrollRect.verticalNormalizedPosition = 1f;
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
                Debug.Log(EndpointConstants.getPlayer);
                break;

            case EndpointConstants.putPlayer:
                Debug.Log(EndpointConstants.putPlayer);
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
}
