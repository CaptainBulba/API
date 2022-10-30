using Newtonsoft.Json.Linq;
using System.Collections;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class Helpo : MonoBehaviour
{
    [SerializeField] private GameObject messageObject;
    [SerializeField] private GameObject skipObject;
    private TextMeshProUGUI messageText;

    private QuestManager questManager;
    private Animator anim;
    private CameraZoom camZoom;
    private TopMenu topMenu;

    private int currentMessage = 0;
    private int currentQuest = 0; 

    private JObject jsonData;
    private int jsonMessageLenght;

    private float typingTimer = 0.05f;
    private bool isShowingMessage = false;

    private void Start()
    {
        camZoom = Camera.main.GetComponent<CameraZoom>();

        anim = GetComponent<Animator>();

        questManager = ApiController.Instance.GetQuestManager();
        topMenu = ApiController.Instance.GetTopMenuObject().GetComponent<TopMenu>();

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
            jsonData = JsonFunctions.LoadJson(filePath);
            jsonMessageLenght = JsonFunctions.CountArray(jsonData["text"]);
            currentMessage = 0;
            currentQuest = questManager.GetCurrentQuest();
        }

        if (jsonMessageLenght == currentMessage)
        {
            MessageFinished();
            yield break;
        }

        anim.Play(HelpoAnimations.HelpoTalking.ToString());

        string text = (string)jsonData["text"][currentMessage]["message"];

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
