using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ShellScript gameobject = other.GetComponent<ShellScript>();
        if (gameobject != null)
        {
            if (other.tag == "shell")
            {
                other.gameObject.SetActive(false);
                ShellCounter.updateCoinCount(gameobject.worth);
            }else if (other.tag == "heat")
            {
                other.gameObject.SetActive(false);
                HeatControl.addMoreHeatToFood(gameobject.worth);
            }
        }
        
        
    }
}
