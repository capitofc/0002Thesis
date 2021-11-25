using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public ModalWindowPanel _modalWindow;

    public ModalWindowPanel modalWindow => _modalWindow;

    private void Awake()
    {
        instance = this;
    }

}
