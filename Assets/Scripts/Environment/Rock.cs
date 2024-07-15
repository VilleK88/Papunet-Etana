using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    Rigidbody2D rb2d;
    float rollSpeed = 3.0f;
    float jumpForce = 4.6f; // original jump force 4.6f

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        DestroyOutOfBounds();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb2d.velocity = new Vector2(-rollSpeed, rb2d.velocity.y);
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            rb2d.velocity = new Vector2(-rollSpeed, rb2d.velocity.y);
            rb2d.AddForce(new Vector2(0, jumpForce + 6), ForceMode2D.Impulse);
        }
    }

    void DestroyOutOfBounds()
    {
        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }
}
