using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//*****************************************************************************************************
// Controls the UI during gameplay. Pause menu, quit dialog and options menu
//*****************************************************************************************************
public class GameplayUI : MonoBehaviour
{
    [SerializeField] ScreenTransition transition, pauseOverlay;
    [SerializeField] SlideMenu pauseMenu;
    [SerializeField] PopupMenu quitDialog;
    [SerializeField]  OptionsMenu optionsMenu;
    
    [SerializeField] float transitionDuration = 0.3f;
    bool isPaused = false;

    void Start()
    {
        transition.StartFadeIn();
    }

    void Update()
    {
        //Toggle pause menu when esc key pressed
        if (Input.GetKeyDown(KeyCode.Escape) ) {

            if (isPaused)
            {
                OnResume();
            }
            else
            {
                OnPause();
            }
        }
    }

    
    public void OnPause()
    {
        isPaused = true;

        //Stop game time
        Time.timeScale = 0;

        //Slide pause menu in from left
        pauseMenu.SlideIn();

        //Darken and desaturate the game world
        pauseOverlay.FadeOut();
    }

    public void OnResume()
    {
        isPaused = false;

        //Resume game time
        Time.timeScale = 1;

        //Slide pause menu out to the left
        pauseMenu.SlideOut();

        //Return brightness and colour to the world
        pauseOverlay.FadeIn();
    }

    //Pop options menu in and out
    public void OnOptions()
    {
        optionsMenu.Init();
        optionsMenu.PopIn();
    }
    public void OnOptionsBack()
    {
        optionsMenu.PopOut();
    }

    //Pop quit dialog in when quit to main menu button is pressed
    public void OnQuit() {
       
        quitDialog.PopIn();
    }

    //Quit to main menu when yes is pressed
    public void QuitYes()
    {
        StartCoroutine(Quitting());
        isPaused = false;
        Time.timeScale = 1;
    }
    //Pop quit dialog out when no is pressed
    public void QuitNo()
    {
        quitDialog.PopOut();
  
    }

    //*****************************************************************************************************
    // Transitions
    //*****************************************************************************************************

    //Fade to black then change scene to main menu
    IEnumerator Quitting()
    {
        transition.FadeOut();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");  
    }

    //Fade from black at start
    IEnumerator Starting()
    {
        transition.FadeOut();
        yield return new WaitForSeconds(2);      
    }
}
