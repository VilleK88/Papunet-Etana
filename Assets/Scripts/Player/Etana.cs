using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Etana : MonoBehaviour
{
    Rigidbody2D rb2d;
    CircleCollider2D circleCollider;
    BoxCollider2D boxCollider;
    Animator anim;
    public ScoreManager scoreManager;
    public float maxEnergy = 100;
    public float currentEnergy;
    public float chipSpeed = 2;
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
    public bool won = false;
    [SerializeField] AudioClip eatsStrawberry;
    [SerializeField] AudioClip rockHitsEtskuSound;
    [SerializeField] AudioClip rockHitsShellSound;
    [SerializeField] AudioClip draggingAroundTheGround;
    [SerializeField] EndingScript endingScript;
    [SerializeField] SpawnManager spawnManager;
    bool dieOnlyOnce;
    private void Start()
    {
        currentEnergy = maxEnergy;
        rb2d = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        currentEnergy = maxEnergy;
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
        if(!dead && !won)
        {
            if (ifHiding)
            {
                snailHeadCollider.enabled = false;
                if (shieldCounterMaxTime > shieldCounter)
                    shieldCounter += Time.deltaTime;
                else
                {
                    currentEnergy -= energyLossPerSecond * Time.deltaTime;
                    EnergybarLogic();
                }
            }
            else
            {
                snailHeadCollider.enabled = true;
                shieldCounter = 0;
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
            if (currentEnergy <= 0 && !dieOnlyOnce)
            {
                dieOnlyOnce = true;
                Die();
            }
        }
    }
    public void EnergybarLogic()
    {
        if (currentEnergy < 0)
            currentEnergy = 0;
        fillF = currentEnergy / maxEnergy;
        frontEnergybar.fillAmount = fillF;
    }
    public void AddHealth(float health)
    {
        currentEnergy += health;
        if (currentEnergy > maxEnergy)
            currentEnergy = maxEnergy;
        EnergybarLogic();
    }
    public void TakeDamage(float damage)
    {
        if(currentEnergy > 0)
        {
            currentEnergy -= damage;
            EnergybarLogic();
            if (currentEnergy <= 0)
                Die();
        }
    }
    void Die()
    {
        dead = true;
        anim.SetTrigger("Die");
        endingScript.GameOverScreen();
        spawnManager.GameOverOrWon();
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        anim.SetBool("Dead", true);
    }
    public void GameWon()
    {
        won = true;
        StartCoroutine(StopAnimations(2f));
    }
    IEnumerator StopAnimations(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("Taso1", false);
        anim.SetBool("Taso2", false);
        anim.SetBool("Taso3", false);
        anim.SetBool("Taso4", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!dead)
        {
            if (!ifHiding)
            {
                if (collision.gameObject.CompareTag("Rock"))
                {
                    if (collision.contacts.Length > 0 && collision.contacts[0].otherCollider.transform.
                        IsChildOf(snailHead))
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
                        SoundManager.Instance.PlaySound(rockHitsShellSound);
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
                    SoundManager.Instance.PlaySound(rockHitsShellSound);
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