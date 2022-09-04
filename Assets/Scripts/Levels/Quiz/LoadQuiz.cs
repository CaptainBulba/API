using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LoadQuiz : MonoBehaviour
{
    private LevelController levelController;

    private string filePreffix = "Quiz/";
    private string questionFileName = "question_";
    private string answerFileName = "answer_";
    private int fileNumber = 0;
    private string fileSuffix = ".txt";

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI answerOneText;
    public TextMeshProUGUI answerTwoText;
    public TextMeshProUGUI answerThreeText;

    private List<string> answersList = new List<string>();

    private void Start()
    {
        levelController = LevelController.Instance;
        LoadSection();
    }

    private void LoadSection()
    {
        string questionFilePath = filePreffix + questionFileName + fileNumber + fileSuffix;
        string answerFilePath = filePreffix + answerFileName + fileNumber + fileSuffix;

        questionText.text = levelController.GetTextFunctions().LoadText(questionFilePath);
        string answersText = levelController.GetTextFunctions().LoadText(answerFilePath);

        answersList = answersText.Split(';').ToList();

        for (int i = answersList.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i);
            string temp = answersList[i];

            answersList[i] = answersList[rnd];
            answersList[rnd] = temp;
        }

        answerOneText.text = answersList[0];
        answerTwoText.text = answersList[1];
        answerThreeText.text = answersList[2];

        fileNumber++;
    }
}
