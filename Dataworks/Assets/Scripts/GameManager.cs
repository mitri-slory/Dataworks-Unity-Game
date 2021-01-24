//Dmitri's Script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //Global command variables that keep track of number of commands each level
    public int totalCommands = 0;
    public int failCommands = 0;
    public int successCommands = 0;
    public int partialSyntaxCommands = 0;
    public int fullSyntaxCommands = 0;

    public int TotalCommands
    {
        get { return totalCommands; }
        set { totalCommands = value; }
    }
    public int FailCommands
    {
        get { return failCommands; }
        set { failCommands = value; }
    }
    public int SuccessCommands
    {
        get { return successCommands; }
        set { successCommands = value; }
    }
    public int PartialSyntaxCommands
    {
        get { return partialSyntaxCommands; }
        set { partialSyntaxCommands = value; }
    }
    public int FullSyntaxCommands
    {
        get { return fullSyntaxCommands; }
        set { fullSyntaxCommands = value; }
    }

}
