using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndpointsChecks : MonoBehaviour
{
    private ApiController apiController;
    private Pipeman pipeman;

    private void Start()
    {
        apiController = ApiController.Instance;
        pipeman = apiController.GetPipeman();
    }

    public bool IsValidJson(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            pipeman.DisplayError(nameof(body), Errors.Null);
            return false;
        }

        try
        {
            JToken.Parse(body);
            return true;
        }
        catch (Exception)
        {
            pipeman.DisplayError(nameof(body), Errors.Null);
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
                pipeman.DisplayError(variable.Key, Errors.WrongVariable);
                return false;
            }
        }
        return true;
    }
}
