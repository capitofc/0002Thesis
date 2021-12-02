using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public static SoundScript instance;

    [Header("Sound Effects")]
    [SerializeField] AudioClip ClickFx;
    [SerializeField] AudioClip SuccessfulFx;
    [SerializeField] AudioClip valorant3;

    [SerializeField] AudioSource clickAS;

    private void Start()
    {
        instance = this;
        if (!clickAS.isPlaying)
            clickAS.PlayOneShot(valorant3);
    }

    public void playClickFx()
    {
        clickAS.PlayOneShot(ClickFx);
    }

    public void playSuccessfulFx()
    {
        clickAS.PlayOneShot(SuccessfulFx);
    }


}
