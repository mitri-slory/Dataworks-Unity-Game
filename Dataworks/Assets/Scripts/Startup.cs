using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startup : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Checks is any key was pressed...
        if (Input.anyKeyDown)
        {
            //Loads next scene in Build Settings
            SceneManager.LoadScene("End");
        }
    }
}
