using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerEndpoints : Endpoints
{
    [SerializeField] private GameObject playerPrefab;
    private PlayerConstructor player;

    private List<string> acceptedVariables = Enum.GetNames(typeof(PlayerVariables)).ToList();

    private enum PlayerVariables
    {
        Name,
        X,
        Y
    }

    protected override void Start()
    {
        base.Start();
        //For testing
        pipeman.SetTokeInput(apiController.GetToken());
        string json = "{\"name\": \"Bob\", \"x\": \"-5\", \"y\": \"0\"}";
        PutPlayer(json);
    }

    public void GetPlayer()
    {
        Debug.Log("hey");
        if (IsPlayerExists() && IsValidToken() && CheckPermission(EndpointsPermissions.getPlayer))
        {
            pipeman.ChangeResponse(ObjectToJson(player));
        }
    }

    public void PutPlayer(string json)
    {
        if (IsValidJson(json) && !IsPlayerExists() && IsValidToken() && CheckPermission(EndpointsPermissions.putPlayer))
        {
            PlayerConstructor jsonData = JsonConvert.DeserializeObject<PlayerConstructor>(json);

            string name = jsonData.name;
            string x = jsonData.x;
            string y = jsonData.y;

            if (CheckName(name) && CheckCord(x) && CheckCord(y))
            {
                player = new PlayerConstructor(name, x, y);
                
                GameObject playerObject = Instantiate(playerPrefab, new Vector2((float)int.Parse(x), (float)int.Parse(y)), transform.rotation);
                
                playerObject.SetActive(false);
                playerObject.AddComponent<Player>();

                CreatePlayer create = new CreatePlayer(playerObject, name, int.Parse(x), int.Parse(y));
                apiController.actions.Add(create);
            }
        }
    }

    public void PostPlayer(string json)
    {
        if (IsValidJson(json) && IsPlayerExists() && VariablesValidation(json, acceptedVariables) 
            && IsValidToken() && CheckPermission(EndpointsPermissions.postPlayer))
        {
            bool editObject = true;
            string name = null;
            string x = null;
            string y = null;

            Dictionary<string, string> jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            foreach (KeyValuePair<string, string> variable in jsonData)
            {
                if (variable.Key.ToLower() == GetPlayerVariable(PlayerVariables.Name))
                {
                    name = variable.Value;
                    if (!CheckName(variable.Value))
                        editObject = false;
                }
                if (variable.Key.ToLower() == GetPlayerVariable(PlayerVariables.X))
                {
                    x = variable.Value;
                    if (!CheckCord(x))
                        editObject = false;
                }
                if (variable.Key.ToLower() == GetPlayerVariable(PlayerVariables.Y))
                {
                    y = variable.Value;
                    if (!CheckCord(y))
                        editObject = false;
                }
            }

            if (editObject)
            {
                JsonConvert.PopulateObject(json, player);
                pipeman.ChangeResponse(ObjectToJson(player));

                if (x != null || y != null)
                {
                    if (x == null)
                        x = player.x;

                    if (y == null)
                        y = player.y;

                    MovePlayer move = new MovePlayer(apiController.GetPlayer(), (float)int.Parse(x), (float)int.Parse(y));
                    apiController.actions.Add(move);
                }

                if(name != null)
                {
                    ChangeName changeName = new ChangeName(apiController.GetPlayer(), name);
                    apiController.actions.Add(changeName);
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
            pipeman.DisplayError(ErrorVariables.Name.ToString(), error);
            return false;
        }
        else
            return true;
    }

    private bool CheckCord(string coordinate)
    {
        Errors error = Errors.None;

        if (string.IsNullOrWhiteSpace(coordinate))
            error = Errors.Null;

        else if (!int.TryParse(coordinate, out _))
            error = Errors.NotInteger;

        if (error != Errors.None)
        {
            pipeman.DisplayError(ErrorVariables.Coordinates.ToString(), error);
            return false;
        }
        else
            return true;
    }

    private bool IsPlayerExists()
    {
        if (player != null)
        {
            pipeman.DisplayError(ErrorVariables.Player.ToString(), Errors.ObjectExists);
            return true;
        }
        else
            return false;
    }

    private string GetPlayerVariable(PlayerVariables variable)
    {
        return variable.ToString().ToLower();
    }
}
