using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class lootSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dBoostsLootParentObj; // all delivery boost powerUp load up here
    public loot[] itemToSpawn; //list of regular item to spawn
    public loot[] deliveryBootsItemsToSpawn; // items like heat, or nitro boost etc.
    
    public Transform[] regularspawnLocations;
    public Transform[] dBoostsSpawnLocations; //delivery boost spawn locations
    private GameObject regularLootParentObj; // all loots spawn below parent




    void Start()
    {
        //dBoostsLootParentObj = GameObject.Find("---- bootLoots ----"); 
        regularLootParentObj = GameObject.Find("---- Loots ----"); 
        // regularspawnLocations = GameObject.Find("--- Locations ---").GetComponentsInChildren<Transform>();
        // dBoostsSpawnLocations = GameObject.Find("--- DB Locations ---").GetComponentsInChildren<Transform>(); 
        //delivery boot locations 
        
        setProbabiltyForItems(itemToSpawn);
        setProbabiltyForItems(deliveryBootsItemsToSpawn);

        spawnRegularLoot();
        spawnDeliveryBoosts(); // spawn the delivery boots powerUp.
    }

    private void Update()
    {
        
    }

    private void setProbabiltyForItems(loot[] loots)
    {
        for (int i = 0; i < loots.Length; i++)
        {
            if (i == 0)
            {
                loots[i].minSpawnProb = 0; // always 
                loots[i].maxSpawnProb = loots[i].spawnRate - 1;
                
                // (0 - (spawn rate -1)) is still equal to spawn rate
            }
            else
            {
                loots[i].minSpawnProb = loots[i - 1].maxSpawnProb + 1;
                loots[i].maxSpawnProb = loots[i].minSpawnProb
                    + loots[i].spawnRate -1;
            }
        }
    }

    private void spawnner( GameObject parentOBJ, Transform spawnTransform, loot[] lootType) //spawn loots to all the location passed in
    {
        
        float randomNum = Random.Range(0, 100);
        GameObject newLoot;

        for (int i = 0; i < lootType.Length; i++)
        {
            if (randomNum >= lootType[i].minSpawnProb &&
                randomNum <= lootType[i].maxSpawnProb)
            {
                newLoot= Instantiate(lootType[i].item, spawnTransform.position, spawnTransform.rotation);
                newLoot.transform.SetParent(parentOBJ.transform);
                break;
            }
        }
    }

    private void spawnRegularLoot()
    {
        for( int x = 0; x < regularspawnLocations.Length; x++)
        {
            
            spawnner(regularLootParentObj,regularspawnLocations[x], itemToSpawn);
        }
    }
    
    private void spawnDeliveryBoosts()
    {
        // Debug.Log(dBoostsSpawnLocations.Length);
        for( int x = 0; x < dBoostsSpawnLocations.Length; x++)
        {
            
            spawnner(dBoostsLootParentObj, dBoostsSpawnLocations[x], deliveryBootsItemsToSpawn);
        }
    }

    public void DeliveryMode(bool state)
    {
        //regularLootParentObj.SetActive(!state);
        dBoostsLootParentObj.SetActive(state);
    }

    [Serializable]
    public struct loot
    {
        public GameObject item;
        [Range(0,100)]public float spawnRate;
        [HideInInspector] public float minSpawnProb, maxSpawnProb;
    }
}
