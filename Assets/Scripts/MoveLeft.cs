using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    float speed = 20;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
