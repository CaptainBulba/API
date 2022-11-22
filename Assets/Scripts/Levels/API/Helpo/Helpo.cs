using System;
using System.Collections;
using System.Collections.Generic;
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

    private List<HelpoJson> helpoLines;
    private int jsonMessageLenght;

    private float typingTimer = 0.05f;
    private bool isShowingMessage = false;

    private void Start()
    {
        camZoom = Camera.main.GetComponent<CameraZoom>();

        anim = GetComponent<Animator>();

        apiController = ApiController.Instance;
        questManager = apiController.GetQuestManager();
        topMenu = apiController.GetTopMenuObject().GetComponent<TopMenu>();

        currentQuest = questManager.GetCurrentQuest();

        messageText = messageObject.GetComponentInChildren<TextMeshProUGUI>();

        InitiateMessage();
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0) && !isShowingMessage)
            InitiateMessage();
    }

    public void SkipMessages()
    {
        MessageFinished();
        currentMessage = jsonMessageLenght;
        anim.Play(HelpoAnimations.HelpoOff.ToString());
    }

    public void InitiateMessage()
    {
        apiController.GetUser().currentState = User.States.Helpo;
        camZoom.ZoomToObject(gameObject);
        StartCoroutine(PlayMessage());
    }

    public IEnumerator PlayMessage()
    {
        if (currentQuest != questManager.GetCurrentQuest() || helpoLines == null)
        {
            helpoLines = questManager.GetHelpoLines();

            jsonMessageLenght = helpoLines.Count;

            currentMessage = 0;
            currentQuest = questManager.GetCurrentQuest();
        }

        if (jsonMessageLenght == currentMessage)
        {
            MessageFinished();
            yield break;
        }

        anim.Play(HelpoAnimations.HelpoTalking.ToString());

        string text = helpoLines[currentMessage].message;

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
        //TODO: Optimize
        if(questManager.GetQuestTitle() == QuestTitles.TopSecret)
            return apiController.GetToken();
        else 
            return null;
    }

    private void MessageFinished()
    {
        ActivateMessage(false);
        isShowingMessage = false;
        camZoom.ReturnFromZoom();
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
