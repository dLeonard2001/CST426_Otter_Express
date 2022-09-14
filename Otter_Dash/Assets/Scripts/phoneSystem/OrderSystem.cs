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
    
    public bool orderPickedUp = false;
    public bool orderDelivered = false;

    private GameObject deliveryLocation;
    private GameObject pickUpLocation;





    private void Awake()
    {
        instance = this;
    }
    

    public void AddOrder()
    {
        if (activeOrder == null)
        {
            activeOrder = myOrders[Random.Range(0, myOrders.Count)];
            Debug.Log("new order taken");
            pickUpLocation = Instantiate(activeOrder.pickUpLocation, activeOrder.myPickUpLocation, Quaternion.identity);
            orderDelivered = false;
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

            orderPickedUp = false;
            orderDelivered = true;

            
            //Delete the delivery location marker
            Destroy(deliveryLocation, 0.0f);
        }

    }


        
        
        
}