using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalFlash : MonoBehaviour
{
    public int currentFails;
    public int currentSuccess;
    public int currentPartial;
    public int currentFullSyntax;

    public Animator AnimController;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.FailCommands != currentFails)
        {
            StartCoroutine(Flash("Red"));
            currentFails += 1;
            FindObjectOfType<AudioManager>().Play("RedError");
        }
        else if (GameManager.Instance.PartialSyntaxCommands != currentPartial)
        {
            StartCoroutine(Flash("Yellow"));
            currentPartial += 1;
            FindObjectOfType<AudioManager>().Play("YellowError");
        }
        else if (GameManager.Instance.FullSyntaxCommands != currentFullSyntax)
        {
            StartCoroutine(Flash("Yellow"));
            currentFullSyntax += 1;
            FindObjectOfType<AudioManager>().Play("YellowError");
        }
        else if (GameManager.Instance.SuccessCommands != currentSuccess)
        {
            StartCoroutine(Flash("Green"));
            currentSuccess += 1;
        }
    }

    IEnumerator Flash(string trigger)
    {
        //Play animation
        AnimController.SetTrigger(trigger);

        //Wait
        yield return new WaitForSeconds(0.3f);

        AnimController.Play("Waiting");
        
    }




}
