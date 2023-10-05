using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Etana : MonoBehaviour
{
    Rigidbody2D rb2d;
    CircleCollider2D circleCollider;
    BoxCollider2D boxCollider;
    Animator anim;

    public ScoreManager scoreManager;

    [SerializeField] float startingEnergy = 100;
    public float currentEnergy;
    public EnergyBar energyBar;
    public bool ifHiding; // fetch from cursorController -script
    public GameObject cursorController;
    public float energyLossPerSecond = 1;
    float shieldCounterMaxTime = 1;
    public float shieldCounter = 0;

    [SerializeField] GameObject rockHitsShell;
    SpriteRenderer rockHitsShellSprite;
    Animator rockHitsShellAnim;

    [SerializeField] Transform snailHead;
    CircleCollider2D snailHeadCollider;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        currentEnergy = startingEnergy;
        energyBar.SetStartingEnergy(startingEnergy);

        rockHitsShellSprite = rockHitsShell.GetComponent<SpriteRenderer>();
        rockHitsShellAnim = rockHitsShell.GetComponent<Animator>();

        snailHeadCollider = snailHead.GetComponent<CircleCollider2D>();
        snailHeadCollider.enabled = true;
    }

    private void Update()
    {
        ifHiding = cursorController.GetComponent<CursorController>().hideHead;

        if (ifHiding)
        {
            snailHeadCollider.enabled = false;
            //boxCollider.enabled = false;

            if (shieldCounterMaxTime > shieldCounter)
            {
                shieldCounter += Time.deltaTime;
            }
            else
            {
                currentEnergy -= energyLossPerSecond * Time.deltaTime;
            }
        }
        else
        {
            snailHeadCollider.enabled = true;
            //boxCollider.enabled = true;
            shieldCounter = 0;

            if(currentEnergy > 74)
            {
                anim.SetBool("Taso1", true);
            }
            else
            {
                anim.SetBool("Taso1", false);
            }

            if(currentEnergy < 75 && currentEnergy > 49)
            {
                anim.SetBool("Taso2", true);
            }
            else
            {
                anim.SetBool("Taso2", false);
            }

            if(currentEnergy < 50 && currentEnergy > 24)
            {
                anim.SetBool("Taso3", true);
            }
            else
            {
                anim.SetBool("Taso3", false);
            }

            if(currentEnergy < 25)
            {
                anim.SetBool("Taso4", true);
            }
            else
            {
                anim.SetBool("Taso4", false);
            }
        }

        energyBar.SetEnergy(currentEnergy);
    }

    public void TakeDamage(float _damage)
    {
        currentEnergy = Mathf.Clamp(currentEnergy - _damage, 0, startingEnergy);

        energyBar.SetEnergy(currentEnergy);

        if (currentEnergy > 0)
        {
            // Player hurt.
        }
        else
        {
            // Player dead.
        }
    }

    public void AddHealth(float _health)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + _health, 0, startingEnergy);

        energyBar.SetEnergy(currentEnergy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!ifHiding)
        {
            if (collision.gameObject.CompareTag("Rock"))
            {
                anim.SetTrigger("Kivi");
                TakeDamage(20);
                scoreManager.scoreCount -= 5;
                StartCoroutine(Invulnerability());
            }

            if (collision.gameObject.CompareTag("Strawberry"))
            {
                if(collision.contacts.Length > 0 && collision.contacts[0].otherCollider.transform.
                    IsChildOf(snailHead))
                {
                    anim.SetTrigger("Mansikka");
                    AddHealth(5);
                    scoreManager.scoreCount += 5;
                    Destroy(collision.gameObject);
                }
            }
        }
        else
        {
            if(collision.gameObject.CompareTag("Rock"))
            {
                rockHitsShellSprite.enabled = true;
                rockHitsShellAnim.SetTrigger("RockHitsShell");
                StartCoroutine(HideRockHitsShellSprite());
            }
        }
    }

    public IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(2);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    IEnumerator HideRockHitsShellSprite()
    {
        yield return new WaitForSeconds(0.2f);
        rockHitsShellSprite.enabled = false;
    }
}
