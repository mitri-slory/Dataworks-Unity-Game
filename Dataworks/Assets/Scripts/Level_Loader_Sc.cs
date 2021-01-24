using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Loader_Sc : MonoBehaviour
{
    public Animator transition;

    public bool AllowedToContinue;

    public float changeTime = 1f;

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey && AllowedToContinue)
        {
            LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void SetAllowedState(bool State)
    {
        AllowedToContinue = State;
    }


    public void LoadNextLevel(int levelnum)
    {
        FindObjectOfType<AudioManager>().FadeOutVolume("Theme");
        StartCoroutine(LoadLevel(levelnum));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(changeTime);

        SceneManager.LoadScene(levelIndex);
    }
}
