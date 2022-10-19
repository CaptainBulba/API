using Newtonsoft.Json.Linq;
using System.IO;

public class JsonFunctions
{
    public static JObject LoadJson(string file)
    {
        StreamReader reader = new StreamReader(file);
        string json = reader.ReadToEnd();

        return JObject.Parse(json);
    }
}
