using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class DeliveryTimeController : MonoBehaviour
{
    float elapsedTime;
    float timeLimit = 1f;
    
    public static float deliveryTimeAllocated; //rawTime for current delivery 
    public static string timeSpent;
    private static int startTimerMinPart; //used to accurately set the minute at start of delivery
    private static int startTimerSecPart; //used to accurately set the sec at start of delivery
    static  TextMeshProUGUI timerUIText = null;
    [SerializeField] public UnityEvent OnTimerFinish;
    private static int addMoreSec = 25 ; // because we are not calculating distance properly
    
    private enum state
    {
        IS_PAUSED, RUNNING 
    }

    private static state timeState;
    

    // Start is called before the first frame update
    private void Awake()
    { 
        timerUIText = GetComponent<TextMeshProUGUI>();
        //setStartTime();

    }

    void Start()
    {
        timerUIText = GetComponent<TextMeshProUGUI>();
        Debug.Log("awake");
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

        timerUIText.text = customizeTimeOutput(minPart, secPart);
    }

    public static void setStartTime()
    {
        startTimerMinPart = Mathf.FloorToInt(deliveryTimeAllocated);
        
        startTimerSecPart=Mathf.CeilToInt( 60 * (deliveryTimeAllocated - startTimerMinPart)) + addMoreSec;
        
        if (startTimerSecPart > 59)
            startTimerSecPart = 59;
        
        timerUIText.text = customizeTimeOutput(startTimerMinPart, startTimerSecPart);
        timeState = state.RUNNING;
    }

    private static string customizeTimeOutput(int minPart, int secPart)
    {
        string output = "";
        
        if (secPart < 10 && secPart > 0)
        {
            output = minPart + ":" + "0" + secPart;
        }
        else
        {
            if (secPart == 0)
            {
                output = minPart + ":" + secPart + "0" ;
            }
            else
            {
                output = minPart + ":" + secPart;
            }
        }

        return output;
    }

    

    
}
