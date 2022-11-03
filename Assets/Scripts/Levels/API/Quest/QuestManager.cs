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

    private void Start()
    {
        questChecks = GameObject.FindWithTag(ObjectsTags.QuestChecks.ToString());
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
            case nameof(QuestTitles.LosingFocus):
                questChecks.GetComponent<LosingFocus>().enabled = true;
                break;
            case nameof(QuestTitles.HelloWorld):
                questChecks.GetComponent<HelloWorld>().enabled = true;
                break;
            case nameof(QuestTitles.FindingButton):
                questChecks.GetComponent<FindingButton>().enabled = true;
                break;
            case nameof(QuestTitles.TopSecret):
                questChecks.GetComponent<TopSecret>().enabled = true;
                break;
        }
    }

    public QuestTitles GetQuestTitle()
    {
        return (QuestTitles)System.Enum.Parse(typeof(QuestTitles), currentQuestClean);
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
