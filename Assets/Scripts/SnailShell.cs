using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailShell : MonoBehaviour
{
    [SerializeField] Transform snailHead;

    [SerializeField] GameObject headAnimations;

    private void Start()
    {
        headAnimations.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Strawberry"))
        {
            if(collision.contacts.Length > 0 && collision.contacts[0].otherCollider.transform.
                IsChildOf(snailHead))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
