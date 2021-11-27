using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;
using TMPro;

public class Stage1ScriptHandler : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject Player;
    [SerializeField] GameObject[] ModelPrefab;
    [SerializeField] GameObject spawnPoint;
    public bool isDead;
    int points;

    [Header("Environment")]
    [SerializeField] GameObject[] Platforms;
    [SerializeField] GameObject[] TvScreen;

    [Header("Player UI Stats")]
    [SerializeField] GameObject[] QuestionStat;
    [SerializeField] GameObject TimerText;
    [SerializeField] GameObject SolveTimer;


    //Question Info
    int QuestionCount = 1;
    int ResetCoolDown = 4;
    string correctAnswer = "";


    private void Start()
    {
        // Player = Instantiate(ModelPrefab[Database.instance.UsedCharacter], spawnPoint.transform);
        //Disable player movement script

        //FOR TESTING SCRIPT
        Player = Instantiate(Player, spawnPoint.transform);
        isDead = false;
        points = 0;
        GameDefault();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            QuestionCount++;
        }
    }

    public void NewMethod()
    {
        //Before the start of the game
        StartGame();
    }

    public void StartGame()
    {
        if (QuestionCount < 10)
        {
            StopAllCoroutines();
            Debug.Log("Initialized StartGame()");
            TimerText.SetActive(true);
            StartCoroutine(InitializeTimer());
        }
    }

    public void SetGiven()
    {
        Stage1GivenHandler Given = GetComponent<Stage1GivenHandler>().ReturnInstance();
        string[] possibleAnswer = { "bool", "char", "float", "str", "int" };
        correctAnswer = possibleAnswer[Random.Range(0, 5)];
        Debug.Log($"Correct Answer : {correctAnswer}");
        //Add question
        QuestionCount++;
        switch (correctAnswer)
        {
            case "bool":
                SetGiven(Given.GenerateBool());
                break;
            case "char":
                SetGiven(Given.GenerateChar());
                break;
            case "float":
                SetGiven(Given.GenerateFloat());
                break;
            case "str":
                SetGiven(Given.GenerateString());
                break;
            case "int":
                SetGiven(Given.GenerateInt());
                break;
        }
    }

    public void SetGiven(string given)
    {
        for (int i = 0; i < TvScreen.Length; i++)
        {
            TvScreen[i].GetComponent<TextMeshPro>().text = "__ varname =" + given;
        }
    }

    IEnumerator InitializeTimer()
    {
        ResetCoolDown = 4;
        while (int.Parse(TimerText.GetComponent<TextMeshProUGUI>().text) > 0)
        {
            if (TimerText.GetComponent<TextMeshProUGUI>().text.Equals("1"))
            {
                //Stop courotine~
                StopCoroutine(InitializeTimer());
                //Disable timer text
                TimerText.SetActive(false);
                //StartPlayerMovemtn
                SetCharacterProperty(true);
                //Start Generating the given
                SetGiven();
                //Start the timer to solve the given~
                StartCoroutine(InitializeSolveTimer());
            }
            int newInt = int.Parse(TimerText.GetComponent<TextMeshProUGUI>().text) - 1;
            TimerText.GetComponent<TextMeshProUGUI>().text = newInt.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    public void CheckWrongPlatform()
    {
        for (int i = 0; i < Platforms.Length; i++)
        {
            if (!Platforms[i].GetComponent<PlatformScript>().GetValue().Equals(correctAnswer))
            {
                Platforms[i].SetActive(false);
            }
        }
    }

    IEnumerator InitializeSolveTimer()
    {
        SolveTimer.SetActive(true);
        while (int.Parse(SolveTimer.GetComponent<TextMeshProUGUI>().text) > 0)
        {
            if (SolveTimer.GetComponent<TextMeshProUGUI>().text.Equals("1"))
            {
                //Check wrong answer
                CheckWrongPlatform();

                Debug.Log($"{QuestionCount} Question.");
                //Add Exp to Player
                StartCoroutine(givePoints());
                //Disable Player
                SetCharacterProperty(true);
                //Disable Solve Timer
                SolveTimer.SetActive(false);
                //Timer for reseting the game
                StartCoroutine(ResetGameTimer());
                StopCoroutine(InitializeSolveTimer());
            }
            int newInt = int.Parse(SolveTimer.GetComponent<TextMeshProUGUI>().text) - 1;
            SolveTimer.GetComponent<TextMeshProUGUI>().text = newInt.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator givePoints()
    {
        yield return new WaitForSeconds(1f);
        if (!isDead)
            addPlayerExp();

        isDead = false;
    }

    IEnumerator ResetGameTimer()
    {
        while (ResetCoolDown > 0)
        {
            if (ResetCoolDown == 1)
            {
                StopCoroutine(ResetGameTimer());
                //Reset the game
                GameDefault();
                Debug.Log($"Timer Text {TimerText.GetComponent<TextMeshProUGUI>().text} -- SolveTimer {SolveTimer.GetComponent<TextMeshProUGUI>().text}");
                StartGame();
            }
            ResetCoolDown--;
            yield return new WaitForSeconds(1.8f);
        }
    }

    public void SetCharacterProperty(bool status)
    {
        Player.GetComponent<TuxAnimations>().enabled = status;
        Player.GetComponent<SkillControls>().enabled = status;
    }

    public void ResetPosition()
    {
        Player.transform.position = spawnPoint.transform.position;
        isDead = false;
    }

    public void GameDefault()
    {

        if (QuestionCount <= 2)
        {
            //Disable player animation, movement, skills
            SetCharacterProperty(true);

            //Reset Timer Text
            TimerText.GetComponent<TextMeshProUGUI>().text = 4 + "";
            SolveTimer.GetComponent<TextMeshProUGUI>().text = 11 + "";

            //Clear TV Screens
            for (int i = 0; i < TvScreen.Length; i++)
            {
                TvScreen[i].GetComponent<TextMeshPro>().text = "";
            }

            //Reset visibility of all platforms
            for (int i = 0; i < Platforms.Length; i++)
            {
                Platforms[i].SetActive(true);
            }

            //Hide Timer
            TimerText.SetActive(false);
            SolveTimer.SetActive(false);
        }

        else
        {
            Debug.Log("Game End");
        }


    }

    void addPlayerExp()
    {
        Debug.Log("Point Added!");
        points++;
        // GameObject.Find("Opening_Game_Script").GetComponent<Database>().playerCurrentExp += 2;
        // GameObject.Find("Opening_Game_Script").GetComponent<PlayerExpCalculator>().UpdatePlayerLevel();
    }
}
