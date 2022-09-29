using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverySystem : MonoBehaviour
{
    private OrderSystem orderSystem; // for handeling orders

    private Order job; // the active order
   private float rawTime;
    private float heatPercentage; // heat percentage of lastest deliverd food
    [Tooltip("used for test")]
    public float testDistanceMeters;
    [Tooltip("set true to use actual distance between pick up and drop off location.")]
    public bool useActualDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        orderSystem = FindObjectOfType<OrderSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCurrentJob() // called when a player accepts a task
    {
        job = orderSystem.activeOrder;
        calculateTime(testDistanceMeters);
        DeliveryTimeController.deliveryTimeAllocated = rawTime; // set time behind the scene
       // DeliveryTimeController.setStartTime();
    }

    public void endCurrentJob()
    {
        job = null;
    }

    public void jobFinished()
    {
        ShellCounter.updateCoinCount(calculateReward());
    }

    private float calculateReward()
    {
        float reward;
        float baseReward;
        float tips = 0;
        heatPercentage = HeatControl.getLastDeliveryHeatAmount();
        Debug.Log(HeatControl.foodState);
        if (HeatControl.foodState != HeatControl.FoodState.BAD &&
            HeatControl.foodState != HeatControl.FoodState.COLD)
        {
            baseReward = job.baseShellReward;
            tips = heatPercentage * job.tipPotential;
            reward =  tips+ baseReward;
        }
        else
        {
            baseReward = job.baseShellReward/2;
            reward = baseReward; // otter recieves half of base shell reward IS FOOD IS COLD
        }
Debug.Log("Base reward= "+ baseReward+"\n Tips= "+ Mathf.CeilToInt(tips));
        return reward;
    }

    private void calculateTime(float testDistanceInMeters) // calculate time in sec for job to be compeleted
    {
        // we assume otters should drive at least 60 km/hr everytime.
        if (useActualDistance == true)
        {
            testDistanceInMeters = Vector3.Distance(orderSystem.currentPickUpLocation, orderSystem.currentDropOffLocation); 
        }
        
        
        Debug.Log("Distance between pickUp location and drop off location is "+ testDistanceInMeters+ "meters");

        rawTime = testDistanceInMeters / 1002;

        //timeInMinutes = Mathf.CeilToInt(testDistanceInMeters / 1002); //each mile is 1002

        Debug.Log("Time alocated for delivery = " + rawTime+ " mins");


    }
}
