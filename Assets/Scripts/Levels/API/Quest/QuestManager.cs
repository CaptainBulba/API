using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private GameObject questChecks;
    private QuestObjects questObjects;

    private int currentQuest = 0;
    private string currentQuestClean;
    private List<HelpoJson> helpoLines;

    private void Start()
    {
        questObjects = FindObjectOfType<QuestObjects>();
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

        questObjects.questTitle.text = jsonData.title;
        questObjects.questDescription.text = jsonData.description;
    }

    public void InitiateQuestChecks()
    {
        QuestChecks quest = questChecks.GetComponent(currentQuestClean) as QuestChecks;
        if (quest != null && quest.enabled == false)
        {
            quest.enabled = true;
            Debug.Log("Starting Quest: " + currentQuestClean);
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
        questObjects.questObject.SetActive(false);
        GetComponent<ApiController>().GetTopMenuObject().SetActive(true);
        InitiateQuestChecks();
    }

    public List<HelpoJson> GetHelpoLines()
    {
        return helpoLines;
    }
}
