using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stage3ScriptHandler : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject Player;
    [SerializeField] GameObject SpawnPoint;

    [Header("Player UI")]
    [SerializeField] GameObject DisplayGivenText;
    [SerializeField] GameObject StartTimerText;
    [SerializeField] GameObject TimerFinishText;

    [Header("Game Info")]
    public string correctAnswer = "";

    [Header("Check Points")]
    [SerializeField] public GameObject CheckPointGO;

    public bool isFinish = false;


    // Start is called before the first frame update
    void Start()
    {
        Player = Instantiate(Player);
        Player.transform.position = SpawnPoint.transform.position;
        Stage3GivenHandler.InitializeGiven();
        LoadDefault();
    }

    public void StartGame()
    {
        //LoadDefaults
        //Start ReadyTimerCoroutine
        StartCoroutine(ReadyTimerCoroutine());
    }

    public void GenerateGiven()
    {
        DisplayGivenText.SetActive(true);
        string given = "";
        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
            correctAnswer = "T";
            int size = Stage3GivenHandler.TrueGiven.Count;
            given = Stage3GivenHandler.TrueGiven[Random.Range(0, size)];
        }
        else
        {
            correctAnswer = "F";
            int size = Stage3GivenHandler.FalseGiven.Count;
            given = Stage3GivenHandler.FalseGiven[Random.Range(0, size)];
        }
        DisplayGivenText.GetComponent<TextMeshProUGUI>().text = given;
    }

    IEnumerator ReadyTimerCoroutine()
    {
        StartTimerText.SetActive(true);
        while(int.Parse(StartTimerText.GetComponent<TextMeshProUGUI>().text) > 0)
        {
            if(int.Parse(StartTimerText.GetComponent<TextMeshProUGUI>().text) == 1)
            {
                StopAllCoroutines();
                StartTimerText.SetActive(false);
                //Generate Given
                GenerateGiven();
                //Start Timer Text To finish
                StartCoroutine(TimerFinishTextCoroutine());
                
            }
            int newInt = int.Parse(StartTimerText.GetComponent<TextMeshProUGUI>().text);
            newInt--;
            StartTimerText.GetComponent<TextMeshProUGUI>().text = newInt.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator TimerFinishTextCoroutine()
    {
        TimerFinishText.SetActive(true);
        while (!isFinish)
        {
            int newInt = int.Parse(TimerFinishText.GetComponent<TextMeshProUGUI>().text);
            newInt++;
            TimerFinishText.GetComponent<TextMeshProUGUI>().text = newInt.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    public void LoadDefault()
    {
        SetUiActive(false);
        DisplayGivenText.GetComponent<TextMeshProUGUI>().text = "";
        StartTimerText.GetComponent<TextMeshProUGUI>().text = "4";
        TimerFinishText.GetComponent<TextMeshProUGUI>().text = "0";
    }

    public void SetUiActive(bool status)
    {
        DisplayGivenText.SetActive(status);
        StartTimerText.SetActive(status);
        TimerFinishText.SetActive(status);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Player.transform.position = CheckPointGO.transform.position;
        }
    }

   
}
