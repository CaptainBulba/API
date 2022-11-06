using UnityEngine;

public class TopMenu : MonoBehaviour
{
    [SerializeField] private GameObject questObject;
    private GameObject pipemanObject;

    private void Start()
    {
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
    }
}
