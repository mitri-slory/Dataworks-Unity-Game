using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePipe : MonoBehaviour
{

    private GamePipe pipe;
    public GameObject grid;
    public GridScript gs;

    private void Awake()
    {
        pipe = GetComponent<GamePipe>();
        grid = GameObject.Find("Grid");
        gs = grid.GetComponent<GridScript>();
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
    //a function to move the pipe given an x and y
    public void Move(int newX, int newY)
    {
        gs.pipes[newX, newY] = pipe;
        this.name = "(" + newX + ", " + newY + ")";

        pipe.transform.localPosition = pipe.GridScriptRef.GetWorldPosition(newX, newY);
        pipe.X = newX;
        pipe.Y = newY;
    }
}
