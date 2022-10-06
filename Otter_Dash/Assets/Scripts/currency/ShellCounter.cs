using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShellCounter : MonoBehaviour
{
     private static TextMeshProUGUI coinCountUI;
     private PlayerAccount playerAccount;

     private float currentCoinAmount;

    // Start is called before the first frame update
    void Awake()    
    {
        
        //Debug.Log(bag.keepWarmStrength);
        coinCountUI = GetComponent<TextMeshProUGUI>();
        playerAccount = FindObjectOfType<PlayerAccount>();
        
    }

    // Update is called once per frame
    

    public static void updateCoinCount(float count)
    {
        
        coinCountUI.text = (int.Parse(coinCountUI.text) + Mathf.CeilToInt(count)).ToString();
    }
    
    public static int getCoinCount()
    {
        return int.Parse(coinCountUI.text); //gets the current coin count.
    }
    
    public void setStartCoinAmmount() // this is called when the players account is loaded
    {
        if (playerAccount == null)
        {
            playerAccount = FindObjectOfType<PlayerAccount>();
        }
        coinCountUI = GetComponent<TextMeshProUGUI>();
        coinCountUI.text = playerAccount.coinCount.ToString();
    }
    

    
}
