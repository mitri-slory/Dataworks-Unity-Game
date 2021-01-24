using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        //When Help/ESC button is clicked, helpPause bool returns to false and runs through Resume and Pause methods based on bool
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("ESC pressed");
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    //When Resume button clicked, the level resumes and puts back all the UI in the Game
    public void Resume()
     {
         pauseMenuUI.SetActive(false);
         Time.timeScale = 1;
         GameIsPaused = false;
     }

    //When Help/ESC button is clicked, the help menu opens upp and the level is paused
    void Pause()
     {
         pauseMenuUI.SetActive(true);
         Time.timeScale = 0;
         GameIsPaused = true;
     }

    //When the Return to Hub Button is clicked, the game returns to the Hub screen
    public void Menu()
    {
        Time.timeScale = 1;
        FindObjectOfType<Level_Loader_Sc>().LoadNextLevel(1);
    }
    
    //When the Quit button is clicked, the player exits the game
    public void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
