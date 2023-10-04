using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RepeatingGround : MonoBehaviour
{
    Vector2 startPos;
    float repeatWidth;
    SpriteRenderer sprite;

    private void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<SpriteRenderer>().size.x / 2;
    }

    private void Update()
    {
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
