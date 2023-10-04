using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    Vector2 startPos;
    float repeatWidth;
    SpriteRenderer sprite;

    private void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x / 2;
        //repeatWidth = GetComponent<SpriteRenderer>().size.x / 2;
    }

    private void Update()
    {
        if(transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
