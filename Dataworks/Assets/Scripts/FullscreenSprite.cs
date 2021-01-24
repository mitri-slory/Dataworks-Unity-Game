using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenSprite : MonoBehaviour
{
    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        //gets the apparent size of the camera and the apparent size of the background image
        float cameraHeight = Camera.main.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        //scales the image so that as much as possible is shown, but whole camera is always filled
        Vector2 scale = transform.localScale;
        if (cameraSize.x/spriteSize.x >= cameraSize.y/spriteSize.y)
        { 
            scale *= cameraSize.x / spriteSize.x;
        }
        else
        {
            scale *= cameraSize.y / spriteSize.y;
        }

        transform.localScale = scale;
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}
