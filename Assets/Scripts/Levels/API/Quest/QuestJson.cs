using System.Collections.Generic;

public class QuestJson
{
    public string title { get; set; }
    public string titleClean { get; set; }
    public string description { get; set; }

    public List<HelpooJson> helpo { get; set; }
}

public class HelpooJson
{
    public string message { get; set; }
}