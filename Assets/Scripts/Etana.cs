using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etana : MonoBehaviour
{
    Rigidbody2D rb2d;
    CircleCollider2D circleCollider;
    Animator anim;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }
}
