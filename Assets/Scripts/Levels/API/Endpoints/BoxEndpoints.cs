using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoxEndpoints : Endpoints
{
    private List<BoxConstructor> boxes = new List<BoxConstructor>();
    private List<string> acceptedVariables = Enum.GetNames(typeof(BoxVariables)).ToList();

    [SerializeField] private GameObject boxPrefab;
 
    private enum BoxVariables
    {
        X,
        Y,
        ActiveFlag
    }

    private enum BoxParameters
    {
        Id
    }

    protected override void Start()
    {
        base.Start();
        FindAllBoxes();

        //Testing
        string json = "{\"x\": \"4\", \"y\": \"0\"}";
        PutBox(json);
        json = "{\"x\": \"5\", \"y\": \"1\", \"activeFlag\": false}";
        PostBox(json);

    }

    private void FindAllBoxes()
    {
        foreach (GameObject box in GameObject.FindGameObjectsWithTag(ObjectsTags.Box.ToString()))
        {
            string x = (box.transform.position.x - 0.5f).ToString();
            string y = (box.transform.position.y - 0.5f).ToString();

            BoxConstructor boxConstructor = new BoxConstructor(box, x, y);
            boxes.Add(boxConstructor);
        }
    }

    public void GetAllBoxes()
    {
        if (AnyBoxExists() && IsValidToken() && CheckPermission(EndpointsPermissions.getAllBoxes))
            pipeman.ChangeResponse(JsonConvert.SerializeObject(boxes, Formatting.Indented));
    }

    public void GetBox()
    {
        if (AnyBoxExists() && IsValidToken() && CheckPermission(EndpointsPermissions.getBox))
        {
            List<string> acceptedParameters = new List<string> { BoxParameters.Id.ToString() };

            Dictionary<string, string> parameters = pipeman.GetParameters();

            if (ParametersValidation(parameters, acceptedParameters))
            {
                int boxId = 0;
                bool displayObject = false;
                foreach (KeyValuePair<string, string> variable in parameters)
                {
                    if (variable.Key.ToLower() == EnumToLower(BoxParameters.Id) && isInteger(EnumToLower(BoxParameters.Id), variable.Value))
                    {
                        if (IsBoxExists(int.Parse(variable.Value)))
                        {
                            boxId = int.Parse(variable.Value);
                            displayObject = true;
                        }
                    }
                }
                if (displayObject)
                    pipeman.ChangeResponse(ObjectToJson(boxes[boxId]));
            }
        }
    }

    public void PutBox(string json)
    {
        if (IsValidJson(json) && IsValidToken() && CheckPermission(EndpointsPermissions.putBox))
        {
            BoxConstructor jsonData = JsonConvert.DeserializeObject<BoxConstructor>(json);

            string x = jsonData.x;
            string y = jsonData.y;

            if (CheckCord(x) && CheckCord(y))
            {
                GameObject boxObject = Instantiate(boxPrefab, new Vector2((float)int.Parse(x), (float)int.Parse(y)), transform.rotation);
                boxObject.SetActive(false);

                BoxConstructor boxConstructor = new BoxConstructor(boxObject, x, y);
                boxes.Add(boxConstructor);

                CreateBox create = new CreateBox(boxObject, int.Parse(x), int.Parse(y));
                apiController.actions.Add(create);
            }
        }
    }

    public void PostBox(string json)
    {
        if (IsValidJson(json) && AnyBoxExists() && VariablesValidation(json, acceptedVariables)
            && IsValidToken() && CheckPermission(EndpointsPermissions.postBox))
        {
            List<string> acceptedParameters = new List<string> { BoxParameters.Id.ToString() };

            Dictionary<string, string> parameters = pipeman.GetParameters();

            bool editObject = false;
            string activeFlag = null;
            string x = null;
            string y = null;
            int boxId = 0;

            Dictionary<string, string> jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            foreach (KeyValuePair<string, string> variable in jsonData)
            {
                if (variable.Key.ToLower() == EnumToLower(BoxVariables.ActiveFlag))
                {
                    if (CheckActiveFlag(variable.Value))
                    {
                        editObject = true;
                        activeFlag = variable.Value;
                    }
                }
                if (variable.Key.ToLower() == EnumToLower(BoxVariables.X))
                {
                    if (CheckCord(variable.Value))
                    {
                        editObject = true;
                        x = variable.Value;
                    }
                }
                if (variable.Key.ToLower() == EnumToLower(BoxVariables.Y))
                {
                    if (CheckCord(variable.Value))
                    {
                        editObject = true;
                        y = variable.Value;
                    }
                }
            }

            if (ParametersValidation(parameters, acceptedParameters))
            {
                foreach (KeyValuePair<string, string> variable in parameters)
                {
                    if (variable.Key.ToLower() == EnumToLower(BoxParameters.Id) && isInteger(EnumToLower(BoxParameters.Id), variable.Value))
                    {
                        if (IsBoxExists(int.Parse(variable.Value)))
                        {
                            boxId = int.Parse(variable.Value);
                            editObject = true;
                        }
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

                        MoveBox move = new MoveBox(boxes[boxId].GetBoxObject(), int.Parse(x), int.Parse(y));
                        apiController.actions.Add(move);
                    }

                    if (activeFlag != null)
                    {
                        ActiveFlagBox flag = new ActiveFlagBox(boxes[boxId].GetBoxObject(), bool.Parse(activeFlag));
                        apiController.actions.Add(flag);
                    }
                }
            }
        }
    }

    public void DeleteBox(int boxId)
    {
        if (AnyBoxExists() && IsValidToken() && CheckPermission(EndpointsPermissions.deleteBox))
        {
            pipeman.ChangeResponse(ObjectToJson(boxes[boxId]));

            DeleteBox flag = new DeleteBox(boxes[boxId].GetBoxObject());
            apiController.actions.Add(flag);
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

    private string ObjectToJson(BoxConstructor json)
    {
        return JsonConvert.SerializeObject(json, Formatting.Indented);
    }

    private bool AnyBoxExists()
    {
        if (boxes.Count != 0)
        {
            //TODO list is empty error
            pipeman.DisplayError(ErrorVariables.Box.ToString(), Errors.ObjectNotExists);
            return true;
        }
        else
            return false;
    }

    private bool IsBoxExists(int boxId)
    {
        if (boxes.ElementAtOrDefault(boxId) != null)
            return true;
        else
        {
            pipeman.DisplayError(ErrorVariables.Box.ToString(), Errors.ObjectNotExists);
            return false;
        }
    }
}
