using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WinDetect : MonoBehaviour
{

    public int xstart = 0;
    public int ystart = 6;
    int x;
    int y;
    int xd;
    int yd;
    public int xend = 14;
    public int yend = 0;
    public bool broken = false;
    public GridScript gs;
    public CommandSelect cs;
    public int endXDir;
    public int endYDir;
    public int startXDir = 0;
    public int startYDir = -1;

    public bool won;
    public bool hasInvoked;

    public GameObject HighScoreCanvas;
    public HighScore hs;
    public GameObject ParentPipe;
    public GameObject ChildPipe;
    public SpriteRenderer childpipeRenderer;


    void Awake()
    {
        gs = GameObject.Find("Grid").GetComponent<GridScript>();
    }

    void Start()
    {
        
    }

    //this function checks if the current pipe configuration is a winning one
    void winCheck()
    {
        //set the x and y, and the direction to start from
        x = xstart;
        y = ystart;
        int xdir = startXDir;
        int ydir = startYDir;
        for (int i = 0; i<100; i++)
        {
            broken = false;
            //checks that there is an object adjacent
            if (GameObject.Find("(" + x + ", " + y + ")") != null)
            {
                GameObject onpipe = GameObject.Find("(" + x + ", " + y + ")");
                var type = onpipe.GetComponent<GamePipe>().Type;
                var rot = onpipe.GetComponent<RotationPipe>().Rotation;
                //checks for non useful pipes
                if (type == GridScript.PipeType.STRROCK)
                {
                    broken = true;
                }
                if (type == GridScript.PipeType.STRAIGHT)
                {
                    //makes sure straight pipes line up correctly
                    if ((xdir == 0 && (rot == RotationPipe.Rotations.NORTH || rot == RotationPipe.Rotations.SOUTH)) || (ydir == 0 && (rot == RotationPipe.Rotations.EAST || rot == RotationPipe.Rotations.WEST)))
                    {
                    }
                    else
                    {
                        broken = true;
                    }
                }

                if (type == GridScript.PipeType.ELBOW)
                {
                    //these ifs first verify that the pipes line up correctly, and then adjust the direction as necessary
                    if (ydir == -1)
                    {
                        if (rot == RotationPipe.Rotations.NORTH)
                        {
                            ydir = 0;
                            xdir = 1;
                        }

                        else if (rot == RotationPipe.Rotations.WEST)
                        {
                            ydir = 0;
                            xdir = -1;
                        }
                        else
                        {
                            broken = true;
                        }
                    }


                    else if (ydir == 1)
                    {
                        if (rot == RotationPipe.Rotations.EAST)
                        {
                            ydir = 0;
                            xdir = 1;
                        }

                        else if (rot == RotationPipe.Rotations.SOUTH)
                        {
                            ydir = 0;
                            xdir = -1;
                        }
                        else
                        {
                            broken = true;
                        }
                    }
                    else if (xdir == -1)
                    {
                        if (rot == RotationPipe.Rotations.NORTH)
                        {
                            ydir = 1;
                            xdir = 0;
                        }

                        else if (rot == RotationPipe.Rotations.EAST)
                        {
                            ydir = -1;
                            xdir = 0;
                        }
                        else
                        {
                            broken = true;
                        }
                    }


                    else if (xdir == 1)
                    {
                        if (rot == RotationPipe.Rotations.WEST)
                        {
                            ydir = 1;
                            xdir = 0;
                        }

                        else if (rot == RotationPipe.Rotations.SOUTH)
                        {
                            ydir = -1;
                            xdir = 0;
                        }
                        else
                        {
                            broken = true;
                        }
                    }
                }
            }
            // if the pipes dont line up, its broken
            else
            {
                broken = true;
            }
            //take a not of the end direction and break
            if (broken)
            {
                xd = xdir;
                yd = ydir;
                break;
            }
            //if its not broken, follow the path of the pipes
            else
            {
                y += ydir;
                x += xdir;
            }
            //if it goes out of bounds, its broken
            if (y < 0 || x < 0 || y > gs.yDim || x > gs.xDim)
            {
                broken = true;
                xd = xdir;
                yd = ydir;
                break;
            }
            //this checks if the pipe path leads to the end pipe in the right direction, this is the only "break" that doesnt set broken to true, which is important for checking the win
            if (x==xend && y == yend && xdir == endXDir && ydir == endYDir)
            {

                xd = xdir;
                yd = ydir;
                break;
            }

        }
    }
 
    void Update()
    {
       winCheck();
       won = !broken;
        //print(x + ", " + y + ", " + xd + ", " + yd);
        if (won)
        {
            //Every pipe in the scene turns green when the level is won
            for (int i = 0; i < gs.xDim; i++)
            {
                for(int j = 0; j < gs.yDim; j++)
                {
                    ParentPipe = GameObject.Find("(" + i + ", " + j + ")");
                    if (ParentPipe != null)
                    {
                        ChildPipe = ParentPipe.transform.Find("pipe").gameObject;
                        childpipeRenderer = ChildPipe.GetComponent<SpriteRenderer>();
                        childpipeRenderer.color = Color.green;
                    }
                }
            }

            if (!hasInvoked)
            {
                //HighScoreCanvas is Open when player has won the game after a few seconds
                Invoke("YouWinScreen", 3f);
                hasInvoked = true;
            }
        }
    }
    //When the level is won, the HighScoreCanvas is the only UI element visible
    public void YouWinScreen() {
        HighScoreCanvas.SetActive(true);
        hs.HighScoreUpdate();
        cs.Grid.SetActive(false);
        cs.GameCanvas.SetActive(false);
        gs.GetComponent<WinDetect>().enabled = false;
    }
}
