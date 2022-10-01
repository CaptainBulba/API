using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEndpoints : MonoBehaviour
{
    private PlayerConstructor player;

    [SerializeField] private GameObject playerPrefab;

    private Pipeman pipemanController;

    private List<string> acceptedVariables = new List<string> { "name", "xCord" };

    private void Start()
    {
        pipemanController = GetComponent<ApiController>().GetPipeman();

        string json = "{\"name\": \"Bob\", \"xCord\": \"1\"}";
    }

    public void PlayerGet()
    {
        if (IsPlayerExists())
        {
            pipemanController.ChangeResponse(ObjectToJson(player));
        }
        else
            Debug.Log("Player does not exist");
    }

    public void PlayerPut(string json)
    {
        if(IsValidJson(json) || IsPlayerExists())
        {
            PlayerConstructor jsonData = JsonUtility.FromJson<PlayerConstructor>(json);

            string name = jsonData.name;
            string x = jsonData.xCord;
            string y = jsonData.yCord;

            if (CheckName(name) && CheckCord(x) && CheckCord(y))
            {
                player = new PlayerConstructor(name, x, y, true);
                Instantiate(playerPrefab, new Vector2((float)int.Parse(jsonData.xCord), (float)int.Parse(jsonData.yCord)), transform.rotation);

                Debug.Log(ObjectToJson(player));
            }
            else
            {
                Debug.Log("Error");
            }
        }
        else
        {
            Debug.Log("Error");
        }
    } 

    private string ObjectToJson(PlayerConstructor accesibleObject)
    {
        return JsonUtility.ToJson(accesibleObject, true);
    }

    private bool CheckName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 10)
            return false;
        else
            return true;
    }

    private bool CheckCord(string cord)
    {
        if (string.IsNullOrWhiteSpace(cord))
            return false;

        int integerCord;

        if (int.TryParse(cord, out integerCord))
            return true;
        else
            return false;
    }

    private bool IsPlayerExists()
    {
        if (player == null)
            return false;
        else
            return true;
    }

    public bool IsValidJson(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return false;

        try
        {
            JToken.Parse(json);
            return true;
        }
        catch (JsonReaderException jex)
        {
            Console.WriteLine(jex.Message);
            return false;
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }

    private bool VariablesValidation(string json)
    {
        Dictionary<string, string> jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

        foreach (KeyValuePair<string, string> variable in jsonData)
        {
            if (!acceptedVariables.Contains(variable.Key))
                return false;          
        }
        return true;
    }
}
