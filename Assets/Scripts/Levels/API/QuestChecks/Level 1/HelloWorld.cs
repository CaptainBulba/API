using UnityEngine;

public class HelloWorld : QuestChecks
{
    private void Update()
    {
        if(GetApiController().GetPlayer() != null)
            QuestCompleted();
    }
}
