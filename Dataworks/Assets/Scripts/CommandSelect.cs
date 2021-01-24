using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TigerForge;
using TMPro;

public class CommandSelect : MonoBehaviour
{
    public string command;
    
    public GameObject pipe1;
    public GameObject collidedPipe1;

    public string pipe1Name;
    public string collidedPipe1Name;

    public Text consoleText;
    public Text inputText;

    public static bool collideInput = false;
    public static bool borderInput = false;
    public static bool helpPause = false;

    public bool SlideRotatePanelActive = false;
    public bool SlideCommandsPanelActive = false;
    public bool RotateCommandsPanelActive = false;
    public bool AddSubstractPanelActive = false;

    public GameObject SlideRotatePanel;
    public GameObject SlideCommandsPanel;
    public GameObject RotateCommandsPanel;
    public GameObject AddSubstractPanel;

    public static int increment;
    public Text numOfCommands;

    public GameObject loseScreen;
    public GameObject enterButton;
    public GameObject GameCanvas;
    public GameObject Grid;
    public GridScript gs2;
    public GameObject Counter;
    public GameObject CounterContainer;
    public GameObject OnScreenTerminal;
    public GameObject LevelDialogueName;
    public GameObject LevelDialogueText;
    public GameObject LevelDialogueContinue;
    public Image DialougeBoxImage;

    public float startingcount;
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        increment = 1;
        Counter = GameObject.Find("Counter");
        enterButton = GameObject.Find("EnterButton");
        Text counttext = Counter.GetComponent<Text>();
        String countstring = counttext.text;
        int count = int.Parse(countstring);
        startingcount = count;
    }

    // Update is called once per frame
    void Update()
    {
        //pipe1 updates based on the pipe clicked and the command in the PlayerTerminal updates as the player clicks on the command buttons
        pipe1Name = GamePipe.pipe1Name;
        pipe1 = GameObject.Find(pipe1Name);
        command = inputText.text;    
    }

    public void AudioFromButton(string AudioName)
    {
        FindObjectOfType<AudioManager>().Play(AudioName);
    }

    //The PlayerTerminal updates to "/s" and the Slide directional buttons appear when the Slide button is clicked
    public void sSelect()
    {
        if (consoleText.text == "No command is in the terminal")
        {
            consoleText.text = "";
        }
        inputText.text = "/s ";
        if (SlideRotatePanel != null)
        {
            bool isActive = SlideCommandsPanel.activeSelf;
            SlideCommandsPanel.SetActive(!isActive);
            SlideRotatePanel.SetActive(isActive);
        }
    }
    //The PlayerTerminal updates to "/r" and the Rotate directional buttons appear when the Rotate button is clicked
    public void rSelect()
    {
        if (consoleText.text == "No command is in the terminal")
        {
            consoleText.text = "";
        }
        inputText.text = "/r ";
        if (RotateCommandsPanel != null)
        {
            bool isActive = RotateCommandsPanel.activeSelf;
            RotateCommandsPanel.SetActive(!isActive);
            SlideRotatePanel.SetActive(isActive);
        }
    }
    //The PlayerTerminal updates to "/s pipe up 1" and the Slide increment buttons appear when the SlideUp button is clicked
    public void slideUpSelect()
    {
        inputText.text += "pipe up 1";
        if (SlideRotatePanel != null)
        {
            bool isActive = AddSubstractPanel.activeSelf;
            SlideCommandsPanel.SetActive(isActive);
            AddSubstractPanel.SetActive(!isActive);
        }
    }
    //The PlayerTerminal updates to "/s pipe down 1" and the Slide increment buttons appear when the SlideUp button is clicked
    public void slideDownSelect()
    {
        inputText.text += "pipe down 1";
        if (SlideRotatePanel != null)
        {
            bool isActive = AddSubstractPanel.activeSelf;
            SlideCommandsPanel.SetActive(isActive);
            AddSubstractPanel.SetActive(!isActive);
        }
    }
    //The PlayerTerminal updates to "/s pipe right 1" and the Slide increment buttons appear when the SlideUp button is clicked
    public void slideRightSelect()
    {
        inputText.text += "pipe right 1";
        if (SlideRotatePanel != null)
        {
            bool isActive = AddSubstractPanel.activeSelf;
            SlideCommandsPanel.SetActive(isActive);
            AddSubstractPanel.SetActive(!isActive);
        }
    }
    //The PlayerTerminal updates to "/s pipe left 1" and the Slide increment buttons appear when the SlideUp button is clicked
    public void slideLeftSelect()
    {
        inputText.text += "pipe left 1";
        if (SlideRotatePanel != null)
        {
            bool isActive = AddSubstractPanel.activeSelf;
            SlideCommandsPanel.SetActive(isActive);
            AddSubstractPanel.SetActive(!isActive);
        }
    }
    //The PlayerTerminal updates to "/r pipe right" when the Rotate Right Button is clicked
    public void rotateClockwise()
    {
        if(inputText.text == "/r pipe right" || inputText.text == "/r pipe left")
        {
            inputText.text = "/r pipe right";
        }
        else
        {
            inputText.text += "pipe right";
        }
    }
    //The PlayerTerminal updates to "/r pipe left" when the Rotate Left Button is clicked
    public void rotateCounterClockwise()
    {
        if (inputText.text == "/r pipe right" || inputText.text == "/r pipe left")
        {
            inputText.text = "/r pipe left";
        }
        else
        {
            inputText.text += "pipe left";
        }
    }
    //Deactivates the UI Game Essentials and sets the bool helpPause to true so the Help Screen is visible
    public void helpSelect()
    {
        bool isActive = true;
        if (SlideRotatePanel.activeSelf == isActive)
        {
            SlideRotatePanel.SetActive(!isActive);
            SlideRotatePanelActive = isActive;
        }
        else if(SlideCommandsPanel.activeSelf == isActive)
        {
            SlideCommandsPanel.SetActive(!isActive);
            SlideCommandsPanelActive = isActive;
        }
        else if (RotateCommandsPanel.activeSelf == isActive)
        {
            RotateCommandsPanel.SetActive(!isActive);
            RotateCommandsPanelActive = isActive;
        }
        else if (AddSubstractPanel.activeSelf == isActive)
        {
            AddSubstractPanel.SetActive(!isActive);
            AddSubstractPanelActive = isActive;
        }
        CounterContainer.SetActive(false);
        OnScreenTerminal.SetActive(false);
        LevelDialogueName.SetActive(false);
        LevelDialogueText.SetActive(false);
        LevelDialogueContinue.SetActive(false);
        Color color = DialougeBoxImage.color;
        color.a = 0;
        DialougeBoxImage.color = color;
        enterButton.SetActive(false);
        helpPause = true;
    }
    //When the Back button is clicked, the previous button commands are now visible and the PlayerTerminal deletes text
    //based on what command panel the Back button was pressed
    public void backSelect()
    {
        bool isActive = true;
        if(SlideCommandsPanel.activeSelf == isActive)
        {
            SlideCommandsPanel.SetActive(!isActive);
            SlideRotatePanel.SetActive(isActive);
            inputText.text = "Waiting for command...";
        }
        else if (RotateCommandsPanel.activeSelf == isActive)
        {
            RotateCommandsPanel.SetActive(!isActive);
            SlideRotatePanel.SetActive(isActive);
            inputText.text = "Waiting for command...";
        }
        else if(AddSubstractPanel.activeSelf == isActive)
        {
            SlideCommandsPanel.SetActive(isActive);
            AddSubstractPanel.SetActive(!isActive);
            inputText.text = "/s ";
            increment = 1;
        }
    }
    //The increment in the PlayTerminal increases by 1 everytime the plus button is clicked
    public void plusSelect()
    {
        increment += 1;
        if (increment >= 11)
        {
            inputText.text = inputText.text.Replace(inputText.text.Substring(inputText.text.Length - 2), increment.ToString());
        }
        else
        {
            inputText.text = inputText.text.Replace("" + inputText.text[inputText.text.Length - 1], "" + increment.ToString());
        }
    }
    //The increment in the PlayTerminal decreases by 1 everytime the minus button is clicked
    public void substractSelect()
    {
        if (increment >= 2 && increment < 10 && increment >= 1)
        {
            increment -= 1;
            inputText.text = inputText.text.Replace("" + inputText.text[inputText.text.Length - 1], "" + increment.ToString());
        }
        else if (increment >= 2 && increment == 10)
        {
            increment -= 1;
            inputText.text = inputText.text.Replace(" 10", " " + increment.ToString());
        }
        else if (increment >= 2 && increment > 10)
        {
            increment -= 1;
            inputText.text = inputText.text.Replace(inputText.text.Substring(inputText.text.Length - 2), increment.ToString());
        }
    }
    //When Enter Button is selected, the console text is updated based on the syntax of the command and a pipe being selected
    public void EnterSelect()
    {
        if (inputText.text == "Waiting for command...")
        {
            consoleText.text = "No command is in the terminal";
            GameManager.Instance.FailCommands += 1;
            GameManager.Instance.TotalCommands += 1;
            TickDown();
        }
        else if (command == "/s ")
        {
            consoleText.text = "This command could not be recognized.";
            inputText.text = "Waiting for command...";
            SlideCommandsPanel.SetActive(false);
            SlideRotatePanel.SetActive(true);
            TickDown();
            GameManager.Instance.PartialSyntaxCommands += 1;
            GameManager.Instance.TotalCommands += 1;
        }
        else if (command == "/r ")
        {
            consoleText.text = "This command could not be recognized.";
            inputText.text = "Waiting for command...";
            RotateCommandsPanel.SetActive(false);
            SlideRotatePanel.SetActive(true);
            TickDown();
            GameManager.Instance.PartialSyntaxCommands += 1;
            GameManager.Instance.TotalCommands += 1;
        }
        //If a pipe is magenta and the command in the terminal is "/r pipe left," the pipe is rotated
        else if (command == "/r pipe left")
        {
            Debug.Log("RotateLeft");
            pipe1.GetComponent<GamePipe>().RotateCommandLeft();
            inputText.text = "Waiting for command...";
            RotateCommandsPanel.SetActive(false);
            SlideRotatePanel.SetActive(true);
            if (GamePipe.pipeRenderer1.color == Color.magenta)
            {
                consoleText.text = "Pipe " + pipe1Name + " has been rotated left";
            }
            else if (GamePipe.pipeRenderer1.color == Color.white)
            {
                consoleText.text = "No pipe has been selected";
            }
        }
        else if (command == "/r pipe right")
        {
            Debug.Log("RotateRight");
            pipe1.GetComponent<GamePipe>().RotateCommandRight();
            inputText.text = "Waiting for command...";
            RotateCommandsPanel.SetActive(false);
            SlideRotatePanel.SetActive(true);
            if (GamePipe.pipeRenderer1.color == Color.magenta)
            {
                consoleText.text = "Pipe " + pipe1Name + " has been rotated right";
            }
            else if (GamePipe.pipeRenderer1.color == Color.white)
            {
                consoleText.text = "No pipe has been selected";
            }
        }
        //If a pipe is magenta, a collided pipe and grid boundary is not in the magenta pipe's path, 
        //and the command is "/s pipe (direction) increment" then hhe pipe can be moved.
        else if (command == "/s pipe right " + increment)
        {
            Debug.Log("Slide Right");
            pipe1.GetComponent<GamePipe>().SlideCommandRight();
            increment = 1;
            inputText.text = "Waiting for command...";
            bool isActive = true;
            if (SlideCommandsPanel.activeSelf == isActive)
            {
                SlideCommandsPanel.SetActive(!isActive);
                SlideRotatePanel.SetActive(isActive);
            }
            else if (AddSubstractPanel.activeSelf == isActive)
            {
                AddSubstractPanel.SetActive(!isActive);
                SlideRotatePanel.SetActive(isActive);
            }

            if (collideInput == true)
            {
                consoleText.text = "Pipe " + pipe1Name + " has moved right";
                collideInput = false;
            }
            else if (collideInput == false && GamePipe.pipeRenderer1.color == Color.white)
            {
                consoleText.text = "No pipe has been selected";
            }
            else if (collideInput == false && GamePipe.pipeRenderer1.color == Color.cyan && borderInput == false)
            {
                consoleText.text = "Pipe " + pipe1Name + " is colliding with " + "Pipe " + GamePipe.collidedPipe1.name;
            }
            else if (borderInput == true)
            {
                consoleText.text = "Pipe " + pipe1Name + " is colliding with the grid";
                borderInput = false;
            }
        }
        else if (command == "/s pipe left " + increment)
        {
            Debug.Log("Slide Left");
            pipe1.GetComponent<GamePipe>().SlideCommandLeft();
            increment = 1;
            inputText.text = "Waiting for command...";
            bool isActive = true;
            if (SlideCommandsPanel.activeSelf == isActive)
            {
                SlideCommandsPanel.SetActive(!isActive);
                SlideRotatePanel.SetActive(isActive);
            }
            else if (AddSubstractPanel.activeSelf == isActive)
            {
                AddSubstractPanel.SetActive(!isActive);
                SlideRotatePanel.SetActive(isActive);
            }
            if (collideInput == true)
            {
                consoleText.text = "Pipe " + pipe1Name + " has moved left";
                collideInput = false;
            }
            else if (collideInput == false && GamePipe.pipeRenderer1.color == Color.white)
            {
                consoleText.text = "No pipe has been selected";
            }
            else if (collideInput == false && GamePipe.pipeRenderer1.color == Color.cyan && borderInput == false)
            {
                consoleText.text = "Pipe " + pipe1Name + " is colliding with " + "Pipe " + GamePipe.collidedPipe1.name;
            }
            else if (borderInput == true)
            {
                consoleText.text = "Pipe " + pipe1Name + " is colliding with the grid";
                borderInput = false;
            }
        }
        else if (command == "/s pipe up " + increment)
        {
            Debug.Log("Slide Up");
            pipe1.GetComponent<GamePipe>().SlideCommandUp();
            increment = 1;
            inputText.text = "Waiting for command...";
            bool isActive = true;
            if (SlideCommandsPanel.activeSelf == isActive)
            {
                SlideCommandsPanel.SetActive(!isActive);
                SlideRotatePanel.SetActive(isActive);
            }
            else if (AddSubstractPanel.activeSelf == isActive)
            {
                AddSubstractPanel.SetActive(!isActive);
                SlideRotatePanel.SetActive(isActive);
            }
            if (collideInput == true)
            {
                consoleText.text = "Pipe " + pipe1Name + " has moved up";
                collideInput = false;
            }
            else if (collideInput == false && GamePipe.pipeRenderer1.color == Color.white)
            {
                consoleText.text = "No pipe has been selected";
            }
            else if (collideInput == false && GamePipe.pipeRenderer1.color == Color.cyan && borderInput == false)
            {
                consoleText.text = "Pipe " + pipe1Name + " is colliding with " + "Pipe " + GamePipe.collidedPipe1.name;
            }
            else if (borderInput == true)
            {
                consoleText.text = "Pipe " + pipe1Name + " is colliding with the grid";
                borderInput = false;
            }
        }
        else if (command == "/s pipe down " + increment)
        {
            Debug.Log("Slide Down");
            pipe1.GetComponent<GamePipe>().SlideCommandDown();
            increment = 1;
            inputText.text = "Waiting for command...";
            bool isActive = true;
            if (SlideCommandsPanel.activeSelf == isActive)
            {
                SlideCommandsPanel.SetActive(!isActive);
                SlideRotatePanel.SetActive(isActive);
            }
            else if (AddSubstractPanel.activeSelf == isActive)
            {
                AddSubstractPanel.SetActive(!isActive);
                SlideRotatePanel.SetActive(isActive);
            }
            if (collideInput == true)
            {
                consoleText.text = "Pipe " + pipe1Name + " has moved down";
                collideInput = false;
            }
            else if (collideInput == false && GamePipe.pipeRenderer1.color == Color.white)
            {
                consoleText.text = "No pipe has been selected";
            }
            else if (collideInput == false && GamePipe.pipeRenderer1.color == Color.cyan && borderInput == false)
            {
                consoleText.text = "Pipe " + pipe1Name + " is colliding with " + "Pipe " + GamePipe.collidedPipe1.name;
            }
            else if (borderInput == true)
            {
                consoleText.text = "Pipe " + pipe1Name + " is colliding with the grid";
                borderInput = false;
            }
        }

        if (count == 0)
        {
            loseScreen.SetActive(true);
            GameCanvas.SetActive(false);
            GameObject.Find("Grid").SetActive(false);
            gs2.GetComponent<WinDetect>().enabled = false;
        }
    }

    public void TickDown()
    {
        Counter = GameObject.Find("Counter");
        Text counttext = Counter.GetComponent<Text>();
        String countstring = counttext.text;
        count = int.Parse(countstring);
        count -= 1;
        counttext.text = count.ToString();
        float floatcount = count;
        Counter.GetComponent<CounterColor>().color = new Color (1,floatcount/startingcount, floatcount / startingcount, 1);
    }
}