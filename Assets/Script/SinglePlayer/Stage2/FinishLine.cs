using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] GameObject endgame;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Zilch"))
        {
            if (GameObject.Find("Stage2Handler").GetComponent<Stage2ScriptHandler>().isDone)
            {
                // GameObject.Find("Stage2Handler").GetComponent<Stage2ScriptHandler>().addPlayerExp();
                GameObject.Find("Stage2Handler").GetComponent<Stage2ScriptHandler>().isFinished = true;
                GameObject.Find("Stage2Handler").GetComponent<Stage2ScriptHandler>().hideUI();

                endgame.SetActive(true);
            }
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene("Main_Menu_Scene");
    }
}
