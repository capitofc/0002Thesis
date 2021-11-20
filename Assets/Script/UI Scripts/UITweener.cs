using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;



public enum UIanimationTypes
{
    Move,
    Scale,
    ScaleX,
    ScaleY,
    Fade
}
public class UITweener : MonoBehaviour
{
    public GameObject objectToAnimate;
    public UIanimationTypes animType;
    public LeanTweenType easeType;
    public float duration;
    public float delay;

    public bool loop;
    public bool pingpong;

    public bool startPositionOffset;
    public Vector3 from;
    public Vector3 to;

    private LTDescr _tweenGameObject;

    public bool showOnEnable;
    public bool workOnDisable;

    public void OnEnable()
    {
        if (showOnEnable)
        {
            Show();
        }
    }

    public void Show()
    {
        HandleTween();
    }

    public void HandleTween()
    {

        if(objectToAnimate == null)
        {
            objectToAnimate = gameObject;
        }   

        switch(animType)
        {
            case UIanimationTypes.Fade:
                Fade();
                break;
            case UIanimationTypes.Move:
                Move();
                break;
            case UIanimationTypes.Scale:
                Scale();
                break;
            case UIanimationTypes.ScaleX:
                Scale();
                break;
            case UIanimationTypes.ScaleY:
                Scale();
                break;
        }

        _tweenGameObject.setDelay(delay);
        _tweenGameObject.setEase(easeType);

        if (loop)
        {
            _tweenGameObject.loopCount = int.MaxValue;
        }

        if (pingpong)
        {
            _tweenGameObject.setLoopPingPong();

        }
    }

    public void Fade() { 
        if(gameObject.GetComponent<CanvasGroup>() == null)
        {
            gameObject.AddComponent<CanvasGroup>();
        }

        if (startPositionOffset)
        {
            objectToAnimate.GetComponent<CanvasGroup>().alpha = from.x;
        }

        _tweenGameObject = LeanTween.alphaCanvas(objectToAnimate.GetComponent<CanvasGroup>(), to.x, duration);

    }

    public void Move()
    {
        objectToAnimate.GetComponent<RectTransform>().anchoredPosition = from;
        _tweenGameObject = LeanTween.move(objectToAnimate.GetComponent<RectTransform>(), to, duration);

    }

    public void Scale()
    {
        if (startPositionOffset)
        {
            objectToAnimate.GetComponent<RectTransform>().localScale = from;
        }

        _tweenGameObject = LeanTween.scale(objectToAnimate, to, duration);
    }

    void SwapDiretion()
    {
        var temp = from;
        from = to;
        to = temp;
    }

    public void Disable()
    {
        SwapDiretion();
        HandleTween();

        _tweenGameObject.setOnComplete(() =>
        {
            SwapDiretion();
            gameObject.SetActive(false);
        });
    }

    public void WorkOnDisable()
    {

    }



}
