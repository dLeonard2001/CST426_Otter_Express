using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
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
