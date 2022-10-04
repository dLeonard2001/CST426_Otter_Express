using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PhoneControls : MonoBehaviour
{

<<<<<<< Updated upstream
=======
    

    
    //Shop Screen
    [Header("Shop App")]
    public GameObject shopPanel;

        //Settings Screen
    [Header("Settings App")] 
    public GameObject settingsPanel;
    
    
>>>>>>> Stashed changes
    [Header("Top block of Phone")]
    //Home page title text(top block of phone)
    public TextMeshProUGUI titleText;

    [Header("Different phone pages")]
    public GameObject dashPageMiddle;
    public GameObject homePageMiddle;
<<<<<<< Updated upstream
    
    
=======
    public GameObject musicPageMiddle;


>>>>>>> Stashed changes
    [Header("Otter Express App parts")]
    //Start Dash Button Text
    public GameObject dashNowButton;
    public TextMeshProUGUI startDashText;

    [Header("Order Text Info")] 
    public TextMeshProUGUI orderText;

    [Header("Accept/Decline Buttons")] 
    public GameObject acceptDeclineButtons;
<<<<<<< Updated upstream
    
=======



    //Music Variables
    [Header("Music list")]
    public List<AudioClip> mySongs = new List<AudioClip>();

    public TextMeshProUGUI songNameText;

    private bool firstTimeUsingMusicApp = true;

    private AudioSource myAudioSource;
    [Header("Testing current song location")]
    public int currentSong  = 0;
    private bool songIsPaused = true;

    [Header("Player/Pause images (private variables)")] 
    [SerializeField] private GameObject playPauseGameObjectButton;
    [SerializeField] private Sprite playImage;
    [SerializeField] private Sprite pauseImage;
    
    
    


    //Phone animations
    private Animator phoneAnimator;
>>>>>>> Stashed changes
    

    
    //Phone states (Not sure if actually needed(mostly not)
    public enum PhoneState
    {
        homePage,
        dashPage
    }

    //Current State of the phone(for pages)
    private PhoneState currentPhoneState;






    // Start is called before the first frame update
    void Start()
    {
        currentPhoneState = PhoneState.homePage;
    }

    private void Update()
    {
        if (OrderSystem.instance.orderPickedUp)
        {
            orderText.text = "Order is hot and ready. Head to the delivery location.";
        }

        if (OrderSystem.instance.orderDelivered)
        {
            orderText.text = "Order delivered!";
            OrderDelivered();
        }
<<<<<<< Updated upstream
=======

        
        
        //Phone going up animation
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            phoneAnimator.Play("PhoneUp");
        }

        //Phone going down animation
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            phoneAnimator.Play("PhoneDown");
        }

        //If song is done, go to next song.
        if (!myAudioSource.isPlaying && !songIsPaused && !firstTimeUsingMusicApp)
        {
            NextSong();
        }
        
>>>>>>> Stashed changes
    }


    //HOME BUTTON FUNCTION
    public void HomeButton()
    {
        //current state
        currentPhoneState = PhoneState.homePage;
        
        //remove dash page
        dashPageMiddle.SetActive(false);
        
<<<<<<< Updated upstream
=======
        //remove music page
        musicPageMiddle.SetActive(false);

>>>>>>> Stashed changes
        //add home page
        homePageMiddle.SetActive(true);

        //Set title text
        titleText.text = "Home";


    }

    
    //BACK BUTTON FUNCTION
    public void BackButton()
    {
        if (currentPhoneState == PhoneState.dashPage)
        {
            dashPageMiddle.SetActive(false);
            homePageMiddle.SetActive(true);
            titleText.text = "Home";
            currentPhoneState = PhoneState.homePage;
        }

    }


    //OTTER EXPRESS APP BUTTON FUNCTION
    public void OtterDashStart()
    {
        //Current State
        currentPhoneState = PhoneState.dashPage;
        
        titleText.text = "Otter Express";
        
        homePageMiddle.SetActive(false);
        dashPageMiddle.SetActive(true);
        
    }
    
    //DASH NOW BUTTON FUNCTION
    public void DashNow()
    {
        if (OrderSystem.instance.activeOrder == null)
        {
            startDashText.text = "Waiting For Order...";

            //Look for an order
            StartCoroutine(OrderFound());
            
        }


    }

    //ORDER HAS BEEN FOUND FUNCTION
    private IEnumerator OrderFound()
    {
        yield return new WaitForSeconds(Random.Range(5, 10));
        
        //order found text
        orderText.text = "Order found!";
        
        
        //Reset dash now text
        startDashText.text = "Dash Now";
        
        //Hide dash now button
        dashNowButton.SetActive(false);
        

        
        //Display accept and decline buttons
        acceptDeclineButtons.SetActive(true);
        
    }

    //DECLINING AN ORDER FUNCTION
    public void DeclineOrder()
    {

        //Hide accept/decline buttons
        acceptDeclineButtons.SetActive(false);
        
        //Display order declined text
        orderText.text = "Order Declined.";
        
        //pause for 2 seconds
        StartCoroutine(ResetDashPagePauseFirst());
        
    }

    //PAUSE BEFORE RESETTING DASH PAGE FUNCTION
    private IEnumerator ResetDashPagePauseFirst()
    {
        yield return new WaitForSeconds(2f);
        ResetDashPage();
    }

    
    //RESET DASH PAGE FUNCTION
    public void ResetDashPage()
    {
        orderText.text = "";
        acceptDeclineButtons.SetActive(false);
        dashNowButton.SetActive(true);
        startDashText.text = "Dash now!";
    }
    

    //ACCEPTING AN ORDER FUNCTION
    public void AcceptOrder()
    {
        //Get a random order from order list(located on OrderSystem(player game object)).
        OrderSystem.instance.AddOrder();
        
        //Display order accepted text
        orderText.text = "Order accepted. Go to pick up location.";
        
        //Hide accept/decline buttons
        acceptDeclineButtons.SetActive(false);
        
        
    }

    private void OrderDelivered()
    {
        
        
        //Reset order delivered to false;
        OrderSystem.instance.orderDelivered = false;
        
        //Reset dash page
        StartCoroutine(ResetDashPagePauseFirst());
    }
    
<<<<<<< Updated upstream
    
=======
    //MUSIC PLAY FUNCTION
    public void PlaySong()
    {
        if (firstTimeUsingMusicApp && songIsPaused)
        {
            myAudioSource.clip = mySongs[currentSong];
            firstTimeUsingMusicApp = false;
        }

        if (songIsPaused)
        {
            myAudioSource.Play();
            //todo: change play button picture to pause picture

            songIsPaused = false;

        }else if (!songIsPaused)
        {
            myAudioSource.Pause();
            // todo: change pause picture to play picture

            songIsPaused = true;
        }

        songNameText.text = myAudioSource.clip.name;

    }

    //Change the play / pause button 
    public void ChangePlayPauseImage()
    {
        if (songIsPaused)
        {
            playPauseGameObjectButton.GetComponent<Image>().sprite = playImage;

        }
        else
        {
            playPauseGameObjectButton.GetComponent<Image>().sprite = pauseImage;
        }
    }

    
    //NEXT SONG FUNCTION
    public void NextSong()
    {
        myAudioSource.Stop();
        if (currentSong + 1 > mySongs.Count -1)
        {
            currentSong = 0;
        }
        else
        {
            currentSong++;
        }
        
        myAudioSource.clip = mySongs[currentSong];
        myAudioSource.Play();
        songNameText.text = myAudioSource.clip.name;
    }
    
    //PREVIOUS SONG FUNCTION
    public void LastSong()
    {
        myAudioSource.Stop();
        if (currentSong - 1 < 0)
        {
            currentSong = mySongs.Count - 1;
        }
        else
        {
           currentSong--;
        }
        myAudioSource.clip = mySongs[currentSong];
        myAudioSource.Play();
        songNameText.text = myAudioSource.clip.name;
    }





    //============== SHOP FUNCITONS ================= //

    public void ShopAppStart()
    {
        PauseGame();
        shopPanel.SetActive(true);
        
    }
    
    
    
    
    
    //=============== SETTINGS FUNCTIONS================ //
    
    //SETTINGS APP START FUNCTION
    public void SettingsAppStart()
    {
        PauseGame();
        settingsPanel.SetActive(true);
    }
    
    

    //PAUSE GAME FUNCTION
    private void PauseGame()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    
    //UNPAUSE GAME FUNCTION
    public void UnpauseGame()
    {
        settingsPanel.SetActive(false);
        shopPanel.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }



    public void ExitGame()
    {
        Debug.Log("You are exiting the game!");
        Application.Quit();
    }
>>>>>>> Stashed changes
    
}
