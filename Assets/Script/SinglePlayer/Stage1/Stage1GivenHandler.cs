using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stage1GivenHandler : MonoBehaviour
{
    public static Stage1GivenHandler instance;
    List<string> StringGiven = new List<string>();

    private void Start()
    {
        StringGiven.Add("Hello");
        StringGiven.Add("Fernan");
        StringGiven.Add("I am a integer");
        StringGiven.Add("Float");
        StringGiven.Add("number variable");
        StringGiven.Add("True");
        StringGiven.Add("Z");
        StringGiven.Add("Not a string");
        StringGiven.Add("Null");
        StringGiven.Add("Message");
        StringGiven.Add("Baguio");
        StringGiven.Add("PNC");
        StringGiven.Add("World of war");
        StringGiven.Add("Graudation");
        StringGiven.Add("character");
        StringGiven.Add("Programming Language");
        StringGiven.Add("Dondon Gecolea");
        StringGiven.Add("PNC");
        StringGiven.Add("Ethics");
        StringGiven.Add("Nino");
        instance = this;
    }

    public Stage1GivenHandler ReturnInstance()
    {
        return instance;
    }

    public string GenerateString() 
    {
        return StringGiven[Random.Range(0, StringGiven.Count)];
    }

    public string GenerateInt() 
    {
        return Random.Range(0, 50) + "";
    }

    public string GenerateFloat()
    {
        return float.MaxValue * 2.0 * (Random.Range(0, 100) - 0.5) + "";
    }

    public string GenerateBool()
    {
        int ran = Random.Range(0, 1);
        if(ran == 1)
        {
            return "true";
        }
        else
        {
            return "false";
        }
    }

    public string GenerateChar()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        int ran = Random.Range(0, chars.Length);
        return chars[ran] + "";
    }
}
