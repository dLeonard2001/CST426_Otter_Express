using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PlayerData
{ // its a template used to hold a playerAccount for saving and loading
    public String accountName;
    public int coinCount;
    public int specialCoinCount;
    public string purchasedItems = "E000";//default
    // each 0 represent the items you can buy from a store
    //index 0 is for Whack ass bag
    //index 1 is for lunch bag
    //index 2 is for styrofoamBag
    //index 3 is for premium bag
    
    // if any of these bags are equipped, the character in its index will be an "e"
    // if any of these bags were purchased already then its character will be "1"

    public PlayerData(PlayerAccount playerAccount)
    {
        accountName = playerAccount.playerAccountName;
        coinCount = playerAccount.coinCount;
        specialCoinCount = playerAccount.specialCoinCount;
        purchasedItems = playerAccount.purchasedItems;
    }
    
}
