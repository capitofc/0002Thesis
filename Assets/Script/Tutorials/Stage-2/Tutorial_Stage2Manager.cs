using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial_Stage2Manager : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Pick Up Spawn Points")]
    [SerializeField] GameObject pickupPointsParent;
    [SerializeField] GameObject[] pickupPoints;


    [Header("Prefabs")]
    [SerializeField] public GameObject []arithmeticPrefab;


    [Header("Player UI")]
    [SerializeField] GameObject DisplayGivenText;
    [SerializeField] GameObject CurrentQuestionNumberText;


    [Header("CheckPoints")]
    [SerializeField] GameObject[] CheckPoints;
    [SerializeField] public GameObject CheckPointGo;


    Dictionary<int, string> given = new Dictionary<int, string>();

    List<string> correctOperator = new List<string>();

    List<string> playerOperator = new List<string>();


    public GameObject wrongAnswerFirstGiven;
    public GameObject wrongAnswer2ndGivenTryAgain;
    public GameObject CorrectAnswerFirstGiven;
    public GameObject CorrectAnswer2ndGiven;

    string givenString = "";
    int KeyTotalAnswer = 0;
    public bool onCollect = false;
    int QuestionAnsweredCorrect = 0;

    // Update is called once per frame
    public void allowPickUp()
    {
        onCollect = !onCollect;
    }
    
    

    public void StartGame(int given)
    {
        GameDefault();
        GenerateGiven(given);
        spawnArithmetic();
    
        //Initialize Ready timer -- Coroutine(ReadyTimer())
    }



    public void GameDefault()
    {
        DisplayGivenText.GetComponent<TextMeshProUGUI>().text = "";
        CurrentQuestionNumberText.GetComponent<TextMeshProUGUI>().text = "0";
        QuestionAnsweredCorrect = 0;
    }

    public void GenerateGiven(int keyNumber)
    {
        given = new Dictionary<int, string>
        {
            { 8, "((7-5) + 4) + 2 = 8)" },
            { 2, " (2 * 3) % 4 = 2"}
        };

        int[] keys = {8,2};
        KeyTotalAnswer = keys[keyNumber];
        RemoveArithmetic();

    }

    public void RemoveArithmetic()
    {
        /// removing arithmetic
        int mercy = 0; 
        givenString = "";
        correctOperator = new List<string>();
        char[] givenToChar = given[KeyTotalAnswer].ToCharArray();

        for (int i = 0; i < givenToChar.Length; i++)
        {
            if (givenToChar[i].Equals('+') || givenToChar[i].Equals('-') || givenToChar[i].Equals('/') || givenToChar[i].Equals('*') || givenToChar[i].Equals("%"))
            {
                if (mercy < 1)
                {
                    mercy++;
                    givenString += givenToChar[i].ToString();
                }
                else
                {
                    correctOperator.Add(givenToChar[i].ToString());
                    givenString += "_";
                }

            }
            else
            {
                givenString += givenToChar[i].ToString();
            }
        }

        givenString += "=" + KeyTotalAnswer;
        DisplayGivenText.GetComponent<TextMeshProUGUI>().text = "Given:" + givenString;
        Debug.Log($"Given : {givenString}");
    }

    public void spawnArithmetic()
    {
        List<GameObject> arithmeticSpawnPointList = new List<GameObject>();
        //Add all the spawn point
        for (int i = 0; i < pickupPoints.Length; i++)
        {
            arithmeticSpawnPointList.Add(pickupPoints[i]);
        }

        for (int i = 0; i < 12; i++)
        {
            //int ran = Random.Range(0, arithmeticSpawnPointList.Count);

            int arithmeticRan = Random.Range(0, arithmeticPrefab.Length - 1);
            GameObject pref = Instantiate( arithmeticPrefab[arithmeticRan], pickupPointsParent.transform);
            pref.transform.position = arithmeticSpawnPointList[i].transform.position;
            arithmeticSpawnPointList.Remove(arithmeticSpawnPointList[i]);
        }
    }

    public void UpdateGivenText(string arithmetic)
    {
        string newGiven = "";
        bool isAdded = false;
        char[] given = givenString.ToCharArray();
        for (int i = 0; i < given.Length; i++)
        {
            if (given[i].ToString().Equals("_") && isAdded == false)
            {
                isAdded = true;
                newGiven += arithmetic;
            }
            else
            {
                newGiven += given[i];
            }
        }
        givenString = newGiven;
        DisplayGivenText.GetComponent<TextMeshProUGUI>().text = "Given: " + givenString;
        playerOperator.Add(arithmetic);

        //check if the player collect all the operator
        if (playerOperator.Count == correctOperator.Count)
        {
            //ELSE check if the player is correct
            bool isCorrect = true;
            for (int i = 0; i < correctOperator.Count; i++)
            {
                if (!playerOperator[i].Equals(correctOperator[i]))
                {
                    isCorrect = false;
                    break;
                }
            }

            //CORRECT
            if (isCorrect)
            {
                QuestionAnsweredCorrect++;
                //IF CurrentQuestionNumber == 3 Congratulate the player
                if (QuestionAnsweredCorrect == 1)
                {
                    //proceed to next Question
                    //GenerateGiven(QuestionAnsweredCorrect);
                    //DisplayGivenText.GetComponent<TextMeshProUGUI>().text = "Great! Run as fast as you can in the finish line!";
                    CorrectAnswerFirstGiven.SetActive(true);
                    Debug.Log("Tama ang sagot mo 1 sa proceed tayo sa 2");
                }
                if (QuestionAnsweredCorrect == 2)
                {
                    UpdateGivenText(given[1].ToString());
                    ///congrats to tutoorial
                    
                }
                //ELSE Generate another given (Dont forget to clear playerOperator)
       
            }
            //NOT CORRECT
            else
            {
                //Generate another given (Dont forget to clear playerOperator)
                playerOperator = new List<string>();
     

                if(QuestionAnsweredCorrect == 0)
                {
                    wrongAnswerFirstGiven.SetActive(true);
                    
                }
                else if(QuestionAnsweredCorrect == 1)
                {
                    wrongAnswer2ndGivenTryAgain.SetActive(true);
                }

                tryAgainMessage();
            }

        }
    }

    public void tryAgainMessage()
    {
        //try again canvas
        //set conversation to question dialouge
        //set quest again
        //wrong answer try again pop up
        Debug.Log("Wrong answer Please try Again");

        GameDefault();
        //spawnArithmetic();


    }





}
