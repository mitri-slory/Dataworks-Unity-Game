using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HighScore : MonoBehaviour
{
    public GameObject WinCommandCanvas;
    public Text HighScoreUpdateText;
    public Text Score;
    public Text highScore;
    public CommandSelect cs;
    public Text TotalCommandsText;
    public Text SuccessCommandsText;
    public Text FullSyntaxCommandsText;
    public Text PartialSyntaxCommandsText;
    public Text FailedCommandsText;
    public int sceneID;

    void Start()
    {
        //Setting the highScore text equal to the Starting Counter of a level
        GameObject Counter1 = GameObject.Find("Counter");
        Text counttext = Counter1.GetComponent<Text>();
        String countstring = counttext.text;
        int count = int.Parse(countstring);
        cs.startingcount = count;
        //Sets the highScoreText equal to the High Score of each level
        highScore.text = PlayerPrefs.GetFloat("HighScore" + sceneID, cs.startingcount).ToString();
    }
    //Updates the HighScore based on if the player completes level with less commands than last time
    public void HighScoreUpdate()
    {
        float score = GameManager.Instance.TotalCommands;
        Score.text = score.ToString();
        if (score < PlayerPrefs.GetFloat("HighScore" + sceneID, cs.startingcount))
        {
            HighScoreUpdateText.gameObject.SetActive(true);
            PlayerPrefs.SetFloat("HighScore" + sceneID, score);
            highScore.text = score.ToString();
        }
    }
    //When the Next button is clicked, WinCommand Canvas opens up which showcases the number of different types of commands made during the level
    public void Next()
    {
        WinCommandCanvas.SetActive(true);
        GameObject.Find("HighScoreCanvas").SetActive(false);
        TotalCommandsText.text += GameManager.Instance.TotalCommands.ToString();
        SuccessCommandsText.text += GameManager.Instance.SuccessCommands.ToString();
        FullSyntaxCommandsText.text += GameManager.Instance.FullSyntaxCommands.ToString();
        PartialSyntaxCommandsText.text += GameManager.Instance.PartialSyntaxCommands.ToString();
        FailedCommandsText.text += GameManager.Instance.FailCommands.ToString();
    }
    //When the Reset Button is hit on the WinCommandCanvas, the HighScore of the level resets to the Starting Counter of the level.
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore" + sceneID);
        highScore.text = cs.startingcount.ToString();
    }
}
