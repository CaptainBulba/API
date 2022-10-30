using Newtonsoft.Json.Linq;
using System.IO;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private GameObject questChecks;
    [SerializeField] private GameObject questObject;
    [SerializeField] private TextMeshProUGUI questTitle;
    [SerializeField] private TextMeshProUGUI questDescription;

    private int currentQuest = 0;
    private string currentQuestClean;

    public enum QuestsNames
    {
        LosingFocus
    }

    private void Start()
    {
        questChecks = GameObject.FindWithTag(ObjectsTags.QuestChecks.ToString());
    //    DisplayQuest();
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
        currentQuestClean = (string)jsonData["titleClean"];
    }

    public void InitiateQuestChecks()
    {
        switch (currentQuestClean)
        {
            case nameof(QuestsNames.LosingFocus):
                questChecks.GetComponent<LosingFocus>().enabled = true;
                break;
        }
    }

    public int GetCurrentQuest()
    {
        return currentQuest;
    }

    public void CloseQuest()
    {
        questObject.SetActive(false);
        GetComponent<ApiController>().GetTopMenuObject().SetActive(true);
    }
}
