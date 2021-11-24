using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class ModalWindowTrigger : MonoBehaviour
{

    public string title;
    public Sprite sprite;
    public string message;
    public bool TriggerOnEnable;

    public string confirmButtonText;
    public string declineButtonText;
   // public string alternateButtonText;


    public UnityEvent onContinueCallback;
    public UnityEvent onDeclineCallback;
    // public UnityEvent onAlternateCallback;


    public void Start()
    {
       
    }
    public void OnEnable()
    {
        if(TriggerOnEnable) { Debug.Log("Why this is not Opening"); return;  }
        Debug.Log("the first thing that open first");
      

        Action continueCallback = null;
        Action cancelCallback = null; 
      //  Action alternateCallback = null;

        if (onContinueCallback.GetPersistentEventCount() > 0){
            continueCallback = onContinueCallback.Invoke;
            Debug.Log("What this shit do?");
        }
        if (onDeclineCallback.GetPersistentEventCount() > 0)
        {
            cancelCallback = onDeclineCallback.Invoke;
        }

        /* if (onAlternateCallback.GetPersistentEventCount() > 0)
         {
             alternateCallback = onAlternateCallback.Invoke;
         }*/

        UIController.instance.modalWindow.ShowAsHero(title, sprite, message, continueCallback, cancelCallback);



    }

}
