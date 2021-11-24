using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadUIHandler : MonoBehaviour
{
    [Header("Stage1ScriptHandler")]
    [SerializeField] GameObject stage1Handler;

    public void Restart() 
    {
        stage1Handler.GetComponent<Stage1ScriptHandler>().ResetPosition();
        stage1Handler.GetComponent<Stage1ScriptHandler>().GameDefault();
        stage1Handler.GetComponent<Stage1ScriptHandler>().StartGame();
        gameObject.SetActive(false);
    }

    public void Exit() 
    {
        SceneManager.LoadScene("Main_Menu_Scene");
    }
}
