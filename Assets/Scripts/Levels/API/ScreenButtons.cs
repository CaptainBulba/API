using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenButtons : MonoBehaviour
{
    [SerializeField] private GameObject apiController;
    [SerializeField] private GameObject pipemanObject;
    [SerializeField] private GameObject questObject;

    public void OpenPipeman()
    {
        pipemanObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OpenQuest()
    {
        gameObject.SetActive(false);
        questObject.SetActive(true);
        apiController.GetComponent<QuestManager>().DisplayQuest();
    }
}
