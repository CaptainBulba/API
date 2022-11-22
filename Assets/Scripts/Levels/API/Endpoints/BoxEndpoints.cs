using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoxEndpoints : MonoBehaviour
{
    //TODO: Create inheritance of main Endpoint class to avoid recreating

    private ApiController apiController;
    private Pipeman pipeman;
    private EndpointsChecks endpointsChecks;

    private List<BoxConstructor> boxes = new List<BoxConstructor>();
    private List<string> acceptedVariables = Enum.GetNames(typeof(BoxVariables)).ToList();

    [SerializeField] private GameObject boxPrefab;

    public enum BoxVariables
    {
        X,
        Y,
        ActiveFlag
    }


    private void Start()
    {
        apiController = GetComponent<ApiController>();
        pipeman = apiController.GetPipeman();
        endpointsChecks = GetComponent<EndpointsChecks>();

        FindAllBoxes();

        string json = "{\"x\": \"4\", \"y\": \"0\"}";
        PutBox(json);
        json = "{\"x\": \"5\", \"y\": \"1\", \"activeFlag\": false}";
        PostPlayer(0, json);
    }

    private void FindAllBoxes()
    {
        foreach (GameObject box in GameObject.FindGameObjectsWithTag(ObjectsTags.Box.ToString()))
        {
            string x = (box.transform.position.x - 0.5f).ToString();
            string y = (box.transform.position.y - 0.5f).ToString();

            BoxConstructor boxConstructor = new BoxConstructor(x, y);
            boxes.Add(boxConstructor);

            GetBox(0);
        }
    }

    public void GetAllBoxes()
    {
        if (IsBoxExists() && endpointsChecks.IsValidToken() && endpointsChecks.CheckPermission(EndpointsPermissions.getAllBoxes))
            pipeman.ChangeResponse(JsonConvert.SerializeObject(boxes, Formatting.Indented));
    }

    public void GetBox(int boxId)
    {
        if (IsBoxExists() && endpointsChecks.IsValidToken() && endpointsChecks.CheckPermission(EndpointsPermissions.getBox))
            pipeman.ChangeResponse(ObjectToJson(boxes[boxId]));
    }

    public void PutBox(string json)
    {
        if (endpointsChecks.IsValidJson(json) && endpointsChecks.IsValidToken() && endpointsChecks.CheckPermission(EndpointsPermissions.putBox))
        {
            BoxConstructor jsonData = JsonConvert.DeserializeObject<BoxConstructor>(json);

            string x = jsonData.x;
            string y = jsonData.y;

            if (CheckCord(x) && CheckCord(y))
            {
                BoxConstructor boxConstructor = new BoxConstructor(x, y);
                boxes.Add(boxConstructor);

                GameObject boxObject = Instantiate(boxPrefab, new Vector2((float)int.Parse(x), (float)int.Parse(y)), transform.rotation);
                boxObject.AddComponent<Box>();
                boxObject.SetActive(false);

                CreateBox create = new CreateBox(boxObject, int.Parse(x), int.Parse(y));
                apiController.actions.Add(create);
            }
        }
    }

    public void PostPlayer(int boxId, string json)
    {
        if (endpointsChecks.IsValidJson(json) && IsBoxExists() && endpointsChecks.VariablesValidation(json, acceptedVariables)
            && endpointsChecks.IsValidToken() && endpointsChecks.CheckPermission(EndpointsPermissions.postBox))
        {
            bool editObject = true;
            string activeFlag = null;
            string x = null;
            string y = null;

            Dictionary<string, string> jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            foreach (KeyValuePair<string, string> variable in jsonData)
            {
                if (variable.Key.ToLower() == GetBoxVariable(BoxVariables.ActiveFlag))
                {
                    if (!CheckActiveFlag(variable.Value))
                        editObject = false;

                    activeFlag = variable.Value;
                }
                if (variable.Key.ToLower() == GetBoxVariable(BoxVariables.X))
                { 
                    if (!CheckCord(variable.Value))
                        editObject = false;

                    x = variable.Value;
                }
                if (variable.Key.ToLower() == GetBoxVariable(BoxVariables.Y))
                {
                    if (!CheckCord(variable.Value))
                        editObject = false;     

                    y = variable.Value;
                }
            }

            if (editObject)
            {
                JsonConvert.PopulateObject(json, boxes[boxId]);
                pipeman.ChangeResponse(ObjectToJson(boxes[boxId]));

                if (x != null || y != null)
                {
                    if (x == null)
                        x = boxes[boxId].x;

                    if (y == null)
                        y = boxes[boxId].y;

                    //TODO: add move box functionality

                    //MovePlayer move = new MovePlayer(apiController.GetPlayer(), (float)int.Parse(x), (float)int.Parse(y));
                   // apiController.actions.Add(move);
                }

                Debug.Log(boxes[boxId].x + boxes[boxId].y + boxes[boxId].activeFlag);
            }
        }
    }

    private bool CheckActiveFlag(string flag)
    {
        Errors error = Errors.None;

        if (string.IsNullOrWhiteSpace(flag))
            error = Errors.Null;

        else if (!Boolean.TryParse(flag, out _))
            error = Errors.NotInteger;

        if (error != Errors.None)
        {
            pipeman.DisplayError(BoxVariables.ActiveFlag.ToString(), error);
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
            pipeman.DisplayError(ErrorVariables.Coordinates.ToString(), error);
            return false;
        }
        else
            return true;
    }

    private string ObjectToJson(BoxConstructor json)
    {
        return JsonConvert.SerializeObject(json, Formatting.Indented);
    }

    private bool IsBoxExists()
    {
        if (boxes.Count != 0)
        {
            //TODO list is empty error
            pipeman.DisplayError(ErrorVariables.Player.ToString(), Errors.ObjectExists);
            return true;
        }
        else
            return false;
    }

    private string GetBoxVariable(Enum variable)
    {
        return variable.ToString().ToLower();
    }
}
