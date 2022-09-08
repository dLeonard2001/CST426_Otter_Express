using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShellCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCountUI;
    // Start is called before the first frame update
    void Start()
    {
        coinCountUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateCoinCount(int count)
    {
        coinCountUI.text = (int.Parse(coinCountUI.text) + count).ToString();
    }

    
}
