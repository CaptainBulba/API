using UnityEngine;

public class TopMenu : MonoBehaviour
{
    private GameObject questObject;
    private GameObject pipemanObject;

    private void Start()
    {
        pipemanObject = ApiController.Instance.GetPipeman().gameObject;
        questObject = FindObjectOfType<QuestObjects>().questObject;
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
    }
}
