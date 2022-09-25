using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShellCounter : MonoBehaviour
{
    private static TextMeshProUGUI coinCountUI;

    // Start is called before the first frame update
    void Start()
    {
        
        //Debug.Log(bag.keepWarmStrength);
        coinCountUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void updateCoinCount(float count)
    {
        coinCountUI.text = (int.Parse(coinCountUI.text) + Mathf.CeilToInt(count)).ToString();
    }

    
}
