using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingWheels : MonoBehaviour
{
    public bool PipeState;
    public bool ButtonState;

    public GameObject Grid;
    public GridScript GS;

    public GameObject currentpipe;
    public BoxCollider2D currentcollider;

    [System.Serializable]
    public struct button
    {
        public GameObject b;
    };
    public button[] buttons;


    void Start()
    {
        GS = Grid.GetComponent<GridScript>();
        //by default pipes and buttons are locked
        PipesUnlocked(false);
        ButtonsUnlocked(false);

    }

    public bool getPipesState
    {
        get { return PipeState; }
    }
    
    public bool getButtonState
    {

        get { return ButtonState; }
    }

    //this runs through all the pipes and enables their colliders
    public void PipesUnlocked(bool PipeBool)
    {
            for (int j = 0; j < GS.array.Length; j++)
            {
                print("(" + GS.array[j].xcoord + ", " + GS.array[j].ycoord + ")");
                currentpipe = GameObject.Find("(" + GS.array[j].xcoord + ", " + GS.array[j].ycoord + ")");
                currentcollider = currentpipe.GetComponent<BoxCollider2D>();
                currentcollider.enabled = PipeBool;

            }

        PipeState = PipeBool;
    }
    //this runs though the buttons on screen and makes them interactable
    public void ButtonsUnlocked(bool ButtonBool)
    {
        for (int k = 0; k < buttons.Length; k++)
        {
            buttons[k].b.GetComponent<Button>().interactable = ButtonBool;

        }

        ButtonState = ButtonBool;
    }
}
