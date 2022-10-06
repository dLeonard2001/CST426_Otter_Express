using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_data_to_pass : MonoBehaviour
{
    public string playerName;
    // Start is called before the first frame update

    public void setPlayerNameData(string name)
    {
        playerName = name;
    }
}
