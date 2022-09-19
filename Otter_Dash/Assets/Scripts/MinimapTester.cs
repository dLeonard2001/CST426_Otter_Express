using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapTester : MonoBehaviour
{
    public GameObject player;
    public GameObject minimap;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Capsule");
        minimap = GameObject.Find("CameraMap");
    }

    // Update is called once per frame
    void Update()
    {
        minimap.transform.position = new Vector3(player.transform.position.x, 10, player.transform.position.z);
    }
}
