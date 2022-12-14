using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAccount : MonoBehaviour
{ // player account itself
    public int coinCount;
    public static string accountNameToLoad;
    private String[] updatedPurchasedItemsList;
    [HideInInspector]public int specialCoinCount = 0;
    public string playerAccountName;
    public string purchasedItems = "E000";
    [SerializeField] private UnityEvent OnLoad;
    private ShopManager shopManager;
    private GameObject playerData;
    
    // each 0 represent the items you can buy from a store
    //index 0 is for Whack ass bag
    //index 1 is for lunch bag
    //index 2 is for styrofoamBag
    //index 3 is for premium bag
    
    // if any of these bags are equipped, the character in its index will be an "e"
    // if any of these bags were purchased already then its character will be "1"
    
    // Start is called before the first frame update

    private void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
        playerData = GameObject.FindWithTag("data");
        
        Debug.Log(playerData.name);
        playerAccountName = playerData.GetComponent<player_data_to_pass>().playerName;
        Debug.Log(playerAccountName);

        loadPlayer();

    }

    public void savePlayer()
    {
        coinCount = ShellCounter.getCoinCount();
        if (!shopManager)
        {
            shopManager = FindObjectOfType<ShopManager>();

        }
        //updatedPurchasedItemsList = shopManager.myInventory.Values.ToList();;
        //purchasedItems = "";
       /* for (int x = 0; x < updatedPurchasedItemsList.Length; x++)
        {
            purchasedItems += updatedPurchasedItemsList[0];
        }*/
       playerData = GameObject.FindWithTag("data");
       playerAccountName = playerData.GetComponent<player_data_to_pass>().playerName;
       SaveSystem.savePlayer(this);
    }

    public void loadPlayer()
    {
        playerAccountName = playerData.GetComponent<player_data_to_pass>().playerName;
        PlayerData data = SaveSystem.loadPlayer(playerAccountName);
        if (data == null)
        {
            savePlayer();
        }
        else
        {
            playerAccountName = data.accountName;
            coinCount = data.coinCount;
            specialCoinCount = data.specialCoinCount;
            purchasedItems = data.purchasedItems;
        }
        OnLoad.Invoke();
        // ShellCounter.setStartCoinAmmount(coinCount); // set the coin count at start of game
        
    }


    public void updatePlayerAccount()
    {
        
        coinCount = ShellCounter.getCoinCount();
    }

    
    
}
