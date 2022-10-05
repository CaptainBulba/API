using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEndpoints : MonoBehaviour
{
    private PlayerConstructor player;
    private Pipeman pipemanController;

    [SerializeField] private GameObject playerPrefab;

    private List<string> acceptedVariables = new List<string> { PlayerVariables.name.ToString(), PlayerVariables.xCords.ToString() };
    private Dictionary<string, string> jsonData;

    private void Start()
    {
        pipemanController = GetComponent<ApiController>().GetPipeman();

        string json = "{\"name\": \"Bob\", \"xCord\": \"1.1\", \"yCord\": \"1\"}";
        PlayerPut(json);               
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
        if(IsValidJson(json) && IsPlayerExists())
        {
            PlayerConstructor jsonData = JsonConvert.DeserializeObject<PlayerConstructor>(json);

            string name = jsonData.name;
            string x = jsonData.xCord;
            string y = jsonData.yCord;

            if (CheckName(name) && CheckCord(x) && CheckCord(y))
            {
                player = new PlayerConstructor(name, x, y, true);
                Instantiate(playerPrefab, new Vector2((float)int.Parse(jsonData.xCord), (float)int.Parse(jsonData.yCord)), transform.rotation);

                Debug.Log(ObjectToJson(player));
            }
        }
    }

    public void PlayerPost(string json)
    {
        if (IsValidJson(json) && IsPlayerExists() && VariablesValidation(json))
        {
            bool editObject = true;
            string name = null;
            string x = null;
            string y = null;

            Dictionary<string, string> jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            foreach (KeyValuePair<string, string> variable in jsonData)
            {
                if (variable.Key == PlayerVariables.name.ToString())
                {
                    name = variable.Value;
                    if (!CheckName(name))
                        editObject = false;
                }
                if (variable.Key == PlayerVariables.xCords.ToString())
                {
                    x = variable.Value;
                    if (!CheckCord(x))
                        editObject = false;
                }
            }

            if (editObject)
            {
                JsonConvert.PopulateObject(json, player);

                if (x != null || y != null)
                {
                    if (x == null)
                        x = player.xCord;

                    if (y == null)
                        y = player.yCord;

                    // update player position
                }
            }
        }
    }

    private string ObjectToJson(PlayerConstructor json)
    {
        return JsonConvert.SerializeObject(json, Formatting.Indented);
    }

    private bool CheckName(string name)
    {
        Errors error = Errors.None;

        if (string.IsNullOrWhiteSpace(name))
            error = Errors.Null;

        else if (name.Length > 10)
            error = Errors.LongName;

        if (error != Errors.None)
        {
            pipemanController.DisplayError(nameof(name), error);
            return false;
        }
        else
            return true;
    }

    private bool CheckCord(string coordinate)
    {
        Errors error = Errors.None;
        int integerCord;

        if (string.IsNullOrWhiteSpace(coordinate))
            error = Errors.Null;

        else if (int.TryParse(coordinate, out integerCord))
            error = Errors.NotInteger;

        if (error != Errors.None)
        {
            pipemanController.DisplayError(nameof(coordinate), error);
            return false;
        }
        else
            return true;
    }

    private bool IsPlayerExists()
    {
        if (player != null)
        {
            pipemanController.DisplayError(nameof(player), Errors.ObjectExists);
            return false;
        }
        else
            return true;
    }

    public bool IsValidJson(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            pipemanController.DisplayError(nameof(body), Errors.Null);
            return false;
        }

        try
        {
            JToken.Parse(body);
            return true;
        }    
        catch (Exception) 
        {
            pipemanController.DisplayError(nameof(body), Errors.Null);
            return false;
        }
    }

    private bool VariablesValidation(string json)
    {
        Dictionary<string, string> jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

        foreach (KeyValuePair<string, string> variable in jsonData)
        {
            if (!acceptedVariables.Contains(variable.Key))
            {
                pipemanController.DisplayError(variable.Key, Errors.WrongVariable);
                return false;
            }      
        }
        return true;
    }
}
