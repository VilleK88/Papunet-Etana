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

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer sprite;
    Color originalColor;

    [SerializeField] GameObject rockHitsShell;
    SpriteRenderer rockHitsShellSprite;
    Animator rockHitsShellAnim;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        currentEnergy = startingEnergy;
        energyBar.SetStartingEnergy(startingEnergy);
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;

        rockHitsShellSprite = rockHitsShell.GetComponent<SpriteRenderer>();
        rockHitsShellAnim = rockHitsShell.GetComponent<Animator>();
    }

    private void Update()
    {
        ifHiding = cursorController.GetComponent<CursorController>().hideHead;

        if (ifHiding)
        {
            boxCollider.enabled = false;

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
            boxCollider.enabled = true;
            shieldCounter = 0;

            if(currentEnergy > 74)
            {
                StartCoroutine(SetEnergyLevelTo1());
            }
            else
            {
                anim.SetBool("Taso1", false);
            }

            if(currentEnergy < 75 && currentEnergy > 49)
            {
                StartCoroutine(SetEnergyLevelTo2());
            }
            else
            {
                anim.SetBool("Taso2", false);
            }

            if(currentEnergy < 50 && currentEnergy > 24)
            {
                StartCoroutine(SetEnergyLevelTo3());
            }
            else
            {
                anim.SetBool("Taso3", false);
            }

            if(currentEnergy < 25)
            {
                StartCoroutine(SetEnergyLevelTo4());
            }
            else
            {
                anim.SetBool("Taso4", false);
            }
        }

        /*if (!ifHiding && currentEnergy > 74)
        {
            //anim.SetBool("Taso1", true);
            StartCoroutine(SetEnergyLevelTo1());
        }
        else if(ifHiding && currentEnergy > 74)
        {
            anim.SetBool("Taso1", false);
        }*/

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
                Debug.Log("Rock hits!");
                scoreManager.scoreCount -= 5;
                StartCoroutine(Invulnerability());
            }

            if (collision.gameObject.CompareTag("Strawberry"))
            {
                anim.SetTrigger("Mansikka");
                AddHealth(5);
                scoreManager.scoreCount += 5;
                Debug.Log("Strawberry hits!");
                Destroy(collision.gameObject);
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

    IEnumerator SetEnergyLevelTo1()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Taso1", true);
    }

    IEnumerator SetEnergyLevelTo2()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Taso2", true);
    }

    IEnumerator SetEnergyLevelTo3()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Taso3", true);
    }

    IEnumerator SetEnergyLevelTo4()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Taso4", true);
    }

    IEnumerator HideRockHitsShellSprite()
    {
        yield return new WaitForSeconds(0.2f);
        rockHitsShellSprite.enabled = false;
    }
}
