using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderDelivered : MonoBehaviour
{

     public GameObject myPhone;
     public GameObject pickUpText;

     private void OnTriggerEnter(Collider other)
     {
          if (other.CompareTag("Player") && myPhone.GetComponentInChildren<OrderSystem>().orderPickedUp)
          {
               Debug.Log("Order is being given to customer!");
               OrderSystem.instance.CompleteOrder(myPhone.GetComponentInChildren<OrderSystem>().activeOrder.orderId);
               
               //Display order complete text, Function
               StartCoroutine(DisplayedOrderCompleteText());
               
          }
     }



     IEnumerator DisplayedOrderCompleteText()
     {
          //Display order complete text
          pickUpText.GetComponent<UnityEngine.UI.Text>().text = "Order Complete!";

          //Display text for 3 seconds only.
          yield return new WaitForSeconds(3f);
          
          //Display goes blank again TODO: return player to home page on phone.
          pickUpText.GetComponent<UnityEngine.UI.Text>().text = "";
     }
     
}
