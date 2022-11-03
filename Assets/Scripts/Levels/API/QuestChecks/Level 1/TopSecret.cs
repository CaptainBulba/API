using UnityEngine;

public class TopSecret : QuestChecks
{
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if(GetApiController().GetPipeman().GetTokenInput() == GetApiController().GetToken())
            QuestCompleted();
    }
}
