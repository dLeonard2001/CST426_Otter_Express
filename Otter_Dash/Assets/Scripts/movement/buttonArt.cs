using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class buttonArt : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image otterImage;
    public RectTransform buttonTrans;
    public buttonManager buttonManager;

    private void Start()
    {
        buttonTrans = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        otterImage.rectTransform.position = buttonTrans.position;
        buttonManager.setButtonAction("up");
        buttonManager.setCurrentButton(buttonTrans);
        buttonManager.performAction(otterImage);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonManager.setButtonAction("down");
        buttonManager.setCurrentButton(eventData.pointerEnter.GetComponent<RectTransform>());
        buttonManager.performAction(otterImage);
    }

    
    

}
