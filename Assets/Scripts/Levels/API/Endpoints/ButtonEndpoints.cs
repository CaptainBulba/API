using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonEndpoints : Endpoints
{
    private ButtonConstructor button;
    private GameObject buttonObject;
    private string buttonName = "button";

    private List<string> acceptedVariables = Enum.GetNames(typeof(ButtonVariables)).ToList();

    private float maxDistance = 0.1f;

    protected override void Start()
    {
        base.Start();
        buttonObject = apiController.GetButtonObject();
        SetButton(buttonObject);
    }

    public void GetButton()
    {
        if (IsButtonExists() && CheckPermission(EndpointsPermissions.getButton))
        {
            pipeman.ChangeResponse(ObjectToJson(button));
        }
    }

    private string ObjectToJson(ButtonConstructor json)
    {
        return JsonConvert.SerializeObject(json, Formatting.Indented);
    }

    private bool IsButtonExists()
    {
        if (button != null)
            return true;
        else
        {
            pipeman.DisplayError(nameof(button), Errors.ObjectExists);
            return false;
        }
    }

    public void PostButton(string json)
    {
        if (IsValidJson(json) && IsButtonExists() 
            && VariablesValidation(json, acceptedVariables) 
            && CheckDistance() && CheckPermission(EndpointsPermissions.postButton))
        {
            bool editObject = true;
            string pressed;

            Dictionary<string, string> jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            foreach (KeyValuePair<string, string> variable in jsonData)
            {
                if (variable.Key.ToLower() == VariableToLower(ButtonVariables.Pressed))
                {
                    pressed = variable.Value;
                    if (!CheckPress(pressed))
                        editObject = false;
                }
            }

            if (editObject)
            {
                JsonConvert.PopulateObject(json, button);
                pipeman.ChangeResponse(ObjectToJson(button));

                PressedButton pressedButton = new PressedButton(buttonObject);
                apiController.actions.Add(pressedButton);
            }
        }
    }

    private bool CheckDistance()
    {

        if (Vector2.Distance(buttonObject.transform.position, apiController.GetPlayer().transform.position) <= maxDistance)
            return true;
        else
        {
            pipeman.DisplayError(buttonName, Errors.TooFar);
            return false;
        }
    }

    private bool CheckPress(string pressed)
    {
        Errors error = Errors.None;

        if (string.IsNullOrWhiteSpace(pressed))
            error = Errors.Null;

        else if (!bool.TryParse(pressed, out bool result))
            error = Errors.WrongValue;

        if (error != Errors.None)
        {
            pipeman.DisplayError(nameof(pressed), error);
            return false;
        }
        else
            return true;
    }

    public void SetButton(GameObject buttonObject)
    {
        button = new ButtonConstructor((buttonObject.transform.position.x - 0.5f).ToString(), (buttonObject.transform.position.y - 0.5f).ToString());
    }
}
