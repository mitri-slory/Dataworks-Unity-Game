using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class EventEmit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnMouseDown()
    {
        EventManager.EmitEvent("ROTATE");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
