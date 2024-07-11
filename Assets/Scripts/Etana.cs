using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Etana : MonoBehaviour
{
    Rigidbody2D rb2d;
    CircleCollider2D circleCollider;
    BoxCollider2D boxCollider;
    Animator anim;
    public ScoreManager scoreManager;
    public float startingEnergy = 100;
    public float maxEnergy = 100;
    public float chipSpeed = 2;
    public float currentEnergy;
    [SerializeField] Image frontEnergybar;
    [SerializeField] Image backEnergybar;
    float fillF;
    float fillB;
    float hFraction;
    public bool ifHiding; // fetch from cursorController -script
    public GameObject cursorController;
    bool animationPlaysFetch; // fetch from CursorController -script
    public float energyLossPerSecond = 1;
    float shieldCounterMaxTime = 1;
    public float shieldCounter = 0;
    [SerializeField] GameObject rockHitsShell;
    SpriteRenderer rockHitsShellSprite;
    Animator rockHitsShellAnim;
    [SerializeField] Transform snailHead;
    CircleCollider2D snailHeadCollider;
    public bool dead = false;
    [SerializeField] AudioClip eatsStrawberry;
    [SerializeField] AudioClip rockHitsEtskuSound;
    [SerializeField] AudioClip rockHitsShellSound;
    [SerializeField] AudioClip draggingAroundTheGround;
    [SerializeField] EndingScript endingScript;
    [SerializeField] SpawnManager spawnManager;
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        startingEnergy = maxEnergy;
        currentEnergy = startingEnergy;
        rockHitsShellSprite = rockHitsShell.GetComponent<SpriteRenderer>();
        rockHitsShellAnim = rockHitsShell.GetComponent<Animator>();
        snailHeadCollider = snailHead.GetComponent<CircleCollider2D>();
        snailHeadCollider.enabled = true;
        endingScript.GetComponent<EndingScript>();
    }
    private void Update()
    {
        ifHiding = cursorController.GetComponent<CursorController>().hideHead;
        animationPlaysFetch = cursorController.GetComponent<CursorController>().animationPlaying;

        if(!dead)
        {
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
                    EnergybarLogic();
                }
            }
            else
            {
                snailHeadCollider.enabled = true;
                //boxCollider.enabled = true;
                shieldCounter = 0;
                //SoundManager.instance.PlaySound(draggingAroundTheGround);
                if (currentEnergy > 74)
                    anim.SetBool("Taso1", true);
                else
                    anim.SetBool("Taso1", false);
                if (currentEnergy < 75 && currentEnergy > 49)
                    anim.SetBool("Taso2", true);
                else
                    anim.SetBool("Taso2", false);
                if (currentEnergy < 50 && currentEnergy > 24)
                    anim.SetBool("Taso3", true);
                else
                    anim.SetBool("Taso3", false);
                if (currentEnergy < 25)
                    anim.SetBool("Taso4", true);
                else
                    anim.SetBool("Taso4", false);
            }
        }
        else
        {
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            anim.SetBool("Dead", true);
        }

        if (currentEnergy <= 0)
        {
            dead = true;
            endingScript.GameOverScreen();
            spawnManager.GameOverOrWon();
        }
    }

    public void EnergybarLogic()
    {
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        fillF = frontEnergybar.fillAmount;
        fillB = backEnergybar.fillAmount;
        hFraction = currentEnergy / maxEnergy;

        if(fillB > hFraction)
            frontEnergybar.fillAmount = hFraction;
    }
    public void TakeDamage(float damage)
    {
        currentEnergy -= damage;
        EnergybarLogic();
        if (currentEnergy <= 0)
            anim.SetTrigger("Die");
    }

    public void AddHealth(float health)
    {
        currentEnergy += health;

        EnergybarLogic();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!dead)
        {
            if (!ifHiding)
            {
                if (collision.gameObject.CompareTag("Rock"))
                {
                    if (!animationPlaysFetch)
                    {
                        anim.SetTrigger("Kivi");
                        SoundManager.Instance.PlaySound(rockHitsEtskuSound);
                    }
                    TakeDamage(20);
                    scoreManager.UpdateScore(-5);
                    StartCoroutine(Invulnerability());
                }

                if (collision.gameObject.CompareTag("Strawberry"))
                {
                    if (collision.contacts.Length > 0 && collision.contacts[0].otherCollider.transform.
                        IsChildOf(snailHead))
                    {
                        if (!animationPlaysFetch)
                        {
                            anim.SetTrigger("Mansikka");
                            SoundManager.Instance.PlaySound(eatsStrawberry);
                        }
                        AddHealth(5);
                        scoreManager.UpdateScore(5);
                        Destroy(collision.gameObject);
                    }
                    else
                    {
                        SoundManager.Instance.PlaySound(rockHitsShellSound);
                    }
                }
            }
            else
            {
                if (collision.gameObject.CompareTag("Rock"))
                {
                    rockHitsShellSprite.enabled = true;
                    rockHitsShellAnim.SetTrigger("RockHitsShell");
                    SoundManager.Instance.PlaySound(rockHitsShellSound);
                    StartCoroutine(HideRockHitsShellSprite());
                }
                if(collision.gameObject.CompareTag("Strawberry"))
                {
                    SoundManager.Instance.PlaySound(rockHitsShellSound);
                }
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
