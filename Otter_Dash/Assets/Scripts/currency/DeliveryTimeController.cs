using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class DeliveryTimeController : MonoBehaviour
{
    float elapsedTime;
    float timeLimit = 1f;
    
    public static int deliveryTimeAllocated; //time(mins) for current delivery 
    public static string timeSpent;
    static  TextMeshProUGUI timerUIText = null;
    [SerializeField] public UnityEvent OnTimerFinish;
    
    private enum state
    {
        IS_PAUSED, RUNNING 
    }

    private static state timeState;
    

    // Start is called before the first frame update
    private void Awake()
    { 
        timerUIText = GetComponent<TextMeshProUGUI>();
        
        
    }

    void Start()
    {
        timerUIText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= timeLimit)
        {
            elapsedTime = 0;
            if (timeState == state.RUNNING)
            {
                updateUITimer();
            }
        }
    }

    public static void startDeliveryTimer() // called when a player picksUp an order
    {
    timerUIText.text = deliveryTimeAllocated + ":00";
    timeState = state.RUNNING;
    }
    public static void EndDeliveryTimer() // called when a player picksUp an order
    {
        timeState = state.IS_PAUSED; //pause the timer
        timeSpent = timerUIText.text;
    }

    public static int getMinSpent()
    {
        return int.Parse(timeSpent.Split(':')[0]);
    }
    
    public static int getSecSpent() //get seconds spent in the last delivery
    {
        return int.Parse(timeSpent.Split(':')[1]);
    }

    private void updateUITimer()
    {
        string[] timeArray = timerUIText.text.Split(':'); // TIME IN ARRAY FORMAT
        int secPart = int.Parse(timeArray[1]);
        int minPart = int.Parse(timeArray[0]);
        

        if (secPart == 0 && minPart != 0)
        {
            minPart--; // decrease min part
            secPart = 59;
        }
        else
        {
            if (secPart == 0 && minPart == 0) // timer is stopped
            {
                timeState =state.IS_PAUSED; // pauses the timer
                OnTimerFinish.Invoke();
            }
            else
            {
                secPart--;
            }
        }

        if (secPart < 10 && secPart > 0)
        {
            timerUIText.text = minPart + ":" + "0" + secPart;
        }
        else
        {
            if (secPart == 0)
            {
                timerUIText.text = minPart + ":" + secPart + "0" ;
            }
            else
            {
                timerUIText.text = minPart + ":" + secPart;
            }
        }
        
    }
}
