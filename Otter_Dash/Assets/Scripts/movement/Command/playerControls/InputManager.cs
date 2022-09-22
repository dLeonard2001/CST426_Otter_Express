using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private static InputManager instance;
    private PlayerControls playerControls;

    public static InputManager createInstance()
    {
        return instance;
    }
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        playerControls = new PlayerControls();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnEnable()
    {
        playerControls.Enable();
    }

    public void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetMouseDelta()
    {
        return playerControls.player.LookAround.ReadValue<Vector2>();
    }

    public bool Forward()
    {
        return playerControls.player.Forward.IsPressed();
    }
    
    public bool Backward()
    {
        return playerControls.player.Backward.IsPressed();
    }
    
    public bool Left()
    {
        return playerControls.player.Left.IsPressed();
    }
    
    public bool Right()
    {
        return playerControls.player.Right.IsPressed();
    }

    public bool UnlockMouse()
    {
        return playerControls.player.UnlockMouse.IsPressed();
    }

    public bool PullOutPhone()
    {
        return playerControls.player.Pull_out_Phone.IsPressed();
    }

}
