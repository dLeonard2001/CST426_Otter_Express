using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneSwitch : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject controlsPanel;
    public bool controlsOn;
    private int i;
    private Stack<int> scenes = new Stack<int>();

    public void Start()
    {
        i = 0;
    }

    public void LoadScene(string sceneName)
    {
        var scene = SceneManager.GetActiveScene();
        var activeScene = scene.buildIndex;
        scenes.Push(activeScene);
        // SceneManager.UnloadSceneAsync(scene);
        SceneManager.LoadSceneAsync(sceneName);
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
        i++;
        LoadScene("main_game");
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

    public void DisplayControls()
    {
        if (controlsOn)
        {
            mainPanel.SetActive(false);
            controlsPanel.SetActive(true);
            
        }
        else
        {
            mainPanel.SetActive(true);
            controlsPanel.SetActive(false);
        }
    }



}
