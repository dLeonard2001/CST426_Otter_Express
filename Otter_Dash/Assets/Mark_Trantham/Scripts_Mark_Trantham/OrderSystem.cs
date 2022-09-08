using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class OrderSystem : MonoBehaviour
{

    public static OrderSystem instance;
    
    public List<Order> myOrders = new List<Order>();

    public Order activeOrder;

    private bool playerAcceptedOrder;

    public GameObject acceptDeclinePanel;
    public GameObject pickUpText;
    
    //Pick up/ dropoff timer, TODO: work on pick up and drop of timers
    private float pickUpTimer = 3f;
    private float dropOffTimer = 3f;

    public bool orderPickedUp = false;

    private GameObject deliveryLocation;
    private GameObject pickUpLocation;





    private void Awake()
    {
        instance = this;
    }
    
    
    //A quick way to recieve an order (press space to recieve order). TODO: add a way new way for orders to get to player
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && activeOrder == null)
        {
            Debug.Log("Sending accept or decline to phone");
            SendOrderToPhone();
        }
    }


    //Send order to player's phone. To be accepted or declined.
    void SendOrderToPhone()
    {
        acceptDeclinePanel.SetActive(true);
    }


    //Accept the order
    public void AcceptOrder()
    {
        //player accepts order
        playerAcceptedOrder = true;
        //take random order and make as new active order.
        AddOrder();
        Debug.Log("Order has been accepted" );
        

        
        //TODO: display pickup location for food.
        if (activeOrder == null)
        {
            return;
        }
        acceptDeclinePanel.SetActive(false);
        PickUpAtRestaurant();
    }

    
    //Display pick up order text
    public void PickUpAtRestaurant()
    {
        pickUpText.GetComponent<UnityEngine.UI.Text>().text = "Pick up order at the restaurant";
    }
    
    
    //Decline the order
    public void DeclineOrder()
    {
        playerAcceptedOrder = false;
        acceptDeclinePanel.SetActive(false);
        Debug.Log("Order declined!");
    }
    
    

    void AddOrder()
    {
        if (activeOrder == null)
        {
            activeOrder = myOrders[Random.Range(0, myOrders.Count)];
            Debug.Log("new order taken");
            pickUpLocation = Instantiate(activeOrder.pickUpLocation, activeOrder.myPickUpLocation, Quaternion.identity);
        }
    }

    public int ReadActiveOrder()
    {
        return activeOrder.orderId;
    }

    public void CompleteOrder(int orderId)
    {
        //Have no active order
        if (activeOrder == null)
        {
            return;
        }
        if (activeOrder.orderId == orderId)
        {
            
            //order complete
            orderPickedUp = false;
            Debug.Log("You have delivered the order!");
            //give reward
            Debug.Log("Take these shells!");
            //update phone

            //Order done
            activeOrder = null;
        }
    }


    //Player pickup and dropoff
    private void OnTriggerEnter(Collider other)
    {
        
        //player picks up food
        if (other.CompareTag("PickUpLocation") && orderPickedUp == false)
        {
            orderPickedUp = true;
            pickUpText.GetComponent<UnityEngine.UI.Text>().text = "Food is hot and ready. Go to the delivery location!";
            
            Debug.Log("Order has been picked up!");

            //spawn the delivery location after picking up food.
            deliveryLocation = Instantiate(activeOrder.deliveryLocation, activeOrder.myDeliveryLocation, Quaternion.identity);
            
            //Delete the pick up location after spawning delivery location.
            Destroy(pickUpLocation,0.0f);

        }

        
        //player drops off food
        if (other.CompareTag("DropOffLocation") && orderPickedUp)
        {
            Debug.Log("Order is being given to customer!");
            instance.CompleteOrder(activeOrder.orderId);

            //Display order complete text, Function
            StartCoroutine(DisplayedOrderCompleteText());
            
            //Delete the delivery location marker
            Destroy(deliveryLocation, 0.0f);
        }

    }

    
    //Order complete text
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
