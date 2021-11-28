using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChallengeModeStageOne()
    {
        SceneManager.LoadScene("Stage1ChallengeMode");
    }
    public void ChallengeModeStageTwo()
    {
        SceneManager.LoadScene("Stage2ChallengeMode");
    }
    public void ChallengeModeStageThree()
    {
        SceneManager.LoadScene("Stage3ChallengeMode");
    }
}
