using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialScript : MonoBehaviour
{
    public Text tutsText;
    private string[] tutScript = {"Hello! I'm Tux, a coding penguin. Welcome to Northern Lights.",
                                "In Northern Lights, you will learn about the Basic Computer Programming Elements.",
                                "But first, I will teach you how to control your character.",
                                "Use the joystick to move to the green platform.", //3
                                "Good Job! Now, move to the red platform.", //4
                                "Very Well. You will see some floating objects above.",
                                "Collect them, and you will gain some additional speed.",
                                "Jumping above platforms will help you, press SPACE to JUMP.", //7
                                "As you can see, you gain additional speed after collecting a power-up.",
                                "Please proceed to the pink platform.", //9
                                "You will need some penguin skills to win some obstacles.",
                                "Using these skills will help you slow down or knockback your enemies.",
                                "Press E to use your first skill.", //12
                                "Very well! In order to use your ULTIMATE SKILL, you have to collect an ULTI-POINT.",
                                "Now, press Q to use your ULTIMATE SKILL.", //14
                                "Good job! I guess you are now ready to conquer and learn in Northern Lights."};
    public static int count;
    public static bool waitNext = false;
    public float waitTime = 2f;


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < tutScript.Length; i++)
        {
            if (i == count)
                tutsText.text = tutScript[i];
            //else
            //tutsText[i].gameObject.SetActive(false);
        }

        if (count == 3 || count == 4 || count == 7 || count == 9 || count == 12 || count == 14)
            waitNext = true;

        if (Input.GetKeyDown(KeyCode.E) && count == 12)
            addCount();
        else if (Input.GetKeyDown(KeyCode.Q) && count == 14)
            addCount();
        else if (Input.GetKeyDown(KeyCode.Space) && count == 7)
            addCount();
        else if (Input.GetKeyDown(KeyCode.Tab) && waitNext == false) //&& count == 2
            addCount();
    }

    public static void next(GameObject other)
    {
        if (other.gameObject.tag == "Blue" && count == 4)
            addCount();
        else if (other.gameObject.tag == "Green" && count == 3)
            addCount();
        else if (other.gameObject.tag == "Pink" && count == 9)
            addCount();

    }

    public static void addCount()
    {
        count++;
        waitNext = false;
    }
}
