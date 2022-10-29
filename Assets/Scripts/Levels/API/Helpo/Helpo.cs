using Newtonsoft.Json.Linq;
using System.Collections;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class Helpo : MonoBehaviour
{
    [SerializeField] private GameObject messageObject;
    private TextMeshProUGUI messageText;

    private QuestManager questManager;

    private int currentMessage = 0;
    private int currentQuest; 

    private JObject jsonData;
    private int jsonMessageLenght;

    private float typingTimer = 0.05f;

    private bool isShowingMessage = false;

    private Animator anim;

    private CameraZoom camZoom;

    private TopMenu topMenu;

    private void Start()
    {
        camZoom = Camera.main.GetComponent<CameraZoom>();

        anim = GetComponent<Animator>();

        questManager = ApiController.Instance.GetQuestManager();
        topMenu = ApiController.Instance.GetTopMenuObject().GetComponent<TopMenu>();

        currentQuest = questManager.GetCurrentQuest();

        messageText = messageObject.GetComponentInChildren<TextMeshProUGUI>();

        StartCoroutine(PlayMessage());
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0) && !isShowingMessage)
            StartCoroutine(PlayMessage());
    }

    private IEnumerator PlayMessage()
    {
        if (currentQuest != questManager.GetCurrentQuest() || jsonData == null)
        {
            string filePath = "Assets/Texts/Helpo/helpo_" + questManager.GetCurrentQuest() + ".json";
            jsonData = JsonFunctions.LoadJson(filePath);
            jsonMessageLenght = JsonFunctions.CountArray(jsonData["text"]);
        }

        if(jsonMessageLenght == currentMessage)
        {
            messageObject.SetActive(false);
            isShowingMessage = false;
            camZoom.ReturnFromZoom();
            topMenu.OpenQuest();
            yield break;
        }

        camZoom.ZoomToObject(gameObject);

        anim.Play(HelpoAnimations.HelpoTalking.ToString());

        string text = (string)jsonData["text"][currentMessage]["message"];

        isShowingMessage = true;
        messageText.text = null;
        currentMessage++;

        messageObject.SetActive(true);

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
}
