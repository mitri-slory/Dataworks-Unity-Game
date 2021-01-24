//Dmitri's Script
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TigerForge;

public class GamePipe : MonoBehaviour
{
    
    private int x;
    private int y;

    public bool rotate = false;
    public bool slideRight = false;
    public bool slideLeft = false;
    public bool slideUp = false;
    public bool slideDown = false;

    public static SpriteRenderer pipeRenderer1;
    public static SpriteRenderer pipeRenderer2;

    public GameObject pipe1;
    public static string pipe1Name;
    public GameObject childPipe1;
    public GameObject pipe2;
    public GameObject childPipe2;
    public static GameObject collidedPipe1;
    public GameObject collidedPipe1Child;
    public GameObject borderPipe;

    public bool selected = false;
    public GridScript gs2;
    public CommandSelect cs;
    
    
    public int X
    {
        get { return x; }
        set
        {
            if (IsMovable())
            {
                x = value;
            }
        }
    }
    public int Y
    {
        get { return y; }
        set
        {
            if (IsMovable())
            {
                y = value;
            }
        }
    }

    private GridScript.PipeType type;

    public GridScript.PipeType Type
    {
        get { return type; }
    }

    private GridScript GridScript;

    public GridScript GridScriptRef
    {
        get { return GridScript; }
    }

    private MovablePipe movableComponent;

    public MovablePipe MovableComponent
    {
        get { return movableComponent; }
    }

    private RotationPipe rotationComponent;

    public RotationPipe RotationComponent
    {
        get { return rotationComponent; }
    }


    private void Awake()
    {
        movableComponent = GetComponent<MovablePipe>();
        rotationComponent = GetComponent<RotationPipe>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Setting the child object of the Pipe objects to the Sprites of the Pipes themselves
        childPipe1 = pipe1.transform.Find("pipe").gameObject;
        pipeRenderer1 = childPipe1.GetComponent<SpriteRenderer>();
        //pipe2 is currently the same as pipe1 when the level begins
        childPipe2 = pipe2.transform.Find("pipe").gameObject;
        pipeRenderer2 = childPipe2.GetComponent<SpriteRenderer>();
        pipe1Name = pipe1.name;

    }

    // Update is called once per frame
    void Update()
    {
        //When a pipe turns magenta, the pipe2 object is equvalent to the pipe that is magenta in the scene
        if (pipeRenderer1.color == Color.magenta)
        {
            //pipe1 for each pipe remains the same throughout the level. Only pipe2 is updated throuhgout the level
            pipe2 = GameObject.Find(pipe1Name);
            childPipe2 = pipe2.transform.Find("pipe").gameObject;
        }
    }
    public void Init(int _x, int _y, GridScript _GridScript, GridScript.PipeType _type)
    {
        x = _x;
        y = _y;
        GridScript = _GridScript;
        type = _type;
    }
    
    public bool IsMovable()
    {
        return movableComponent != null;
    }
    public bool IsRotation()
    {
        return rotationComponent != null;
    }
    public void OnMouseDown()
    {
        //When the help or pause menu is open, the player can't click on a GameObject that is behind the UIs
        if (EventSystem.current.IsPointerOverGameObject())
        {
            print("Clicked on the UI");
        }

        else
        {
            //When a pipe is clicked on while another pipe is magneta in the scene, the original magenta pipe becomes white again
            pipeRenderer2 = childPipe2.GetComponent<SpriteRenderer>();
            pipeRenderer2.color = Color.white;
            pipeRenderer1 = childPipe1.GetComponent<SpriteRenderer>();
            //When the selected bool is false and a pipe is clicked on, the pipe turns a mangenta color
            if (!selected)
            {
                pipeRenderer1.color = Color.magenta;
                pipe1Name = pipe1.name;
                print(pipe1Name);
                selected = true;
                FindObjectOfType<AudioManager>().Play("PipeClicked");
            }
            //When the selected bool is true and a magenta pipe is clicked on, the pipe returns back to white
            else if (selected)
            {
                pipeRenderer1.color = Color.white;
                selected = false;
            }
        }
    }


    public void RotateCommandRight()
    {
        //Getting access to the ComandSelect global variables which are attached to the PlayterTerminalObject
        cs = GameObject.Find("PlayerTerminalObject").GetComponent<CommandSelect>();

        //When a pipe is magenta, the pipe is rotated clockwise of their original orientation
        if (selected)
        {
            if (this.RotationComponent.Rotation == RotationPipe.Rotations.NORTH)
            {
                this.RotationComponent.SetRotation(RotationPipe.Rotations.EAST);
            }

            else if (this.RotationComponent.Rotation == RotationPipe.Rotations.EAST)
            {
                this.RotationComponent.SetRotation(RotationPipe.Rotations.SOUTH);
            }
            else if (this.RotationComponent.Rotation == RotationPipe.Rotations.SOUTH)
            {
                this.RotationComponent.SetRotation(RotationPipe.Rotations.WEST);
            }
            else if (this.RotationComponent.Rotation == RotationPipe.Rotations.WEST)
            {
                this.RotationComponent.SetRotation(RotationPipe.Rotations.NORTH);
            }
            GameManager.Instance.SuccessCommands += 1;
            GameManager.Instance.TotalCommands += 1;
            FindObjectOfType<AudioManager>().Play("R_Right");
        }
        //If no pipe is magenta and a command is passed, the FullSyntaxCommands and TotalCommands values increment by 1
        else
        {
            GameManager.Instance.FullSyntaxCommands += 1;
            GameManager.Instance.TotalCommands += 1;
        }

        cs.TickDown();
        //When count becomes 0, the player loses the level and the lose screen is the only UI element visible
        if (cs.count == 0)
        {
            cs.loseScreen.SetActive(true);
            cs.GameCanvas.SetActive(false);
            GameObject.Find("Grid").SetActive(false);
            gs2.GetComponent<WinDetect>().enabled = false;
        }
    }

    public void RotateCommandLeft()
    {
        //Getting access to the ComandSelect global variables which are attached to the PlayterTerminalObject
        cs = GameObject.Find("PlayerTerminalObject").GetComponent<CommandSelect>();
            //When a pipe is magenta, the pipe is rotated counterclockwise of their original orientation
            if (selected)
            {

                if (this.RotationComponent.Rotation == RotationPipe.Rotations.NORTH)
                {
                    this.RotationComponent.SetRotation(RotationPipe.Rotations.WEST);
                }
                else if (this.RotationComponent.Rotation == RotationPipe.Rotations.EAST)
                {
                    this.RotationComponent.SetRotation(RotationPipe.Rotations.NORTH);
                }
                else if (this.RotationComponent.Rotation == RotationPipe.Rotations.SOUTH)
                {
                    this.RotationComponent.SetRotation(RotationPipe.Rotations.EAST);
                }
                else if (this.RotationComponent.Rotation == RotationPipe.Rotations.WEST)
                {
                    this.RotationComponent.SetRotation(RotationPipe.Rotations.SOUTH);
                }
                FindObjectOfType<AudioManager>().Play("R_Left");
                GameManager.Instance.SuccessCommands += 1;
                GameManager.Instance.TotalCommands += 1;
            }
            //If no pipe is magenta and a command is passed, the FullSyntaxCommands and TotalCommands values increment by 1
            else
            {
                GameManager.Instance.FullSyntaxCommands += 1;
                GameManager.Instance.TotalCommands += 1;
            }

        cs.TickDown();
        //When count becomes 0, the player loses the level and the lose screen is the only UI element visible
        if (cs.count == 0)
        {
            cs.loseScreen.SetActive(true);
            cs.GameCanvas.SetActive(false);
            GameObject.Find("Grid").SetActive(false);
            gs2.GetComponent<WinDetect>().enabled = false;
        }
    }

    public void SlideCommandUp()
    {
        //Getting access to the ComandSelect global variables which are attached to the PlayterTerminalObject
        cs = GameObject.Find("PlayerTerminalObject").GetComponent<CommandSelect>();
        //When a pipe is magenta, the pipe is moved up by 1 unit on the grid
        if (selected)
        {
            for (int i = 0; i < CommandSelect.increment; i++)
            {
                collidedPipe1 = GameObject.Find("(" + (X).ToString() + ", " + (Y + 1).ToString() + ")");
                gs2 = GameObject.Find("Grid").GetComponent<GridScript>();
                //These if/else conditionals check if there is a pipe or grid boundary that the selected pipe can collide with
                if (collidedPipe1 == null && Y < gs2.yDim - 1)
                {
                    this.MovableComponent.Move(X, Y + 1);
                    pipe1Name = pipe1.name;
                    CommandSelect.collideInput = true;
                }
                else if (collidedPipe1 != null)
                {
                    Debug.Log(collidedPipe1.name);
                    CommandSelect.collideInput = false;
                }
                else
                {
                    CommandSelect.borderInput = true;
                }
            }
            //The Command global variables from GameManager update based on if the selected pipe does or doesn't collide with other objects
            if (CommandSelect.collideInput == false)
            {
                GameManager.Instance.FullSyntaxCommands += 1;
                GameManager.Instance.TotalCommands += 1;
            }
            else if (CommandSelect.borderInput == true)
            {
                GameManager.Instance.FullSyntaxCommands += 1;
                GameManager.Instance.TotalCommands += 1;
            }
            else if (CommandSelect.collideInput == true)
            {
                GameManager.Instance.SuccessCommands += 1;
                GameManager.Instance.TotalCommands += 1;
            }
        }
        //If no pipe is magenta and a command is passed, the FullSyntaxCommands and TotalCommands values increment by 1
        else
        {
            GameManager.Instance.FullSyntaxCommands += 1;
            GameManager.Instance.TotalCommands += 1;
        }

        FindObjectOfType<AudioManager>().Play("Slide_Normal");
        cs.TickDown();
        //When count becomes 0, the player loses the level and the lose screen is the only UI element visible
        if (cs.count == 0)
        {
            cs.loseScreen.SetActive(true);
            cs.GameCanvas.SetActive(false);
            GameObject.Find("Grid").SetActive(false);
            gs2.GetComponent<WinDetect>().enabled = false;
        }
    }

    public void SlideCommandDown()
    {
        //Getting access to the ComandSelect global variables which are attached to the PlayterTerminalObject
        cs = GameObject.Find("PlayerTerminalObject").GetComponent<CommandSelect>();

        //When a pipe is magenta, the pipe is moved down by 1 unit on the grid
        if (selected)
        {
            for (int i = 0; i < CommandSelect.increment; i++)
            {
                collidedPipe1 = GameObject.Find("(" + (X).ToString() + ", " + (Y - 1).ToString() + ")");
                gs2 = GameObject.Find("Grid").GetComponent<GridScript>();
                //These if/else conditionals check if there is a pipe or grid boundary that the selected pipe can collide with
                if (collidedPipe1 == null && Y > 0)
                {
                    this.MovableComponent.Move(X, Y - 1);
                    pipe1Name = pipe1.name;
                    CommandSelect.collideInput = true;
                }
                else if (collidedPipe1 != null)
                {
                    Debug.Log(collidedPipe1.name);
                    CommandSelect.collideInput = false;
                }
                else
                {
                    CommandSelect.borderInput = true;
                }
            }
            //The Command global variables from GameManager update based on if the selected pipe does or doesn't collide with other objects.
            if (CommandSelect.collideInput == false)
            {
                GameManager.Instance.FullSyntaxCommands += 1;
                GameManager.Instance.TotalCommands += 1;
            }
            else if (CommandSelect.borderInput == true)
            {
                GameManager.Instance.FullSyntaxCommands += 1;
                GameManager.Instance.TotalCommands += 1;
            }
            else if (CommandSelect.collideInput == true)
            {
                GameManager.Instance.SuccessCommands += 1;
                GameManager.Instance.TotalCommands += 1;
            }
        }
        //If no pipe is magenta and a command is passed, the FullSyntaxCommands and TotalCommands values increment by 1
        else
        {
            GameManager.Instance.FullSyntaxCommands += 1;
            GameManager.Instance.TotalCommands += 1;
        }
        FindObjectOfType<AudioManager>().Play("Slide_Normal");
        cs.TickDown();
        //When count becomes 0, the player loses the level and the lose screen is the only UI element visible
        if (cs.count == 0)
        {
            cs.loseScreen.SetActive(true);
            cs.GameCanvas.SetActive(false);
            GameObject.Find("Grid").SetActive(false);
            gs2.GetComponent<WinDetect>().enabled = false;
        }
    }

    public void SlideCommandRight()
    {
        //Getting access to the ComandSelect global variables which are attached to the PlayterTerminalObject
        cs = GameObject.Find("PlayerTerminalObject").GetComponent<CommandSelect>();
        //When a pipe is magenta, the pipe is moved right by 1 unit on the grid
        if (selected)
        {
            for (int i = 0; i < CommandSelect.increment; i++)
            {
                collidedPipe1 = GameObject.Find("(" + (X + 1).ToString() + ", " + Y.ToString() + ")");
                gs2 = GameObject.Find("Grid").GetComponent<GridScript>();
                //These if/else conditionals check if there is a pipe or grid boundary that the selected pipe can collide with
                if (collidedPipe1 == null && X < gs2.xDim - 1)
                {
                    this.MovableComponent.Move(X + 1, Y);
                    pipe1Name = pipe1.name;
                    CommandSelect.collideInput = true;
                }
                else if (collidedPipe1 != null)
                {
                    Debug.Log(collidedPipe1.name);
                    CommandSelect.collideInput = false;
                }
                else
                {
                    CommandSelect.borderInput = true;
                }
            }
            //The Command global variables from GameManager update based on if the selected pipe does or doesn't collide with other objects.
            if (CommandSelect.collideInput == false)
            {
                GameManager.Instance.FullSyntaxCommands += 1;
                GameManager.Instance.TotalCommands += 1;
            }
            else if (CommandSelect.borderInput == true)
            {
                GameManager.Instance.FullSyntaxCommands += 1;
                GameManager.Instance.TotalCommands += 1;
            }
            else if (CommandSelect.collideInput == true)
            {
                GameManager.Instance.SuccessCommands += 1;
                GameManager.Instance.TotalCommands += 1;
            }
        }
        //If no pipe is magenta and a command is passed, the FullSyntaxCommands and TotalCommands values increment by 1
        else
        {
            GameManager.Instance.FullSyntaxCommands += 1;
            GameManager.Instance.TotalCommands += 1;
        }
        FindObjectOfType<AudioManager>().Play("Slide_Normal");
        cs.TickDown();
        //When count becomes 0, the player loses the level and the lose screen is the only UI element visible
        if (cs.count == 0)
        {
            cs.loseScreen.SetActive(true);
            cs.GameCanvas.SetActive(false);
            GameObject.Find("Grid").SetActive(false);
            gs2.GetComponent<WinDetect>().enabled = false;
        }
    }


    public void SlideCommandLeft()
    {
        //Getting access to the ComandSelect global variables which are attached to the PlayterTerminalObject
        cs = GameObject.Find("PlayerTerminalObject").GetComponent<CommandSelect>();
        //When a pipe is magenta, the pipe is moved left by 1 unit on the grid
        if (selected)
        {
            for (int i = 0; i < CommandSelect.increment; i++)
            {
                collidedPipe1 = GameObject.Find("(" + (X - 1).ToString() + ", " + Y.ToString() + ")");
                gs2 = GameObject.Find("Grid").GetComponent<GridScript>();
                //These if/else conditionals check if there is a pipe or grid boundary that the selected pipe can collide with
                if (collidedPipe1 == null && X > 0)
                {
                    this.MovableComponent.Move(X - 1, Y);
                    pipe1Name = pipe1.name;
                    CommandSelect.collideInput = true;
                }
                else if (collidedPipe1 != null)
                {
                    Debug.Log(collidedPipe1.name);
                    CommandSelect.collideInput = false;
                }
                else
                {
                    CommandSelect.borderInput = true;
                }
            }
            //The Command global variables from GameManager update based on if the selected pipe does or doesn't collide with other objects.
            if (CommandSelect.collideInput == false)
            {
                GameManager.Instance.FullSyntaxCommands += 1;
                GameManager.Instance.TotalCommands += 1;
            }
            else if (CommandSelect.borderInput == true)
            {
                GameManager.Instance.FullSyntaxCommands += 1;
                GameManager.Instance.TotalCommands += 1;
            }
            else if (CommandSelect.collideInput == true)
            {
                GameManager.Instance.SuccessCommands += 1;
                GameManager.Instance.TotalCommands += 1;
            }
        }
        //If no pipe is magenta and a command is passed, the FullSyntaxCommands and TotalCommands values increment by 1
        else
        {
            GameManager.Instance.FullSyntaxCommands += 1;
            GameManager.Instance.TotalCommands += 1;
        }
        FindObjectOfType<AudioManager>().Play("Slide_Normal");
        cs.TickDown();
        //When count becomes 0, the player loses the level and the lose screen is the only UI element visible
        if (cs.count == 0)
        {
            cs.loseScreen.SetActive(true);
            cs.GameCanvas.SetActive(false);
            GameObject.Find("Grid").SetActive(false);
            gs2.GetComponent<WinDetect>().enabled = false;
        }
    }
}

