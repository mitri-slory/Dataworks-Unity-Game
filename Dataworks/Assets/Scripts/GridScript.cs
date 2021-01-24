using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GridScript : MonoBehaviour
{
    public enum PipeType
    {
        STRAIGHT,
        ELBOW,
        PLUS,
        T_INTER,
        NONE,
        STARTSTOP,
        BARRIER,
        STRROCK,
    };

    [System.Serializable]
    public struct PipePrefab
    {
        public PipeType type;
        public GameObject prefab;
    };

    public int xDim;
    public int yDim;
    public float ypos = 0.0f;

    public PipePrefab[] pipePrefabs;
    public GameObject backgroundPrefab;

    private Dictionary<PipeType, GameObject> pipePrefabDict;

    public GamePipe[,] pipes;


    [System.Serializable]
    public struct PipesToGenerate
    {
        public int xcoord;
        public int ycoord;
        public PipeType t;
        public RotationPipe.Rotations orientation;
    };
    public PipesToGenerate[] array;



    void Awake()
    {
        pipePrefabDict = new Dictionary<PipeType, GameObject>();
        //assigning the pipe prefabs to their specific types
        for (int i = 0; i < pipePrefabs.Length; i++)
        {
            if (!pipePrefabDict.ContainsKey(pipePrefabs[i].type))
            {
                pipePrefabDict.Add(pipePrefabs[i].type, pipePrefabs[i].prefab);
            }
        }
        //creating the background squares
        for (int x = 0; x < xDim; x++){
             for (int y = 0; y < yDim; y++){
                GameObject background = (GameObject)Instantiate(backgroundPrefab, GetWorldPosition(x, y), Quaternion.identity);
                background.transform.parent = transform;
            }
        }

        

     
        // an array to store the pipes
        pipes = new GamePipe[xDim, yDim];

        //creating the starting and ending pipes
        WinDetect win = this.GetComponent<WinDetect>();
        SpawnNewPipe(win.xstart, win.ystart, PipeType.STARTSTOP);
        StartEndRot(GameObject.Find("(" + win.xstart + ", " + win.ystart + ")"), 1);
        SpawnNewPipe(win.xend, win.yend, PipeType.STARTSTOP);
        StartEndRot(GameObject.Find("(" + win.xend + ", " + win.yend + ")"), -1);

        //creates the pipes from the level loader
        for (int j = 0; j<array.Length; j++)
        {
            SpawnNewPipe(array[j].xcoord, array[j].ycoord, array[j].t);
            GameObject.Find("(" + array[j].xcoord + ", " + array[j].ycoord + ")").GetComponent<RotationPipe>().SetRotation(array[j].orientation);
        }

        //scaling the grid so it looks good at any size
        float xprop = xDim / 15f;
        float yprop = yDim / 7f;

        if(xprop >= yprop)
        {
            this.transform.localScale = new Vector3(1 / xprop, 1/xprop, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(1 / yprop, 1/yprop,1);
        }
    }

    void Update()
        {

        }
    //used for placing the grids objects, as well as for moving the grid up and down for tweaking
    public Vector2 GetWorldPosition (int x, int y)
    {
        return new Vector2(transform.position.x - xDim / 2.0f + x+.5f,
            transform.position.y -yDim/2.0f +ypos*yDim + y);
    }

    //this is the function thats spawns in pipes 
    public GamePipe SpawnNewPipe(int x, int y, PipeType type)
    {
        GameObject newPipe = (GameObject)Instantiate(pipePrefabDict[type], GetWorldPosition(x, y), Quaternion.identity);
        newPipe.transform.parent = transform;
        newPipe.name = "(" + x + ", " + y + ")";
        pipes[x, y] = newPipe.GetComponent<GamePipe>();
        pipes[x, y].Init(x, y, this, type);
        return pipes[x, y];
    }
    //this function determines the NESW rotation of the starting/ending pipe
    public void StartEndRot(GameObject Pipe, int define)
    {
        WinDetect win = this.GetComponent<WinDetect>();
        if (define == 1)
        {
            if (win.startXDir * define == 1)
            {
                Pipe.GetComponent<RotationPipe>().SetRotation(RotationPipe.Rotations.EAST);
            }
            if (win.startXDir * define == -1)
            {
                Pipe.GetComponent<RotationPipe>().SetRotation(RotationPipe.Rotations.WEST);
            }
            if (win.startYDir * define == 1)
            {
                Pipe.GetComponent<RotationPipe>().SetRotation(RotationPipe.Rotations.NORTH);
            }
            if (win.startYDir * define == -1)
            {
                Pipe.GetComponent<RotationPipe>().SetRotation(RotationPipe.Rotations.SOUTH);
            }
        }
        else
        {
            if ((win.endXDir * define) == 1)
            {
                Pipe.GetComponent<RotationPipe>().SetRotation(RotationPipe.Rotations.EAST);
            }
            if ((win.endXDir * define) == -1)
            {
                Pipe.GetComponent<RotationPipe>().SetRotation(RotationPipe.Rotations.WEST);
            }
            if ((win.endYDir * define) == 1)
            {
                Pipe.GetComponent<RotationPipe>().SetRotation(RotationPipe.Rotations.NORTH);
            }
            if ((win.endYDir * define) == -1)
            {
                Pipe.GetComponent<RotationPipe>().SetRotation(RotationPipe.Rotations.SOUTH);
            }
        }
    }
}
