using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Stage2ScriptHandler : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject Player;
    [SerializeField] GameObject SpawnObject;

    [Header("CheckPoints")]
    [SerializeField] GameObject[] CheckPoints;
    [SerializeField] public GameObject CheckPointGo;

    [Header("Pick Up Spawn Points")]
    [SerializeField] GameObject pickupPointsParent;
    [SerializeField] GameObject[] pickupPoints;

    [Header("Prefabs")]
    [SerializeField] GameObject[] arithmeticPrefab;
    [SerializeField] GameObject symbol;

    [Header("Player UI")]
    [SerializeField] GameObject DisplayGivenText;
    [SerializeField] GameObject GameTimerText;
    [SerializeField] GameObject ReadyTimerText;
    [SerializeField] GameObject CurrentQuestionNumberText;
    [SerializeField] GameObject playerUIDisplay;

    [Header("Game UI")]
    [SerializeField] GameObject correctAnswers;

    [SerializeField] public GameObject Given_timer_AnswerCtr;
    [SerializeField] public GameObject PlayerBtnHandler;
    [SerializeField] public GameObject joyStick;
    [SerializeField] public GameObject pickupButton;


    Dictionary<int, string> given = new Dictionary<int, string>();
    List<string> correctOperator = new List<string>();
    List<string> playerOperator = new List<string>();
    string givenString = "";
    int KeyTotalAnswer = 0;
    int QuestionAnsweredCorrect = 0;
    public bool onCollect = false;
    public bool isDone;
    public bool isFinished;
    public int timeConsumed;
    public int expGained;



    void Start()
    {
        //For testing
        Player = Instantiate(Player, CheckPoints[0].transform);
        Player.GetComponent<Arithmetic_Character_Script>().setPickup(pickupButton);
        PlayerBtnHandler.GetComponent<ButtonsHandler>().setPlayer(Player);
        StartGame();
    }

    public void StartGame()
    {

        //Initialize GameDefault()
        GameDefault();


        PlayerUI(false);
        //Initialize Ready timer -- Coroutine(ReadyTimer())
        StartCoroutine(ReadyTimer());

    }

    IEnumerator ReadyTimer()
    {
        ReadyTimerText.SetActive(true);
        while (int.Parse(ReadyTimerText.GetComponent<TextMeshProUGUI>().text) > 0)
        {
            if (ReadyTimerText.GetComponent<TextMeshProUGUI>().text.Equals("1"))
            {
                StopAllCoroutines();
                ReadyTimerText.SetActive(false);
                //Initalize Game Timer -- Coroutine(GameTimer())
                StartCoroutine(GameTimer());
                //Generate Given -- GenerateGiven()
                PlayerUI(false);
                GenerateGiven();
                //Spawn arithmetic -- spawnArithmetic()
                spawnArithmetic();




            }
            int num = int.Parse(ReadyTimerText.GetComponent<TextMeshProUGUI>().text);
            num--;
            ReadyTimerText.GetComponent<TextMeshProUGUI>().text = num.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator GameTimer()
    {
        // GameTimerText.SetActive(true);
        //DisplayGivenText.SetActive(true);
        // CurrentQuestionNumberText.SetActive(true);
        while (int.Parse(GameTimerText.GetComponent<TextMeshProUGUI>().text) > 0)
        {
            // if (GameTimerText.GetComponent<TextMeshProUGUI>().text.Equals("1"))
            // {
            //     StopAllCoroutines();
            //     GameTimerText.SetActive(false);
            //     //Check if he finish the game

            //     //Reset the game
            // }

            if (isFinished)
            {
                StopAllCoroutines();
                print(timeConsumed);
                print(calculatePoints());
                addPlayerExp();
            }
            int num = int.Parse(GameTimerText.GetComponent<TextMeshProUGUI>().text);
            num++;
            timeConsumed = num;
            GameTimerText.GetComponent<TextMeshProUGUI>().text = num.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    public void GameDefault()
    {
        //DisplayPlayerUi(false);
        ReadyTimerText.GetComponent<TextMeshProUGUI>().text = "4";
        GameTimerText.GetComponent<TextMeshProUGUI>().text = "5";
        DisplayGivenText.GetComponent<TextMeshProUGUI>().text = "";
        CurrentQuestionNumberText.GetComponent<TextMeshProUGUI>().text = "0";

        QuestionAnsweredCorrect = 0;
    }

    public void DisplayPlayerUi(bool status)
    {
        DisplayGivenText.SetActive(status);
        GameTimerText.SetActive(status);
        ReadyTimerText.SetActive(status);
        CurrentQuestionNumberText.SetActive(status);
    }

    public void PlayerUI(bool stat)
    {
        Given_timer_AnswerCtr.SetActive(stat);
        PlayerBtnHandler.SetActive(stat);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            onCollect = !onCollect;
            Debug.Log($"onCollect was changed to {onCollect}");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartGame();
        }
    }

    public void GenerateGiven()
    {
        given = new Dictionary<int, string>
        {
            { 7, "6 + 6 + (2 + 5)  + 2" }, //"6 + 6 - (2 * 5)  / 2"
            // { 8, "1 - 2 * (2 * 2) + 1" },
            // { 20, "5 + 5 - (5 * 5) + 5" },
            // { 16, "1 * 2 * 3 + (1 * 10)" },
            // { 6, "8 + 1 - (10 + 5)" },
            // { 9, "((10 / 2) * (8 / 4) + 5" },
            // { 2, "1 - 6 + 5 / (2 + 3)" },
            // { 5, "(9/3) * 3 + 1 - 5" },
            // { 14, "(21 / 3) + 7 * 1" }
        };
        int[] keys = { 7, 8, 20, 16, 6, 9, 2, 5, 14 };
        // int randomRef = Random.Range(0, keys.Length);
        int randomRef = 0;
        // KeyTotalAnswer = keys[randomRef];
        KeyTotalAnswer = keys[0];
        RemoveArithmetic();
    }

    public void RemoveArithmetic()
    {
        int mercy = 0;
        givenString = "";
        correctOperator = new List<string>();
        char[] givenToChar = given[KeyTotalAnswer].ToCharArray();
        for (int i = 0; i < givenToChar.Length; i++)
        {
            if (givenToChar[i].Equals('+') || givenToChar[i].Equals('-') || givenToChar[i].Equals('/') || givenToChar[i].Equals('*'))
            {
                int ran = Random.Range(0, 2);
                if (ran == 1 && mercy < 2)
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


        DisplayGivenText.GetComponent<TextMeshProUGUI>().text = givenString;
        PlayerUI(false);
        Invoke(nameof(setUi), 0.01f);

        Debug.Log($"Given : {givenString}");
    }

    void setUi()
    {
        PlayerUI(true);
    }

    /*public void spawnArithmetic()
    {
        List<GameObject> arithmeticSpawnPointList = new List<GameObject>();
        //Add all the spawn point
        for(int i = 0; i < pickupPoints.Length; i++)
        {
            arithmeticSpawnPointList.Add(pickupPoints[i]);
        }

        for(int i = 0; i < 12; i++)
        {
            int ran = Random.Range(0, arithmeticSpawnPointList.Count);
            GameObject pref = Instantiate(arithmeticPrefab, pickupPointsParent.transform);
            pref.transform.position = arithmeticSpawnPointList[ran].transform.position;
            arithmeticSpawnPointList.Remove(arithmeticSpawnPointList[ran]);
        }
    }*/

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

            // int arithmeticRan = Random.Range(0, arithmeticPrefab.Length - 1);
            // GameObject pref = Instantiate(arithmeticPrefab[arithmeticRan], pickupPointsParent.transform);
            GameObject pref = Instantiate(symbol, pickupPointsParent.transform);
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
        DisplayGivenText.GetComponent<TextMeshProUGUI>().text = givenString;
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
                correctAnswers.GetComponent<TextMeshProUGUI>().text = "Correct Answers: " + QuestionAnsweredCorrect + "/3";
                //IF CurrentQuestionNumber == 3 Congratulate the player
                if (QuestionAnsweredCorrect == 3)
                {
                    isDone = true;
                    correctAnswers.GetComponent<TextMeshProUGUI>().text = "Run To The Finish Line BOBO!";
                }
                //ELSE Generate another given (Dont forget to clear playerOperator)
                else
                {
                    playerOperator = new List<string>();
                    GenerateGiven();
                }
            }
            //NOT CORRECT
            else
            {
                //Generate another given (Dont forget to clear playerOperator)
                playerOperator = new List<string>();
                GenerateGiven();
            }

        }
    }

    public int calculatePoints()
    {
        expGained = timeConsumed / 3;
        return 40 - expGained;
    }

    public void addexp()
    {
        //GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerCurrentExp += calculatePoints();
        //GameObject.Find("Opening_Game_Script").GetComponent<PlayerExpCalculator>().UpdatePlayerLevel();
    }

    public void addPlayerExp()
    {
        GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerCurrentExp += calculatePoints();
        GameObject.Find("Opening_Game_Script").GetComponent<PlayerExpCalculator>().UpdatePlayerLevel();
        // int points = int.Parse(calculatePoints().ToString());
        GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerMoney += calculatePoints() * 5;
        SaveData.SaveDataProgress(Database.instance);
    }

    public void hideUI()
    {
        playerUIDisplay.SetActive(false);
        Given_timer_AnswerCtr.SetActive(false);
    }
}
