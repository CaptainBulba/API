using System;
using TMPro;
using UnityEngine;

public class ApiController : MonoBehaviour
{
    private LevelController levelController;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Pipeman pipemanController;

    private AccessibleObjects player;

    [SerializeField] TMP_InputField inputField;

    private void Start()
    {
        levelController = LevelController.Instance;
        PlayerPut("Bob", "0", "0");
        PlayerGet();
        inputField.text = ObjectToJson(player);
        IsJson(inputField.text);
    }

    private void PlayerGet()
    {
        Debug.Log(ObjectToJson(player));

        pipemanController.ChangeResponse(ObjectToJson(player));
    }
     
    private void PlayerPut(string name, string xCord, string yCord)
    {
        //Need to add a check if player already exist
        if(CheckName(name) && CheckCord(xCord) && CheckCord(yCord))
        {
            float x = (float)int.Parse(xCord);
            float y = (float)int.Parse(yCord);

            player = new AccessibleObjects(name, int.Parse(xCord), int.Parse(yCord));
            Instantiate(playerPrefab, new Vector2(x, y), transform.rotation);

           // Debug.Log(ObjectToJson(player));
        }
        else
        {
            Debug.Log("Error"); 
        }
    }

    private string ObjectToJson(AccessibleObjects accesibleObject)
    {
        return JsonUtility.ToJson(accesibleObject, true);
    }

    private bool CheckName(string name)
    {
        if (name == null || name == " " || name.Length > 10)
            return false;
        else
            return true;
    }

    private bool CheckCord(string cord)
    {
        int integerCord;

        if (int.TryParse(cord, out integerCord))
            return true;
        else
            return false;
    }

    public bool IsJson(string json)
    {
        if (json == null)
            return false;

        try
        {
            JsonUtility.FromJson<AccessibleObjects>(json);
            Debug.Log(true);
            return true;
        }
        catch (ArgumentException)
        {
            Debug.Log("Nope");
            return false;
        }
    }
}
