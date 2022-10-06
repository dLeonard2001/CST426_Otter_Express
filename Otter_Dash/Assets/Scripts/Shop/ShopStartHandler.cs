using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopStartHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerAccount playerAccount;
    public ShopManager shopManager;
    private string purchasedItems; // gets the purchased info from player account
    private Dictionary<string, string> testDic = new Dictionary<string, string>(); //test
    public static List<string> shopItemNameList; 
    private List<bool> purchasedItemsList = new List<bool>();
    private List<bool> equipedItemList = new List<bool>();
    
    

    
    void Start()
    {
        
        

        
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStorePurchaseInfo() //sets up the store based on the signed in account player
    {
        //heatControl = FindObjectOfType<HeatControl>();
        shopManager = FindObjectOfType<ShopManager>(); // will use
        shopItemNameList = shopManager.myInventory.Keys.ToList(); // what we
        playerAccount = FindObjectOfType<PlayerAccount>();
        purchasedItems = playerAccount.purchasedItems; // stores string representation of purchases
                                                       //saved in player Account
        
        //***************** Settings for the wack ass ****************
        purchasedItemsList.Add(true); // wackAss bag is always purchased
        
        if (purchasedItems[0] == 'E')
        {
            equipedItemList.Add(true); // thats to equip the wackass
        }
        else
        {
            equipedItemList.Add(false);
        }
        //************************************************************
        
        // this will fill up the purchases list and the equip list
        for (int x = 1; x < purchasedItems.Length; x++)
        { //x = 1 because we are skipping the wack ass bag
            
            switch (purchasedItems[x])
            {
                case '0':
                    purchasedItemsList.Add(false); // means that item is not purchased yet
                    equipedItemList.Add(false); // means that item is not equiped yet
                    break;
                case '1':
                    purchasedItemsList.Add(true);
                    equipedItemList.Add(false); // means that item is not equiped yet
                    break;
                default:
                    purchasedItemsList.Add(true); // equiped items are always purchased
                    equipedItemList.Add(true); // means that item is equied
                    Debug.Log("equiped");
                    break;
            }
        }

        /*lunchBagPurchased = purchasedItemsList[1];  //test
        StyrofoamBagPurchased = purchasedItemsList[2];  //test
        PremiumBagPurchased = purchasedItemsList[3];  //test

       wackAssEquiped = equipedItemList[0];  //test
        lunchBagEquiped = equipedItemList[1]; //test
        StyrofoamBagEquiped = equipedItemList[2]; //test
        PremiumBagEquiped = equipedItemList[3]; //test*/
        
        
        // this will refresh the shop based on what the player has purchased.
        shopManager.LoadInPurchasedItems(purchasedItemsList[1], purchasedItemsList[2], purchasedItemsList[3]);
        
        
        //this will set what item is equipped items in the shop
       for (int x = 0; x < equipedItemList.Count; x++)
        {
            
            if (equipedItemList[x])
            {
               shopManager.EquipBag(shopItemNameList[x]);
               HeatControl.changeBag(shopItemNameList[x]);
               break;
               
            }
        }
        
    }
    
}
