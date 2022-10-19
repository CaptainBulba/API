using Newtonsoft.Json.Linq;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;

public class Helpo : MonoBehaviour
{
    [SerializeField] private GameObject messageObject;
    [SerializeField] private GameObject nameObject;
    private TextMeshProUGUI messageText;

    [SerializeField] private ApiController apiController;
    private QuestManager questManager;

    private int currentMessage = 0;
    private int currentQuest; 

    private JObject jsonData;

    private float typingTimer = 0.125f;

    private bool isShowingMessage = false;

    private IEnumerator co;

    private void Start()
    {
        questManager = apiController.GetQuestManager();
        currentQuest = questManager.GetCurrentQuest();
        messageText = messageObject.GetComponent<TextMeshProUGUI>();
        co = PlayMessage();
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
        }

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

        isShowingMessage = false;
    }
}
