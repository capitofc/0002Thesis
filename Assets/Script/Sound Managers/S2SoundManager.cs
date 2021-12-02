using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2SoundManager : MonoBehaviour
{
    public AudioClip collect;
    public AudioClip finish;
    public AudioClip timer;
    public AudioClip startTimer;
    public AudioClip correct;
    public AudioClip wrong;
    public AudioSource adSrc;
    public AudioSource timerAdSrc;
    public AudioSource checker;
    // Start is called before the first frame update
    public void PlayMusic(int choice)
    {
        switch (choice)
        {
            case 0: adSrc.PlayOneShot(collect); break;
            case 1: timerAdSrc.PlayOneShot(startTimer); break;
            case 2: adSrc.PlayOneShot(finish); break;
            case 3: timerAdSrc.PlayOneShot(timer); break;
            case 4: checker.PlayOneShot(correct); break;
            case 5: checker.PlayOneShot(wrong); break;

        }
    }
}
