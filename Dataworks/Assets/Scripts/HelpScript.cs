using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScript : MonoBehaviour
{
    public GameObject helpMenuUI;
    public CommandSelect cs;

    // Update is called once per frame
    void Update()
    {
        //When Help button is clicked, helpPause bool returns to false and runs through Resume and Pause methods based on bool
        if (CommandSelect.helpPause == true)
        {
            CommandSelect.helpPause = false;
            print("Help Menu is Open");
            if (CommandSelect.helpPause == true)
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
        helpMenuUI.SetActive(false);
        Color color = cs.DialougeBoxImage.color;
        color.a = 0.204f;
        cs.DialougeBoxImage.color = color;
        cs.enterButton.SetActive(true);
        cs.CounterContainer.SetActive(true);
        cs.OnScreenTerminal.SetActive(true);
        cs.LevelDialogueName.SetActive(true);
        cs.LevelDialogueText.SetActive(true);
        cs.LevelDialogueContinue.SetActive(true);
        Time.timeScale = 1;
        //Whichever command menu was open when the help menu opened that menu becomes visible again when the level resumes
        if (cs.SlideRotatePanelActive == true)
        {
            cs.SlideRotatePanel.SetActive(true);
            cs.SlideRotatePanelActive = false;
        }
        else if(cs.SlideCommandsPanelActive == true)
        {
            cs.SlideCommandsPanel.SetActive(true);
            cs.SlideCommandsPanelActive = false;
        }
        else if(cs.RotateCommandsPanelActive == true)
        {
            cs.RotateCommandsPanel.SetActive(true);
            cs.RotateCommandsPanelActive = false;
        }
        else if (cs.AddSubstractPanelActive == true)
        {
            cs.AddSubstractPanel.SetActive(true);
            cs.AddSubstractPanelActive = false;
        }
        CommandSelect.helpPause = false;
    }
    //When Help button is clicked, the help menu opens upp and the level is paused
    public void Pause()
    {
        helpMenuUI.SetActive(true);
        Time.timeScale = 0;
        CommandSelect.helpPause = true;
    }
}
