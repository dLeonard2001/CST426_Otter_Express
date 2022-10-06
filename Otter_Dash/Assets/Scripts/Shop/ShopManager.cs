using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VehicleBehaviour;

public class ShopManager : MonoBehaviour
{



    //Test money.
    //public int testCurrency = 1000;
    //public int testSpecialCurrency = 2;
    
    
    public TextMeshProUGUI testCurrencyText;
    //public TextMeshProUGUI testSpecialCurrencyText;

    //Reference to heatControl, so i can access 'ChangeBag()' function
    [SerializeField] private GameObject heatControl;


    //All Buttons and Text (currently only bags(first 3))
    [SerializeField] private List<GameObject> myPurchaseButtons = new List<GameObject>();
    [SerializeField] private List<GameObject> myEquipButtons = new List<GameObject>();
    [SerializeField] private List<TextMeshProUGUI> myEquipButtonText = new List<TextMeshProUGUI>();

    //Inventory 
    public Dictionary<string, string> myInventory = new Dictionary<string, string>();
    public WheelVehicle player;
    private float wallet;

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
        myInventory.Add("WhackAssBag", "E");
        myInventory.Add("LunchBag", "0");
        myInventory.Add("StyrofoamBag", "0");
        myInventory.Add("PremiumBag", "0");
        
        CheckShopSaveFile();
    }

    private void Update()
    {
        
        //Display value of our currency 
        testCurrencyText.text = getMoney().ToString();
        //testSpecialCurrencyText.text = testSpecialCurrency.ToString();
    }


    public void LoadInPurchasedItems(bool haveLunchBag, bool haveStyrofoamBag, bool havePremiumBag)
    {
        lunchBagPurchased = haveLunchBag;
        StyrofoamBagPurchased = haveStyrofoamBag;
        PremiumBagPurchased = havePremiumBag;
        CheckShopSaveFile();
    }
    
    

    //Not sure if this helps or not. Will check if the booleans are true, if they are then the buttons will be set up correctly.
    public void CheckShopSaveFile()
    {
        if (lunchBagPurchased)
        {
            myPurchaseButtons[0].SetActive(false);
            myEquipButtons[0].SetActive(true);
            myInventory["LunchBag"] = "1";
        }

        if (StyrofoamBagPurchased)
        {
            myPurchaseButtons[1].SetActive(false);
            myEquipButtons[1].SetActive(true);
            myInventory["StyrofoamBag"] = "1";
        }

        if (PremiumBagPurchased)
        {
            myPurchaseButtons[2].SetActive(false);
            myEquipButtons[2].SetActive(true);
            myInventory["PremiumBag"] = "1";
        }
    }


    //PURCHASING ITEMS
    public void PurchaseItem(string bagName)
    {
        //Purchase LunchBag
        if (bagName == "LunchBag" && !lunchBagPurchased)
        {
            if (getMoney() >= 100)
            {
                Debug.Log("you have purchased the LunchBag");
                myPurchaseButtons[0].SetActive(false);
                lunchBagPurchased = true;
                myEquipButtons[0].SetActive(true);
                
                //decrease currency
                ShellCounter.updateCoinCount(-100);
                
                //testCurrency -= 100;
                
                //Dictionary update
                myInventory["LunchBag"] = "1";
                

            }
            else
            {
                Debug.Log("You don't have enough money! Start dashing!");
            }
        }

        //Purchase StyrofoamBag
        if (bagName == "StyrofoamBag" && !StyrofoamBagPurchased)
        {
            if (getMoney() >= 200)
            {
                Debug.Log("You have purchased the StyrofoamBag");
                myPurchaseButtons[1].SetActive(false);
                StyrofoamBagPurchased = true;
                myEquipButtons[1].SetActive(true);
                
                //decrease currency
                ShellCounter.updateCoinCount(-200);
                
                //testCurrency -= 200;
                
                //Dictionary Update
                myInventory["StyrofoamBag"] = "1";
                

            }
            else
            {
                Debug.Log("You don't have enough money! Start dashing!");
            }

        }

        //Purchase PremiumBag
        if (bagName == "PremiumBag" && !PremiumBagPurchased)
        {
            if (getMoney() >= 300)
            {
                Debug.Log("You have purchased the PremiumBag");
                myPurchaseButtons[2].SetActive(false);
                PremiumBagPurchased = true;
                myEquipButtons[2].SetActive(true);
                
                //decrease currency
                ShellCounter.updateCoinCount(-300);
                
                //testCurrency -= 300;
                //testSpecialCurrency -= 1;
                
                //Dictionary Update
                myInventory["PremiumBag"] = "1";
                

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
                HeatControl.changeBag("WhackAssBag");
                lunchBagEquiped = false;

                //dictionary update
                myInventory["LunchBag"] = "1";
                myInventory["WhackAssBag"] = "E";


            }
            else if (!lunchBagEquiped)
            {
                myEquipButtonText[0].text = "Unequip";
                myEquipButtonText[1].text = "Equip";
                myEquipButtonText[2].text = "Equip";
                
                HeatControl.changeBag(bagName);
                
                //Update dictionary to show LunchBag as equiped.
                myInventory["LunchBag"] = "E";
                myInventory["WhackAssBag"] = "1";

                //if we own premium bag but equip LunchBag set StyrofoamBag to 1 else 0 if we don't have it
                if (StyrofoamBagPurchased)
                {
                    myInventory["StyrofoamBag"] = "1";
                }
                else
                {
                    myInventory["StyrofoamBag"] = "0";
                }

                //if we own premium bag but equip LunchBag set PremiumBag to 1 else 0 if we don't have it
                if (PremiumBagPurchased)
                {
                    myInventory["PremiumBag"] = "1";
                }
                else
                {
                    myInventory["PremiumBag"] = "0";
                }



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
                Debug.Log(8);
                myEquipButtonText[1].text = "Equip";
                HeatControl.changeBag("WhackAssBag");
                styrofoamBagEquiped = false;

                //dictionary update
                myInventory["StyrofoamBag"] = "1";
                myInventory["WhackAssBag"] = "E";



            }
            else if (!styrofoamBagEquiped)
            {
                Debug.Log(9);
                myEquipButtonText[1].text = "Unequip";
                myEquipButtonText[0].text = "Equip";
                myEquipButtonText[2].text = "Equip";

                HeatControl.changeBag(bagName);

                //Update dictionary to show StyrofoamBag as equiped.
                myInventory["StyrofoamBag"] = "E";
                myInventory["WhackAssBag"] = "1";

                //if we own premium bag but equip StyrofoamBag set LunchBag to 1 else 0 if we don't have it
                if (lunchBagPurchased)
                {
                    myInventory["LunchBag"] = "1";
                }
                else
                {
                    myInventory["LunchBag"] = "0";
                }

                //if we own premium bag but equip StyrofoamBag set PremiumBag to 1 else 0 if we don't have it
                if (PremiumBagPurchased)
                {
                    myInventory["PremiumBag"] = "1";
                }
                else
                {
                    myInventory["PremiumBag"] = "0";
                }



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
                HeatControl.changeBag("WhackAssBag");
                premiumBagEquiped = false;



            }
            else if (!premiumBagEquiped)
            {
                myEquipButtonText[2].text = "Unequip";
                myEquipButtonText[0].text = "Equip";
                myEquipButtonText[1].text = "Equip";

                HeatControl.changeBag(bagName);

                //Update dictionary to show PremiumBag as equiped.
                myInventory["PremiumBag"] = "E";
                myInventory["WhackAssBag"] = "1";

                //if we own premium bag but equip PremiumBag set LunchBag to 1 else 0 if we don't have it
                if (lunchBagPurchased)
                {
                    myInventory["LunchBag"] = "1";
                }
                else
                {
                    myInventory["LunchBag"] = "0";
                }

                //if we own premium bag but equip PremiumBag set StyrofoamBag to 1 else 0 if we don't have it
                if (StyrofoamBagPurchased)
                {
                    
                    myInventory["StyrofoamBag"] = "1";
                }
                else
                {
                    myInventory["StyrofoamBag"] = "0";
                }

                premiumBagEquiped = true;
                lunchBagEquiped = false;
                styrofoamBagEquiped = false;


            }
        }

    }

    public float getMoney()
    {
        return ShellCounter.getCoinCount();
    }

    public void addNitro()
    {
        player = GameObject.FindWithTag("Player").GetComponent<WheelVehicle>();
        if (getMoney() >= 50)
        {
            if (player.isNitroFull())
            {
                Debug.Log("nitro is full, didnt purchase");
            }
            else
            {
                Debug.Log("buyiny nitro");
                player.addNitro(10f);
                ShellCounter.updateCoinCount(-50);
            }
            
        }
        
    }
    
}
