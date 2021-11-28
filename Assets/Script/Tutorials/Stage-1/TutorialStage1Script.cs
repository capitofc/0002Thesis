using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialStage1Script : MonoBehaviour
{
    [Header("Questionaire Text")]
    [SerializeField] GameObject[] QuestionText;

    [Header("Platforms")]
    [SerializeField] GameObject[] Platforms;



    [SerializeField]
    private string[] given = { "What is the number used to count a human population?",
                "What is the data type used to store a cents in peso?", "boolean", "float", "char"};


    public string correctAnswer = "";

    public string[] key = { "int", "float", "char", "str", "bool" };

    public bool isGame = false;


    [SerializeField] GameObject[] enableOnCorrect;
    [SerializeField] GameObject successDisplay;
    [SerializeField] GameObject CongratulationDisplay;
    [SerializeField] GameObject controls;
    [SerializeField] GameObject myCamera;
    [SerializeField] GameObject mazeChar;

    public void SetGive(int givenType)
    {
        correctAnswer = key[givenType];
        Debug.Log(correctAnswer);
        for (int i = 0; i < QuestionText.Length; i++)
        {
            QuestionText[i].GetComponent<TextMeshPro>().text = given[givenType];
        }
        isGame = true;
    }

    public void setSucessMessage()
    {
        for (int i = 0; i < QuestionText.Length; i++)
        {
            QuestionText[i].GetComponent<TextMeshPro>().text = "SUCESS YOU UNDERSTAND IT WELL!";
        }
        successDisplay.SetActive(true);
    }

    public void StartGame(int givenType)
    {
        ResetDefault();
        SetGive(givenType);
    }

    public void ResetDefault()
    {
        ResetPlatforms();
        ResetQuestionText();
        isGame = false;
        correctAnswer = "";
    }

    public void ResetPlatforms()
    {
        for (int i = 0; i < Platforms.Length; i++)
        {
            Platforms[i].SetActive(true);
        }
    }

    public void ResetQuestionText()
    {
        for (int i = 0; i < QuestionText.Length; i++)
        {
            QuestionText[i].GetComponent<TextMeshPro>().text = "";
        }
    }

    public void DisableWrongAnswer()
    {
        Debug.Log(correctAnswer);
        for (int i = 0; i < Platforms.Length; i++)
        {
            if (!Platforms[i].GetComponent<TutorialStage1Platform>().value.Equals(correctAnswer))
            {
                Platforms[i].SetActive(false);
            }
        }

        setSucessMessage();
        controls.SetActive(false);
        StartCoroutine(sucessCountdown());


    }

    IEnumerator sucessCountdown()
    {
        yield return new WaitForSeconds(3f);
        enableTriggerForConversation(correctAnswer);
    }

    public void enableTriggerForConversation(string value)
    {
        StopAllCoroutines();
        successDisplay.GetComponent<UITweener>().Disable();
        string answer = value;
        switch (answer)
        {
            case "int":
               enableOnCorrect[0].SetActive(true);
                ///enable quest trigger, conversation trigger
                break;
            case "str": ///enable quest trigger, conversation trigger
                enableOnCorrect[3].SetActive(true);
                break;
            case "float": ///enable quest trigger, conversation trigger
                enableOnCorrect[1].SetActive(true);
                break;
            case "bool": ///enable quest trigger, conversation trigger
                CongratulationDisplay.SetActive(true);
                controls.SetActive(false);
                myCamera.SetActive(false);
                break;
            case "char": ///enable quest trigger, conversation trigger
                enableOnCorrect[2].SetActive(true);
                break;

        }

    }

}
