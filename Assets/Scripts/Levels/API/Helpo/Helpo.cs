using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Helpo : MonoBehaviour
{
    [SerializeField] private GameObject messageObject;
    [SerializeField] private GameObject skipObject;
    private TextMeshProUGUI messageText;

    private ApiController apiController;
    private QuestManager questManager;
    private Animator anim;
    private CameraZoom camZoom;
    private TopMenu topMenu;

    private int currentMessage = 0;
    private int currentQuest = 0;

    private List<HelpoJson> jsonData;
    private int jsonMessageLenght;

    private float typingTimer = 0.05f;
    private bool isShowingMessage = false;


    [Serializable]
    public class HelpoJson
    {
        public string message { get; set; }
    }

    private void Start()
    {
        camZoom = Camera.main.GetComponent<CameraZoom>();

        anim = GetComponent<Animator>();

        apiController = ApiController.Instance;
        questManager = apiController.GetQuestManager();
        topMenu = apiController.GetTopMenuObject().GetComponent<TopMenu>();

        currentQuest = questManager.GetCurrentQuest();

        messageText = messageObject.GetComponentInChildren<TextMeshProUGUI>();

        camZoom.ZoomToObject(gameObject);
        StartCoroutine(PlayMessage());
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0) && !isShowingMessage)
        {
            camZoom.ZoomToObject(gameObject);
            StartCoroutine(PlayMessage());
        }
    }

    public void SkipMessages()
    {
        MessageFinished();
        currentMessage = jsonMessageLenght;
        anim.Play(HelpoAnimations.HelpoOff.ToString());
    }

    public IEnumerator PlayMessage()
    {
        if (currentQuest != questManager.GetCurrentQuest() || jsonData == null)
        {
            string filePath = "Assets/Texts/Helpo/helpo_" + questManager.GetCurrentQuest() + ".json";
            
            StreamReader r = new StreamReader(filePath);
            string json = r.ReadToEnd();

            jsonData = JsonConvert.DeserializeObject<List<HelpoJson>>(json);

            jsonMessageLenght = jsonData.Count;
            
            currentMessage = 0;
            currentQuest = questManager.GetCurrentQuest();
        }

        if (jsonMessageLenght == currentMessage)
        {
            MessageFinished();
            yield break;
        }

        anim.Play(HelpoAnimations.HelpoTalking.ToString());

        string text = jsonData[currentMessage].message;

        if (text.HasPlaceholder())
            text = string.Format(text, PlaceholderText());

        isShowingMessage = true;
        messageText.text = null;
        currentMessage++;

        ActivateMessage(true);

        foreach (char c in text)
        {
            messageText.text += c;
            yield return new WaitForSeconds(typingTimer);
        }

        if (jsonMessageLenght != currentMessage)
            anim.Play(HelpoAnimations.HelpoMessage.ToString());
        else
            anim.Play(HelpoAnimations.HelpoOff.ToString());

        isShowingMessage = false;
    }

    private string PlaceholderText()
    {
        Debug.Log(questManager.GetQuestTitle());
        if(questManager.GetQuestTitle() == QuestTitles.TopSecret)
            return apiController.GetToken();

        else 
            return null;
    }

    private void MessageFinished()
    {
        ActivateMessage(false);
        isShowingMessage = false;
        camZoom.ReturnFromZoom(true);
        topMenu.OpenQuest();
    }

    public void NewMessage()
    {
        anim.Play(HelpoAnimations.HelpoMessage.ToString());
    }

    private void ActivateMessage(bool option)
    {
        messageObject.SetActive(option);
        skipObject.SetActive(option);
    }
}
