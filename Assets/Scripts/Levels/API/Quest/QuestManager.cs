using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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

    public class QuestJson
    {
        public string title { get; set; }
        public string titleClean { get; set; }
        public string description { get; set; }
    }

    private void Start()
    {
        questChecks = GameObject.FindWithTag(ObjectsTags.QuestChecks.ToString());
        LoadQuest();
    }

    public void QuestCompleted()
    {
        currentQuest++;
        LoadQuest();
    }

    public void LoadQuest()
    {
        string filePath = "Assets/Texts/Quests/quest_" + currentQuest + ".json";
        string json;

        try
        {
            StreamReader r = new StreamReader(filePath);
            json = r.ReadToEnd();
        }
        catch (Exception)
        {
            return;
        }

        var jsonData = JsonConvert.DeserializeObject<QuestJson>(json);

        currentQuestClean = jsonData.titleClean;

        questTitle.text = jsonData.title;
        questDescription.text = jsonData.description;
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
