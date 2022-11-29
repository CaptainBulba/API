using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Endpoints : MonoBehaviour
{
    [HideInInspector] public ApiController apiController;
    [HideInInspector] public Pipeman pipeman;

    protected virtual void Start()
    {
        apiController = ApiController.Instance;
        pipeman = apiController.GetPipeman();
    }

    public bool IsValidToken()
    {
        if (pipeman.GetTokenInput() != apiController.GetToken())
        {
            pipeman.DisplayError(ErrorVariables.Token.ToString(), Errors.WrongToken);
            return false;
        }
        else
            return true;
    }

    public bool IsValidJson(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            pipeman.DisplayError(ErrorVariables.Body.ToString(), Errors.Null);
            return false;
        }

        try
        {
            JToken.Parse(body);
            return true;
        }
        catch (Exception)
        {
            pipeman.DisplayError(ErrorVariables.Body.ToString(), Errors.Null);
            return false;
        }
    }

    public string VariableToLower(Enum variable)
    {
        return variable.ToString().ToLower();
    }

    public bool VariablesValidation(string json, List<string> acceptedVariables)
    {
        Dictionary<string, string> jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

        foreach (KeyValuePair<string, string> variable in jsonData)
        {
            if (!acceptedVariables.Contains(variable.Key, StringComparer.OrdinalIgnoreCase))
            {
                pipeman.DisplayError(variable.Key, Errors.ObjectNotExists);
                return false;
            }
        }
        return true;
    }

    public bool ParametersValidation(Dictionary<string, string> parameters, List<string> acceptedParemeters)
    {
        if(parameters.Count == 0)
        {
            pipeman.DisplayError(ErrorVariables.Parameters.ToString(), Errors.Null);
            return false;
        }
        else
        {
            foreach (KeyValuePair<string, string> param in parameters)
            {
                if (!acceptedParemeters.Contains(param.Key, StringComparer.OrdinalIgnoreCase))
                {
                    pipeman.DisplayError(param.Key, Errors.ObjectNotExists);
                    return false;
                }
            }
            return true;
        }
    }

    public bool CheckPermission(bool endpoint)
    {
        if (endpoint == false)
        {
            pipeman.DisplayError("", Errors.NoPermission);
            return false;
        }
        else
            return true;
    }

    public string EnumToLower(Enum variable)
    {
        return variable.ToString().ToLower();
    }

    public bool isInteger(string variable, string value)
    {
        if (int.TryParse(value, out _))
            return true;
        else
        {
            pipeman.DisplayError(variable, Errors.ObjectNotExists);
            return false;
        }
    }
}
