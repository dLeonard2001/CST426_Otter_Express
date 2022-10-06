using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class HeatControl : MonoBehaviour
{
    private BagScriptableObject bag; // a scriptable object.
    [Tooltip("how many sec that goes by before the heat meter drops by drop rate.")]
    private float keepWarmStrength; 
    //private float foodHotness = 100; //every food starts with a 100 heat
    private const float HEAT_DROP_RATE = 0.02f;
    private static bool changeBagOnStart;
    public Image parentImageComponent;
    private static float lastDeliveryHeatAmmount;
    private PlayerAccount playerAccount;
    private static string bagName = "WhackAssBag";
    float elapsedTime;
    private DirectoryInfo dir;

    [SerializeField] UnityEvent OnFoodTooCold;
    

    [SerializeField] private Color red;
    [SerializeField] private Color yellow;
    [SerializeField] private Color blue;



    public static Image heatCirlceImg;

    public enum FoodState {BAD, HOT, WARM, COLD }
    // Start is called before the first frame update
    public static FoodState foodState;

    private void Awake()
    {
        foodState = FoodState.HOT;
    }

    void Start()
    {
        parentImageComponent.enabled = true;
        heatCirlceImg = GetComponent<Image>();
        heatCirlceImg.enabled = true;
        changeBag(bagName);
        
        
        // red = heatCirlceImg.color; //save the initial red color.
        // dir = new DirectoryInfo(Application.persistentDataPath);
        // FileInfo[] info = dir.GetFiles("*.log");
        // Debug.Log(info[0].Name);
    }

    public static void changeBag(string bag)
    {
        bagName = bag;
        changeBagOnStart = true;
        
    }

    public void changeBagOnWake()
    {
        bag = Resources.Load<BagScriptableObject>("ScriptableOBJ/"+ bagName);
        keepWarmStrength = bag.keepWarmStrength;
        parentImageComponent.sprite= bag.bagPicture; // adds bag picture
    }
    




    // Update is called once per frame
    void Update()
    {
        if (changeBagOnStart)
        {
            changeBagOnWake();
            changeBagOnStart = false;
        }
        if (foodState != FoodState.BAD)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= keepWarmStrength)
            {
                elapsedTime = 0;
                reduceFoodHeat();
            }

            if (heatCirlceImg.fillAmount > 0.67f)
            {
                foodState = FoodState.HOT;
                turnCirlcleColorRed();
            }
            else if (heatCirlceImg.fillAmount <= 0.67f
                     && heatCirlceImg.fillAmount > 0.34f )
            {
                turnCirlcleColorYellow();
                foodState = FoodState.WARM;

            }
            else if (heatCirlceImg.fillAmount <= 0.34f )
            {
                turnCirlcleColorBlue();
                foodState = FoodState.COLD;

            }
        }
    }

    void turnCirlcleColorYellow()
    {
        heatCirlceImg.color = Color.Lerp(heatCirlceImg.color, yellow,0.02f);
    }
    void turnCirlcleColorBlue()
    {
        heatCirlceImg.color = Color.Lerp(heatCirlceImg.color, blue,0.02f);
    }
    
    void turnCirlcleColorRed()
    {
        heatCirlceImg.color = Color.Lerp(heatCirlceImg.color, red,0.02f);
    }

    
    public void resetHeatCircle() // on order delivered this is called
    {
        lastDeliveryHeatAmmount = heatCirlceImg.fillAmount; // store last delivery heat amount
        heatCirlceImg.color = red;
        heatCirlceImg.fillAmount = 1;
    }

    public static float getLastDeliveryHeatAmount()
    {
        return lastDeliveryHeatAmmount;
    }
   

    void reduceFoodHeat()
    {
       
        if (heatCirlceImg.fillAmount > 0)
        {
            heatCirlceImg.fillAmount -=HEAT_DROP_RATE;
        }
        else
        {
            OnFoodTooCold.Invoke();
            foodState = FoodState.BAD;
            Debug.Log("Food is cold");
        }
    }

    public static void addMoreHeatToFood(float heatAmmount)
    {
        if (heatCirlceImg.fillAmount > 0)
        {
            heatCirlceImg.fillAmount +=heatAmmount;
        }
    }
}
