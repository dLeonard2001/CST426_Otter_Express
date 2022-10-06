using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class sceneSwitch : MonoBehaviour
{
    public void Awake()
    {
        Debug.Log("im waking up");
    }

    public GameObject mainPanel;
    public GameObject controlsPanel;
    public Button startBtn;
    public bool controlsOn;
    public Image otterImage;

    [Header("Animations")] 
    public Animator transition;
    public float transitionTime;
    
    
    private int i;
    private Stack<int> scenes = new Stack<int>();

    public void Start()
    {
        i = 0;
    }

    public void LoadScene(int i)
    {
        var scene = SceneManager.GetActiveScene();
        var activeScene = scene.buildIndex;
        scenes.Push(activeScene);
        StartCoroutine(LoadLevel(i));
    }

    public void PreviousScene()
    {
        if (scenes.Count > 0)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadSceneAsync(scenes.Pop());
        }
    }

    public void start_btn()
    {
        i = 1;
        LoadScene(i);
    }

    public void back_to_main_menu()
    {
        Time.timeScale = 1;
        i = 0;
        LoadScene(i);
    }

    public void controls_btn()
    {
        controlsOn = true;
        DisplayControls();
    }

    public void back_btn()
    {
        controlsOn = false;
        DisplayControls();
    }

    public void quit_btn()
    {
        Application.Quit();
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("start");

        yield return new WaitForSeconds(3f);
        
        SceneManager.LoadSceneAsync(levelIndex);
        
    }

    public void DisplayControls()
    {
        if (controlsOn)
        {
            otterImage.enabled = false;
            mainPanel.SetActive(false);
            controlsPanel.SetActive(true);
        }
        else
        {
             
            mainPanel.SetActive(true);
            otterImage.enabled = true;
            controlsPanel.SetActive(false);
        }
    }
    
}
