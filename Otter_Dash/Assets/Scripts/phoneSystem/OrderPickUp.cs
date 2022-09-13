using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPickUp : MonoBehaviour
{


    public GameObject myPhone;
    public GameObject pickUpText;


    private void OnTriggerEnter(Collider other)
    {
        if (myPhone.GetComponentInChildren<OrderSystem>().activeOrder == null)
        {
            return;
        }
        
        if(other.CompareTag("Player") && myPhone.GetComponentInChildren<OrderSystem>().orderPickedUp == false)
        {
            myPhone.GetComponentInChildren<OrderSystem>().orderPickedUp = true;
            pickUpText.GetComponent<UnityEngine.UI.Text>().text = "Food is hot and ready. Go to the delivery location!";
            Debug.Log("Order has been picked up!");
        }
    }
}
