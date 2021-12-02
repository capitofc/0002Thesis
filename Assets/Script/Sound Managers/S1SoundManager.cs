using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S1SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Clips")]
    [SerializeField] public AudioClip bgm;
    [SerializeField] public AudioClip startingCountdown;
    [SerializeField] public AudioClip Finish;
    [SerializeField] public AudioClip timer;
    public AudioSource adSrc;
    public AudioSource timerAdSrc;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayMusic(int choice)
    {
        switch (choice)
        {
            case 0: adSrc.PlayOneShot(bgm); break;
            case 1: timerAdSrc.PlayOneShot(startingCountdown); break;
            case 2: adSrc.PlayOneShot(Finish); break;
            case 3: adSrc.PlayOneShot(timer); break;

        }
    }
}
