using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class LoadGameHandler : MonoBehaviour
{

    public String accountSelection;
    public GameObject playerData;
    public TextMeshProUGUI username_input;
    [SerializeField] UnityEvent OnClickEmptySlot;
    [SerializeField] UnityEvent OnClickOccupiedSlot;


    // gemme all files that ends in .otter

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void updateAccountSelection(TextMeshProUGUI selection)
    {
        accountSelection = selection.text;
    }
    

    public void creatNewAccount(Button slot) // if save slot is empty create a new account
    {
        if (slot.tag == "empty")
        {
            OnClickEmptySlot.Invoke();
        }
        else
        {
            OnClickOccupiedSlot.Invoke();
        }
    }

    public void setUserSlection(TextMeshProUGUI text)
    {
        accountSelection = text.text;
    }


    public void setLoadingVars()
    {
        
        PlayerAccount.accountNameToLoad = accountSelection;
        playerData.GetComponent<player_data_to_pass>().setPlayerNameData(PlayerAccount.accountNameToLoad);
        DontDestroyOnLoad(playerData);
    }

    public void setNewPlayerName()
    {
        Debug.Log(username_input.text);
        PlayerAccount.accountNameToLoad = username_input.text;
        playerData.GetComponent<player_data_to_pass>().setPlayerNameData(PlayerAccount.accountNameToLoad);
        DontDestroyOnLoad(playerData);
    }
    

    

}
