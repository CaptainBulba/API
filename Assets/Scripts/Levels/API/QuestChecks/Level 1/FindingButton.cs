using UnityEngine;

public class FindingButton : QuestChecks
{
    private void Update()
    {
        if (Vector3.Distance(GetApiController().GetPlayer().transform.position, GetApiController().GetButtonObject().transform.position) > 0.01f)
            QuestCompleted();
    }
}
