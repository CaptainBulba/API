using UnityEngine;

public class QuestChecks : MonoBehaviour
{
    public ApiController apiController;
    public QuestManager questManager;
    private Helpo helpo;

    protected virtual void Start()
    {
        apiController = ApiController.Instance;
        questManager = apiController.GetQuestManager();
        helpo = FindObjectOfType<Helpo>();
    }

    public void QuestCompleted()
    {
        questManager.QuestCompleted();
        helpo.NewMessage();
        enabled = false;
    }

    public QuestManager GetQuestManager()
    {
        return questManager;
    }

    public Helpo GetHelpo()
    {
        return helpo;
    }

    public ApiController GetApiController()
    {
        return apiController;
    }
}
