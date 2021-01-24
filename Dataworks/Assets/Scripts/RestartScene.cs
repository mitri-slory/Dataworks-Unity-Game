//Dmitri's Script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Restarts the level and resets the number of commands the player has made that level to 0
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.TotalCommands = 0;
        GameManager.Instance.SuccessCommands = 0;
        GameManager.Instance.FullSyntaxCommands = 0;
        GameManager.Instance.PartialSyntaxCommands = 0;
        GameManager.Instance.FailCommands = 0;
    }

    public void MoveOn()
    {
        FindObjectOfType<Level_Loader_Sc>().LoadNextLevel((SceneManager.GetActiveScene().buildIndex) + 1);
    }
}
