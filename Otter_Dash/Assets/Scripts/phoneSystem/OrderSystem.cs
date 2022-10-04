using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class OrderSystem : MonoBehaviour
{

    public static OrderSystem instance;
    
    public List<Order> myOrders = new List<Order>();
    
    public Order activeOrder;


    
    
    //Variables to check if order is picked up
    public bool orderPickedUp = false;
    //variable to check if order has been delivered. 
    public bool orderDelivered = false;

    //the green space on the floor
    public GameObject myDeliveryGreenSpacePrefab;
    public GameObject myPickUpGreenSpacePrefab;

    //Gameobject that is parent for all locations
    public GameObject allMyDropOffLocations;
    public GameObject allMyPickUpLocations;
    
    
    //List of vectors for all pickup locations and all delivery locations
    public List<Vector3> myDeliveryLocations = new List<Vector3>();
    public List<Vector3> myPickupLocations = new List<Vector3>();

    //Pick a random 'pick up order' and 'delivery' location. these variables can be used for currency calculations.
    //For example: float totalDistance =  OrderSystem.instance.currentDropOffLocation - OrderSystem.instance.currentPickUpLocation
    public Vector3 currentPickUpLocation;
    public Vector3 currentDropOffLocation;

    
    private GameObject greenSpaceToBeDestroyed;
    [SerializeField] UnityEvent OnPickUpOrder;
    [SerializeField] private UnityEvent OnAddOrder;
    [SerializeField] private UnityEvent OnDropOffOrder;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Get the transform(positions) for pick ups (only 2 currently). 
        Transform[] getPickUpLocations = allMyPickUpLocations.GetComponentsInChildren<Transform>();
        foreach (Transform child in getPickUpLocations)
        {
            myPickupLocations.Add(child.position);
        }
        
        
        //Get the transform(position) for all delivery locations (currently ~28 locations)
        Transform[] getDeliveryLocations = allMyDropOffLocations.GetComponentsInChildren<Transform>();
        foreach (Transform child in getDeliveryLocations)
        {
            myDeliveryLocations.Add(child.position);
        }
    }


    public void AddOrder()
    {
        if (activeOrder == null)
        {
            activeOrder = myOrders[Random.Range(0, myOrders.Count)];

            Debug.Log("new order taken");

            //Pick a random 'pick up order' and 'delivery' location. these variables can be used for currency calculations.
            //For example: float totalDistance =  OrderSystem.instance.currentDropOffLocation - OrderSystem.instance.currentPickUpLocation
            currentPickUpLocation = myPickupLocations[Random.Range(1, myPickupLocations.Count)];
            currentDropOffLocation = myDeliveryLocations[Random.Range(1, myDeliveryLocations.Count)];
            
            //spawn in green trigger prefab. I spawn the prefab as a instance and assign as a GameObject. This allows me to destroy the object at a later time in game.
            greenSpaceToBeDestroyed = Instantiate(myPickUpGreenSpacePrefab, currentPickUpLocation , Quaternion.identity);
            
            
            orderDelivered = false;
            OnAddOrder.Invoke();
            
            
            //old code
            //pickUpLocation = Instantiate(activeOrder.pickUpLocation, activeOrder.pickUpLocation.transform.position, Quaternion.identity);
            
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
            
            
            Destroy(greenSpaceToBeDestroyed, 0.0f);
            
            greenSpaceToBeDestroyed = Instantiate(myDeliveryGreenSpacePrefab, currentDropOffLocation, Quaternion.identity);
            OnPickUpOrder.Invoke();// fire the event
            
            
            //old code
            //spawn the delivery location after picking up food.
            //deliveryLocation = Instantiate(activeOrder.deliveryLocation, activeOrder.deliveryLocation.transform.position, Quaternion.identity);
            
            //Delete the pick up location after spawning delivery location.
            //old code
            //Destroy(pickUpLocation,0.0f);

        }

        
        //player drops off food
        if (other.CompareTag("DropOffLocation") && orderPickedUp)
        {
            Debug.Log("Order is being given to customer!");
            instance.CompleteOrder(activeOrder.orderId);

            orderPickedUp = false;
            orderDelivered = true;

            Destroy(greenSpaceToBeDestroyed);
            OnDropOffOrder.Invoke();
            
            //old code
            //Delete the delivery location marker
            //Destroy(deliveryLocation, 0.0f);
        }

    }

    public void destroyDeliveryLocation()
    {
        activeOrder = null;
        orderPickedUp = false;
        Destroy(greenSpaceToBeDestroyed);

    }



}