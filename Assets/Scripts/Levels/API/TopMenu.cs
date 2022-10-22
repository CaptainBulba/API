using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopMenu : MonoBehaviour
{
    [SerializeField] private GameObject apiController;
    [SerializeField] private GameObject questObject;
    private GameObject pipemanObject;

    private void Start()
    {
        apiController = ApiController.Instance.gameObject;
        pipemanObject = ApiController.Instance.GetPipeman().gameObject;
    }

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
