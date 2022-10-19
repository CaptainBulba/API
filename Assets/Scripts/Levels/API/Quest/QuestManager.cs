using Newtonsoft.Json.Linq;
using System.IO;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject questObject;
    [SerializeField] private TextMeshProUGUI questTitle;
    [SerializeField] private TextMeshProUGUI questDescription;

    private int currentQuest = 0; 

    private void Start()
    {
        DisplayQuest();   
    }

    public void QuestCompleted()
    {
        currentQuest++;
    }

    public void DisplayQuest()
    {
        string filePath = "Assets/Texts/Quests/quest_" + currentQuest + ".json";

        StreamReader reader = new StreamReader(filePath);
        string json = reader.ReadToEnd();

        var jsonData = JObject.Parse(json);

        questTitle.text = (string)jsonData["title"];
        questDescription.text = (string)jsonData["description"];
    }

    public int GetCurrentQuest()
    {
        return currentQuest;
    }

    public void CloseQuest()
    {
        questObject.SetActive(false);
        GetComponent<ApiController>().GetScreenButtons().SetActive(true);
    }
}
