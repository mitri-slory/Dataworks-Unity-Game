using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimTrigger : MonoBehaviour
{
    public Animator AnimController;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            FindObjectOfType<AudioManager>().Play("GoodConfirm");
            AnimController.SetTrigger("KeyPress");
        }

    }
}
