using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Stage3GivenHandler
{
    public static List<string> TrueGiven = new List<string>();
    public static List<string> FalseGiven = new List<string>();

    public static void InitializeGiven()
    {
        TrueGiven = new List<string>
        {
            "(T && T) || F",
            "(T || F) || (!T && T)",
            "((T && T) && (F || T)) && T",
            "((F && T) || T) && T",
            "(T || F) || (!F && T)"
        };

        FalseGiven = new List<string>
        {
            "(!(F || T) && T) && (!F && F)",
            "F && ((T && T) || T)",
            "((T || F) && (T && T)) && ((F && T) || !T) ",
            "(((T && T) || T) && T) && ( T && F)",
            "(F && T) || (T && F)"
        };
    }
}
