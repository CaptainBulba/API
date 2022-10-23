using Newtonsoft.Json.Linq;
using System.Collections;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class Helpo : MonoBehaviour
{
    [SerializeField] private GameObject messageObject;
    [SerializeField] private GameObject nameObject;
    private TextMeshProUGUI messageText;

    private QuestManager questManager;

    private int currentMessage = 0;
    private int currentQuest; 

    private JObject jsonData;
    private int jsonMessageLenght;

    private float typingTimer = 0.125f;

    private bool isShowingMessage = false;

    private Animator anim;
    private SpriteRenderer spriteRen;

    [SerializeField] private Sprite spriteOff;

    private void Start()
    {
        questManager = ApiController.Instance.GetQuestManager();
        currentQuest = questManager.GetCurrentQuest();
        messageText = messageObject.GetComponent<TextMeshProUGUI>();
        anim = GetComponent<Animator>();
        spriteRen = GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!isShowingMessage)
                StartCoroutine(PlayMessage());
        }
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
            nameObject.SetActive(true);
            messageObject.SetActive(false);
            isShowingMessage = false;
            anim.enabled = false;
            spriteRen.sprite = spriteOff;
            yield break;
        }

        if(anim.enabled == false)
            anim.enabled = true;

        anim.Play(HelpoAnimations.HelpoTalking.ToString());

        string text = (string)jsonData["text"][currentMessage]["message"];

        isShowingMessage = true;
        messageText.text = null;
        currentMessage++;

        nameObject.SetActive(false);
        messageObject.SetActive(true);

        foreach (char c in text)
        {
            messageText.text += c;
            yield return new WaitForSeconds(typingTimer);
        }

        anim.Play(HelpoAnimations.HelpoMessage.ToString());
        isShowingMessage = false;
    }
}
