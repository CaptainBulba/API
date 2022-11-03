public class TopSecret : QuestChecks
{
    private void Update()
    {
        if(GetApiController().GetPipeman().GetTokenInput() == GetApiController().GetToken())
            QuestCompleted();
    }
}
