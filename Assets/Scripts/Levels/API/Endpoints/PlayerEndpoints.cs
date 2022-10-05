using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerEndpoints : MonoBehaviour
{
    private PlayerConstructor player;
    private Pipeman pipemanController;

    [SerializeField] private GameObject playerPrefab;

    private List<string> acceptedVariables = Enum.GetNames(typeof(PlayerVariables)).ToList();

    private void Start()
    {
        pipemanController = GetComponent<ApiController>().GetPipeman();

        string json = "{\"name\": \"Bob\", \"coordinateX\": \"1\", \"coordinateY\": \"1\"}";
        PutPlayer(json);
    }

    public void GetPlayer()
    {
        if (IsPlayerExists())
        {
            pipemanController.ChangeResponse(ObjectToJson(player));
        }
    }

    public void PutPlayer(string json)
    {
        if (IsValidJson(json) && !IsPlayerExists())
        {
            PlayerConstructor jsonData = JsonConvert.DeserializeObject<PlayerConstructor>(json);

            string name = jsonData.name;
            string x = jsonData.coordinateX;
            string y = jsonData.coordinateY;

            Debug.Log(x + " " + y);

            if (CheckName(name) && CheckCord(x) && CheckCord(y))
            {
                player = new PlayerConstructor(name, x, y);
                Instantiate(playerPrefab, new Vector2((float)int.Parse(x), (float)int.Parse(y)), transform.rotation);

                Debug.Log(ObjectToJson(player));
            }
        }
    }

    public void PostPlayer(string json)
    {
        if (IsValidJson(json) && IsPlayerExists() &&  VariablesValidation(json))
        {
            bool editObject = true;
            string x = null;
            string y = null;

            Dictionary<string, string> jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            foreach (KeyValuePair<string, string> variable in jsonData)
            {
                Debug.Log((variable.Key.ToLower() == GetPlayerVariable(PlayerVariables.Name)));
                if (variable.Key.ToLower() == GetPlayerVariable(PlayerVariables.Name))
                {
                    if (!CheckName(variable.Key))
                        editObject = false;
                }
                if (variable.Key.ToLower() == GetPlayerVariable(PlayerVariables.CoordinateX))
                {
                    x = variable.Value;
                    if (!CheckCord(x))
                        editObject = false;
                }
                if (variable.Key.ToLower() == GetPlayerVariable(PlayerVariables.CoordinateY))
                {
                    y = variable.Value;
                    if (!CheckCord(y))
                        editObject = false;
                }
            }

            if (editObject)
            {
                JsonConvert.PopulateObject(json, player);
                pipemanController.ChangeResponse(ObjectToJson(player));

                if (x != null || y != null)
                {
                    if (x == null)
                        x = player.coordinateX;

                    if (y == null)
                        y = player.coordinateY;

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

        else if (!int.TryParse(coordinate, out integerCord))
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
            return true;
        }
        else
            return false;
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
            if (!acceptedVariables.Contains(variable.Key, StringComparer.OrdinalIgnoreCase))
            {
                pipemanController.DisplayError(variable.Key, Errors.WrongVariable);
                return false;
            }
        }
        return true;
    }

    private string GetPlayerVariable(PlayerVariables variable)
    {
        return variable.ToString().ToLower();
    }
}
