using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccount : MonoBehaviour
{ // player account itself
    public int coinCount = 0;
    public static string accountNameToLoad;
    [HideInInspector]public int specialCoinCount = 0;
    public string playerAccountName = "Account1";
    [HideInInspector]public string purchasedItems = "E000";
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
        if (accountNameToLoad != null)
        {
            playerAccountName = accountNameToLoad;

        }
        loadPlayer();

    }

    public void savePlayer()
    {
        SaveSystem.savePlayer(this);
    }

    public void loadPlayer()
    {
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

        
    }

    
    
}
