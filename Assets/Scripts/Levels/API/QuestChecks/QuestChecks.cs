using UnityEngine;

public class QuestChecks : MonoBehaviour
{
    private QuestManager questManager;
    private Helpo helpo;

    protected virtual void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        helpo = FindObjectOfType<Helpo>();
    }

    public void QuestCompleted()
    {
        questManager.QuestCompleted();
        helpo.NewMessage();
    }

    public QuestManager GetQuestManager()
    {
        return questManager;
    }

    public Helpo GetHelpo()
    {
        return helpo;
    }


}
