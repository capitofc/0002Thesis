using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public static SoundScript instance;

    [Header("Sound Effects")]
    [SerializeField] AudioClip ClickFx;
    [SerializeField] AudioClip SuccessfulFx;

    [SerializeField] AudioSource clickAS;

    private void Start()
    {
        instance = this;
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
