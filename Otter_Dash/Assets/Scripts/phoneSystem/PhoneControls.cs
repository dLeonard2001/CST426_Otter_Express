using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PhoneControls : MonoBehaviour
{

    [Header("Top block of Phone")]
    //Home page title text(top block of phone)
    public TextMeshProUGUI titleText;

    [Header("Different phone pages")]
    public GameObject dashPageMiddle;
    public GameObject homePageMiddle;
    
    
    [Header("Otter Express App parts")]
    //Start Dash Button Text
    public GameObject dashNowButton;
    public TextMeshProUGUI startDashText;

    [Header("Order Text Info")] 
    public TextMeshProUGUI orderText;

    [Header("Accept/Decline Buttons")] 
    public GameObject acceptDeclineButtons;
    
    

    
    //Phone states (Not sure if actually needed(mostly not)
    public enum PhoneState
    {
        homePage,
        dashPage
    }

    //Current State of the phone(for pages)
    private PhoneState currentPhoneState;






    // Start is called before the first frame update
    void Start()
    {
        currentPhoneState = PhoneState.homePage;
    }

    private void Update()
    {
        if (OrderSystem.instance.orderPickedUp)
        {
            orderText.text = "Order is hot and ready. Head to the delivery location.";
        }

        if (OrderSystem.instance.orderDelivered)
        {
            orderText.text = "Order delivered!";
            OrderDelivered();
        }
    }


    //HOME BUTTON FUNCTION
    public void HomeButton()
    {
        //current state
        currentPhoneState = PhoneState.homePage;
        
        //remove dash page
        dashPageMiddle.SetActive(false);
        
        //add home page
        homePageMiddle.SetActive(true);

        //Set title text
        titleText.text = "Home";


    }

    
    //BACK BUTTON FUNCTION
    public void BackButton()
    {
        if (currentPhoneState == PhoneState.dashPage)
        {
            dashPageMiddle.SetActive(false);
            homePageMiddle.SetActive(true);
            titleText.text = "Home";
            currentPhoneState = PhoneState.homePage;
        }

    }


    //OTTER EXPRESS APP BUTTON FUNCTION
    public void OtterDashStart()
    {
        //Current State
        currentPhoneState = PhoneState.dashPage;
        
        titleText.text = "Otter Express";
        
        homePageMiddle.SetActive(false);
        dashPageMiddle.SetActive(true);
        
    }
    
    //DASH NOW BUTTON FUNCTION
    public void DashNow()
    {
        if (OrderSystem.instance.activeOrder == null)
        {
            startDashText.text = "Waiting For Order...";

            //Look for an order
            StartCoroutine(OrderFound());
            
        }


    }

    //ORDER HAS BEEN FOUND FUNCTION
    private IEnumerator OrderFound()
    {
        yield return new WaitForSeconds(Random.Range(5, 10));
        
        //order found text
        orderText.text = "Order found!";
        
        
        //Reset dash now text
        startDashText.text = "Dash Now";
        
        //Hide dash now button
        dashNowButton.SetActive(false);
        

        
        //Display accept and decline buttons
        acceptDeclineButtons.SetActive(true);
        
    }

    //DECLINING AN ORDER FUNCTION
    public void DeclineOrder()
    {

        //Hide accept/decline buttons
        acceptDeclineButtons.SetActive(false);
        
        //Display order declined text
        orderText.text = "Order Declined.";
        
        //pause for 2 seconds
        StartCoroutine(ResetDashPagePauseFirst());
        
    }

    //PAUSE BEFORE RESETTING DASH PAGE FUNCTION
    private IEnumerator ResetDashPagePauseFirst()
    {
        yield return new WaitForSeconds(2f);
        ResetDashPage();
    }

    
    //RESET DASH PAGE FUNCTION
    public void ResetDashPage()
    {
        orderText.text = "";
        acceptDeclineButtons.SetActive(false);
        dashNowButton.SetActive(true);
        startDashText.text = "Dash now!";
    }
    

    //ACCEPTING AN ORDER FUNCTION
    public void AcceptOrder()
    {
        //Get a random order from order list(located on OrderSystem(player game object)).
        OrderSystem.instance.AddOrder();
        
        //Display order accepted text
        orderText.text = "Order accepted. Go to pick up location.";
        
        //Hide accept/decline buttons
        acceptDeclineButtons.SetActive(false);
        
        
    }

    private void OrderDelivered()
    {
        
        
        //Reset order delivered to false;
        OrderSystem.instance.orderDelivered = false;
        
        //Reset dash page
        StartCoroutine(ResetDashPagePauseFirst());
    }
    
    
    
}
