using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    public AudioClip[] footsteps;
    public AudioClip jump;
    public AudioSource adSrc;
    public AudioClip bump;
    int choice;
    // Start is called before the first frame update
    void Start()
    {
        adSrc = GetComponent<AudioSource>();
    }

    public void PlayMusic(int choice)
    {
        switch (choice)
        {
            case 0: adSrc.PlayOneShot(footsteps[Random.Range(0, 1)]); break;
            case 1: adSrc.PlayOneShot(jump); break;
            case 2: adSrc.PlayOneShot(bump); break;

        }
    }

    // public static void PlayS(string choice)
    // {
    //     switch (choice)
    //     {
    //         case "Score": adSrc.PlayOneShot(score); break;
    //         case "Hit": adSrc.PlayOneShot(dead); break;

    //     }
    // }

}
