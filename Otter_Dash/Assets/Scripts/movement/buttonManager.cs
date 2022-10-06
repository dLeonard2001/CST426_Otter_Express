using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonManager : MonoBehaviour
{
    public RectTransform currentButton;
    public String buttonAction;

    public RectTransform getCurrentButton()
    {
        return currentButton;
    }

    public String getButtonAction()
    {
        return buttonAction;
    }
    
    public void setCurrentButton(RectTransform currentButton)
    {
        this.currentButton = currentButton;
        Debug.Log(currentButton);
    }

    public void setButtonAction(String buttonAction)
    {
        this.buttonAction = buttonAction;
    }

    public void performAction(Image image)
    {
        StopAllCoroutines();
        StartCoroutine(moveImageCoroutine(image, getButtonAction()));
    }
    
    private IEnumerator moveImageCoroutine(Image image, String direction)
    {
        Debug.Log(direction);
        float y_pos = image.rectTransform.position.y;
        if (direction.Equals("up"))
        {
            while (image.rectTransform.position.y < y_pos + 40f)
            {
                image.rectTransform.position += new Vector3(0f, 1, 0f);
                yield return null;
            }
            
        }else if(direction.Equals("down"))
        {
            while (image.rectTransform.position.y > getCurrentButton().position.y)
            {
                image.rectTransform.position -= new Vector3(0f, 1, 0f);
                yield return null;
            }
            
        }
    }
}
