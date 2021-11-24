using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class ButtonsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    SkillControls cast;
    string[] tags = { "Maze", "Trix", "Zilch", "Player" };
    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Maze");
        // if (player == null)
        //     player = GameObject.FindGameObjectWithTag("Trix");
        // if (player == null)
        //     player = GameObject.FindGameObjectWithTag("Zilch");
        // if (player == null)
        //     player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            for (int i = 0; i < tags.Length; i++)
            {
                player = GameObject.FindGameObjectWithTag(tags[i]);
                if (player != null)
                    break;
            }

        }

        cast = player.GetComponent<SkillControls>();
    }
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
        player.GetComponent<AdvancedWalkerController>().jump();
    }
}
