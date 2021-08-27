using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] ScreenTransition transition, pauseOverlay;
    [SerializeField] SlideMenu pauseMenu;
    [SerializeField] PopupMenu quitDialog, optionsMenu;
    
    [SerializeField] float transitionDuration = 0.3f;
    bool isPaused = false;

    void Start()
    {
        transition.StartFadeIn();
    }

    void Update()
    {
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
        Time.timeScale = 0;
        pauseMenu.SlideIn();
        pauseOverlay.FadeOut();
    }
    public void OnResume()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SlideOut();
        pauseOverlay.FadeIn();
    }

    public void OnOptions()
    {
        optionsMenu.PopIn();
    }
    public void OnOptionsBack()
    {
        optionsMenu.PopOut();
    }

    public void OnQuit() {
       
        quitDialog.PopIn();
    }


    public void QuitYes()
    {
        StartCoroutine(Quitting());
        isPaused = false;
        Time.timeScale = 1;
    }

    public void QuitNo()
    {

        quitDialog.PopOut();
  

    }

    //*****************************************************************************************************
    // Transitions
    //*****************************************************************************************************

    // Fade to black then quit application/editor
    IEnumerator Quitting()
    {
        transition.FadeOut();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    // Fade to black then start game
    IEnumerator Starting()
    {
        transition.FadeOut();
        yield return new WaitForSeconds(2);
        
    }

}
