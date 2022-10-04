using System.Collections;
using UnityEngine;

// need to change this entire script to a replay script
namespace Command
{
    public class Replay : MonoBehaviour
    {
        public static IEnumerator replayOrder()
        {
            yield return new WaitForSeconds(1);
        }
    } 
}

