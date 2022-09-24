using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testOrderCallSystem : MonoBehaviour
{
    public OrderSystem testOrderSytem;
    
    // Start is called before the first frame update
    void Start()
    {
        acceptNewOrder();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator addOrder()
    {
        yield return new WaitForSeconds(5f);
        testOrderSytem.AddOrder();
    }

    public void acceptNewOrder()
    {
        StartCoroutine("addOrder");
    }
}
