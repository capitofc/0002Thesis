using System.Collections;
using System.Collections.Generic;
using System;
using System.Windows;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class ModalWindowPanel : MonoBehaviour
{

    [Header("Header")]
    public Transform headerArea;
    public TextMeshProUGUI titleField;

    [Header("Content")]
    [SerializeField]
    private Transform contentArea;

    [SerializeField]
    public Transform verticalLayoutArea;
    public Image heroImage;
    public TextMeshProUGUI heroText;

    [Space()]
    public Transform horizontalLayoutArea;
    public Transform iconContainer;
    public Image iconImg;
    public TextMeshProUGUI iconTxt;

    [Header("Footer")]
    [SerializeField]
    private Transform footerArea;
    [SerializeField]
    private Button continueBtn;
    [SerializeField]
    private Button declineBtn;
    public Button alternateBtn;

    public TextMeshProUGUI continueBtnTxt;
    public TextMeshProUGUI declineBtnTxt;
    public TextMeshProUGUI alternateBtnTxt;

    private Action onConfirmAction;
    private Action onDeclineAction;
    private Action onAlternateAction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Confirm()
    {
        Debug.Log("confirm button pressed");
        onConfirmAction?.Invoke();
    }

    public void Decline()
    {
        onDeclineAction?.Invoke();
        //Close();
    }

    public void Alternate()
    {
        onAlternateAction?.Invoke();
       // Close();
    }
    public void ShowAsHero(string title, Sprite imageToShow, string message, string confirmBtn_txt, string declineBtn_txt, string alternateBtn_txt, Action confirmAction, Action declineAction = null, Action alternateAction = null)
    {

            Debug.Log("Pumasok sya sa show as hero");
            horizontalLayoutArea.gameObject.SetActive(false);
            verticalLayoutArea.gameObject.SetActive(true);
            //Hide the header if there's no title

            bool hasTitle = false;
            if(title.Length > 0)
            {
                hasTitle = true;
            }
            headerArea.gameObject.SetActive(hasTitle);

            titleField.text = title;

            heroImage.sprite = imageToShow;
            heroText.text = message;

            continueBtnTxt.text = confirmBtn_txt;

            Debug.Log(confirmBtn_txt);
            onConfirmAction = confirmAction;

            bool hasDecline = (declineAction != null);
            Debug.Log(hasDecline);
            declineBtn.gameObject.SetActive(hasDecline);
            declineBtnTxt.text = declineBtn_txt;
            onDeclineAction = declineAction;

            bool hasAlternate = (alternateAction != null);
            alternateBtn.gameObject.SetActive(hasAlternate);
            alternateBtnTxt.text = alternateBtn_txt;
            onAlternateAction  = alternateAction;
    }

    public void ShowAsHero(string title, Sprite imageToShow, string message, Action confirmAction)
    {
        ShowAsHero(title, imageToShow, message, "Continue", "", "", confirmAction);
    }

    public void ShowAsHero(string title, Sprite imageToShow, string message, Action confirmAction, Action declineAction)
    {
        ShowAsHero(title, imageToShow, message, "Continue", "Back", "", confirmAction, declineAction);
    }




}
