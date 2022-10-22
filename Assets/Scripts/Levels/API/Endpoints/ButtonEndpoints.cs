using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonEndpoints : MonoBehaviour
{
    private ApiController apiController;
    private Pipeman pipeman;
    private ButtonConstructor button;
    private EndpointsChecks endpointsChecks;

    private List<string> acceptedVariables = Enum.GetNames(typeof(ButtonVariables)).ToList();

    private void Start()
    {
        apiController = ApiController.Instance;
        pipeman = apiController.GetPipeman();
        SetButton(apiController.GetButtonObject());
        endpointsChecks = GetComponent<EndpointsChecks>();
    }

    public void GetButton()
    {
        if (IsButtonExists())
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
        if (endpointsChecks.IsValidJson(json) && IsButtonExists() &&  endpointsChecks.VariablesValidation(json, acceptedVariables))
        {
            bool editObject = true;
            string pressed;

            Dictionary<string, string> jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            foreach (KeyValuePair<string, string> variable in jsonData)
            {
                if (variable.Key.ToLower() == endpointsChecks.VariableToLower(ButtonVariables.Pressed))
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

                // Add user action to change level 

                //MovePlayer move = new MovePlayer(apiController.GetPlayer(), (float)int.Parse(x), (float)int.Parse(y));
                //apiController.actions.Add(move);
            }
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
