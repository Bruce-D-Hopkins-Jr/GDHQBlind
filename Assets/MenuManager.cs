using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using JetBrains.Annotations;
using UnityEngine.InputSystem;
using System;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine.Rendering.Universal;


public class MenuManager : Singleton<MenuManager>
{
    /// <summary>
    /// This script can be combined with the UIManager or its own seperate one.
    /// This script can also be deleted.
    /// </summary>
      TankControlInputs _tankControlInputs;

    [Header("Options Menu")]
    [SerializeField] GameObject optionsPanel;

    public UI_Slider[] Sliders;
    [SerializeField] Button back;
    [SerializeField] Button save;


    [Header("Pause Menu")]
    [SerializeField] bool isPaused;
    [SerializeField] Button cancel;
    [SerializeField] Button options;
    [SerializeField] Button mainMenu;

    //float currentValue = 1;
    [SerializeField] GameObject menuPanel;

    bool isMenuOpen; /// this can be deleted.
    
    // Start is called before the first frame update
    void Start()
    {
        _tankControlInputs = new();
        _tankControlInputs.UI.Enable();

        _tankControlInputs.UI.Cancel.performed += CloseMenu_Performed;
        _tankControlInputs.UI.Pause.performed += PauseMenuOpened;
    }

    private void PauseMenuOpened(InputAction.CallbackContext context)
    {
        OpenMenu();
    }

    private void CloseMenu_Performed(InputAction.CallbackContext context)
    {
       CloseMenu();
    }

    // Update is called once per frame
    void Update()
    { 
/// do not have to use time scale, we can make the object to stop moving with a method

        if (isPaused)
        {
            // add any other function
            Time.timeScale = 0f;
        }
        else
        {
            // add any other function
            Time.timeScale = 1f;
        }

        if(isMenuOpen)
        {
            // this can be used on the pause bool
        AudioManager.Instance.StopAllBG();
        AudioManager.Instance.StopAllSFX();
        }


       
    }

   



    public void OpenMenu() // pause menu
    {
        isMenuOpen = true;
        menuPanel.SetActive(true);
        isPaused = true;
       // AudioManager.Instance.StopAllBG();
       // AudioManager.Instance.StopAllSFX();
        
    }

    public void CloseMenu() //  the button on the right corner of the window
    {
        isMenuOpen = false;
        menuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        isPaused = false;
      

    }
    #region Options Menu

    public void BackButton() // for options menu only
    {
      isMenuOpen = true;
        isPaused = true;
        optionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void OpenOptionsMenu() // for options menu only
    {
        isMenuOpen = true;
        isPaused = true;
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

   
   public void SaveButton()
   {
    for  (int i  = 0; i < Sliders.Length; i++)
    {
       float currentValue = Sliders[i].VolumeScale();
        
       Sliders[i].SaveValues(currentValue);
        Debug.Log(currentValue);
    }
   }
    #endregion
    
}
