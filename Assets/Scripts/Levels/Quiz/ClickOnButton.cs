using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickOnButton : MonoBehaviour
{
    private LevelController levelController;
    private LoadQuiz loadQuiz;

    private string filePreffix = "Quiz/";
    private string fileName = "correct_";
    private int fileNumber = 0;
    private string fileSuffix = ".txt";
    private string filePath;
    private int filesAmount;

    private void Start()
    {
        levelController = LevelController.Instance;
        loadQuiz = GameObject.FindObjectOfType<LoadQuiz>();
        filePath = filePreffix + fileName + fileNumber + fileSuffix;
        filesAmount = levelController.GetTextFunctions().CheckFilesNumber(filePreffix);
    }

    public void CheckAnswer(GameObject button)
    {
        
        string correctAnswer = levelController.GetTextFunctions().LoadText(filePath);

        if (button.GetComponent<TextMeshProUGUI>().text == correctAnswer)
        {
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("Not correct");
        }
    }
}
