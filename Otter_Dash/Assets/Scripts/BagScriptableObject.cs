using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "NEW BAG", menuName = "Bags/WhackAss")]
public class BagScriptableObject : ScriptableObject
{
    public Sprite bagPicture;
    [Tooltip("how many sec that goes by before the heat meter drops by drop rate.")]
    public float keepWarmStrength;
}
