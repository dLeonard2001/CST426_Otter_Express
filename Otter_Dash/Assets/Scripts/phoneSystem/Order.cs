using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Order", menuName = "OrderSystem")]
public class Order : ScriptableObject
{
    public int orderId;
    public int baseShellReward;
    public int tipPotential;
    public string customersMessage;
    public string customersOrder;
    public int foodTemperature;
    public int orderAcceptTimer;
    



}
