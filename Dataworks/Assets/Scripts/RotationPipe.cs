using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotationPipe : MonoBehaviour
{
    public enum Rotations
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    };

    [System.Serializable]
    public struct RotationSprite
    {
        public Rotations rotation;
        public Sprite sprite;
    };

    public RotationSprite[] rotationSprites;

    public Rotations rotation;
    public Rotations Rotation
    {
        get { return rotation; }
        set { SetRotation(value); }
    }
    public int NumRotations
    {
        get { return rotationSprites.Length; }
    }

    private SpriteRenderer sprite;
    private Dictionary<Rotations, Sprite> rotationSpriteDict;

    private void Awake()
    {
        //this finds the "pipe" object which has the sprite renderer, and defines the sprites for NESW
        sprite = transform.Find("pipe").GetComponent<SpriteRenderer>();
        rotationSpriteDict = new Dictionary<Rotations, Sprite>();
        for (int i = 0; i < rotationSprites.Length; i++)
        {
            if (!rotationSpriteDict.ContainsKey(rotationSprites[i].rotation)){
                rotationSpriteDict.Add(rotationSprites[i].rotation, rotationSprites[i].sprite);
            }
        }
    }


    void Start()
    {
        
    }
    void Update()
    {

    }

    //this swaps the sprite of the pipe to match its rotation, and changes its rotation
    public void SetRotation(Rotations newRotation)
    {
        if (rotationSpriteDict.ContainsKey(newRotation))
        {
            sprite.sprite = rotationSpriteDict[newRotation];
            rotation = newRotation;
        }
    }
}
