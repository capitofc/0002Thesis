using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class ButtonsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public SkillControls cast;
    string[] tags = { "Maze", "Trix", "Zilch", "Player" };

    public void castSkill()
    {
        cast.castSkill();
    }

    public void castUltimate()
    {
        cast.castUltimate();
    }

    public void jump()
    {
        player.GetComponent<CharacterKeyboardInput>().jumpButton();
    }
}
