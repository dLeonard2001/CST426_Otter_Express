using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{



    //Test money.
    public int testCurrency = 1000;
    public int testSpecialCurrency = 2;
    
    
    public TextMeshProUGUI testCurrencyText;
    public TextMeshProUGUI testSpecialCurrencyText;

    //Reference to heatControl, so i can access 'ChangeBag()' function
    [SerializeField] private GameObject heatControl;


    //All Buttons and Text (currently only bags(first 3))
    [SerializeField] private List<GameObject> myPurchaseButtons = new List<GameObject>();
    [SerializeField] private List<GameObject> myEquipButtons = new List<GameObject>();
    [SerializeField] private List<TextMeshProUGUI> myEquipButtonText = new List<TextMeshProUGUI>();

    //which bags are purchased
    private bool lunchBagPurchased = false;
    private bool StyrofoamBagPurchased = false;
    private bool PremiumBagPurchased = false;
    
    //Which bag is equiped
    private bool lunchBagEquiped = false;
    private bool styrofoamBagEquiped = false;
    private bool premiumBagEquiped = false;


    private void Start()
    {
        CheckShopSaveFile();
    }

    private void Update()
    {
        //Display value of our currency 
        testCurrencyText.text = testCurrency.ToString();
        testSpecialCurrencyText.text = testSpecialCurrency.ToString();
    }


    //Not sure if this helps or not. Will check if the booleans are true, if they are then the buttons will be set up correctly.
    public void CheckShopSaveFile()
    {
        if (lunchBagPurchased)
        {
            myPurchaseButtons[0].SetActive(false);
            myEquipButtons[0].SetActive(true);
        }

        if (StyrofoamBagPurchased)
        {
            myPurchaseButtons[1].SetActive(false);
            myEquipButtons[1].SetActive(true);
        }

        if (PremiumBagPurchased)
        {
            myPurchaseButtons[2].SetActive(false);
            myEquipButtons[2].SetActive(true);
        }
    }


    //PURCHASING ITEMS
    public void PurchaseItem(string bagName)
    {
        //Purchase LunchBag
        if (bagName == "LunchBag" && !lunchBagPurchased)
        {
            if (testCurrency >= 100)
            {
                Debug.Log("you have purchased the LunchBag");
                myPurchaseButtons[0].SetActive(false);
                lunchBagPurchased = true;
                myEquipButtons[0].SetActive(true);
                
                //decrease currency
                testCurrency -= 100;
            }
            else
            {
                Debug.Log("You don't have enough money! Start dashing!");
            }
        }

        //Purchase StyrofoamBag
        if (bagName == "StyrofoamBag" && !StyrofoamBagPurchased)
        {
            if (testCurrency >= 200)
            {
                Debug.Log("You have purchased the StyrofoamBag");
                myPurchaseButtons[1].SetActive(false);
                StyrofoamBagPurchased = true;
                myEquipButtons[1].SetActive(true);
                
                //decrease currency
                testCurrency -= 200;
            }
            else
            {
                Debug.Log("You don't have enough money! Start dashing!");
            }

        }

        //Purchase PremiumBag
        if (bagName == "PremiumBag" && !PremiumBagPurchased)
        {
            if (testCurrency >= 300 && testSpecialCurrency >= 1)
            {
                Debug.Log("You have purchased the PremiumBag");
                myPurchaseButtons[2].SetActive(false);
                PremiumBagPurchased = true;
                myEquipButtons[2].SetActive(true);
                
                //decrease currency
                testCurrency -= 300;
                testSpecialCurrency -= 1;
            }
            else
            {
                Debug.Log("You don't have enough money and you need a rare piece! Start dashing!");
            }

        }

    }

    
    
    //EQUIPPING BAGS
    public void EquipBag(string bagName)
    {
        //Equip LunchBag
        if (bagName == "LunchBag" && lunchBagPurchased)
        {
            //if bag is already equiped it will show 'unequip' so we can click the button again we want to have display equip and go back to default bag
            //then make equip boolean false.
            if (lunchBagEquiped)
            {
                myEquipButtonText[0].text = "Equip";
                heatControl.GetComponent<HeatControl>().changeBag("WhackAssBag");
                lunchBagEquiped = false;
            }else if (!lunchBagEquiped)
            {
                myEquipButtonText[0].text = "Unequip";
                myEquipButtonText[1].text = "Equip";
                myEquipButtonText[2].text = "Equip";
                heatControl.GetComponent<HeatControl>().changeBag(bagName);
                lunchBagEquiped = true;
                styrofoamBagEquiped = false;
                premiumBagEquiped = false;
            }
        }

        //Equip StyrofoamBag
        if (bagName == "StyrofoamBag" && StyrofoamBagPurchased)
        {
            //if bag is already equiped it will show 'unequip' so we can click the button again we want to have display equip and go back to default bag
            //then make equip boolean false.
            if (styrofoamBagEquiped)
            {
                myEquipButtonText[1].text = "Equip";
                heatControl.GetComponent<HeatControl>().changeBag("WhackAssBag");
                styrofoamBagEquiped = false;
            }else if (!styrofoamBagEquiped)
            {
                myEquipButtonText[1].text = "Unequip";
                myEquipButtonText[0].text = "Equip";
                myEquipButtonText[2].text = "Equip";
                heatControl.GetComponent<HeatControl>().changeBag(bagName);
                styrofoamBagEquiped = true;
                lunchBagEquiped = false;
                premiumBagEquiped = false;
            }
        }

        //Equip PremiumBag
        if (bagName == "PremiumBag" && PremiumBagPurchased)
        {
            //if bag is already equiped it will show 'unequip' so we can click the button again we want to have display equip and go back to default bag
            //then make equip boolean false.
            if (premiumBagEquiped)
            {
                myEquipButtonText[2].text = "Equip";
                heatControl.GetComponent<HeatControl>().changeBag("WhackAssBag");
                premiumBagEquiped = false;
            }else if (!premiumBagEquiped)
            {
                myEquipButtonText[2].text = "Unequip";
                myEquipButtonText[0].text = "Equip";
                myEquipButtonText[1].text = "Equip";
                heatControl.GetComponent<HeatControl>().changeBag(bagName);
                premiumBagEquiped = true;
                lunchBagEquiped = false;
                styrofoamBagEquiped = false;
            }
        }
        
    }


}
