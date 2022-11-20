using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
    private List<HelpooJson> helpoLines; 

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
            // TODO: Logic if quest not found
            return;
        }

        var jsonData = JsonConvert.DeserializeObject<QuestJson>(json);


        currentQuestClean = jsonData.titleClean;
        helpoLines = jsonData.helpo;

        questTitle.text = jsonData.title;
        questDescription.text = jsonData.description;
    }

    public void InitiateQuestChecks()
    {
        if (Enum.IsDefined(typeof(QuestTitles), currentQuestClean))
        {
            QuestChecks quest = questChecks.GetComponent(currentQuestClean) as QuestChecks;
            if(quest.enabled == false)
            {
                quest.enabled = true;
                Debug.Log("Starting Quest: " + currentQuestClean);
            }
        }
    }

    public QuestTitles GetQuestTitle()
    {
        return (QuestTitles)Enum.Parse(typeof(QuestTitles), currentQuestClean);
    }

    public int GetCurrentQuest()
    {
        return currentQuest;
    }

    public void CloseQuest()
    {
        questObject.SetActive(false);
        GetComponent<ApiController>().GetTopMenuObject().SetActive(true);
        InitiateQuestChecks();
    }

    public List<HelpooJson> GetHelpoLines()
    {
        return helpoLines;
    }
}
