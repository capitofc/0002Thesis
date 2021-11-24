using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTweening : MonoBehaviour
{
    // Start is called before the first frame update

    //public Vector3 to;

    public float targetYaxis;
    public LeanTweenType easeType;
    public float duration = 1f;
    public float delay = 0f;


    private void OnEnable()
    {
        LeanTween.move(gameObject, targetYMovement(), duration).setDelay(delay).setLoopPingPong().setEase(easeType);
    }

    private Vector3 targetYMovement()
    {
        Vector3 movement = new Vector3(transform.position.x, targetYaxis, transform.position.z);
        return movement;
    }
}
