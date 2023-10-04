using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Etana : MonoBehaviour
{
    Rigidbody2D rb2d;
    CircleCollider2D circleCollider;
    Animator anim;

    public ScoreManager scoreManager;

    [SerializeField] float startingEnergy = 100;
    public float currentEnergy;
    public EnergyBar energyBar;
    bool ifHiding; // fetch from cursorController -script
    public GameObject cursorController;
    public float energyLossPerSecond = 1;
    float shieldCounterMaxTime = 2;
    public float shieldCounter = 0;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer sprite;
    Color originalColor;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();

        currentEnergy = startingEnergy;
        energyBar.SetStartingEnergy(startingEnergy);
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }

    private void Update()
    {
        ifHiding = cursorController.GetComponent<CursorController>().hideHead;

        if (ifHiding)
        {
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
            shieldCounter = 0;
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
                TakeDamage(20);
                Debug.Log("Rock hits!");
                //ScoreManager.scoreValue -= 5;
                scoreManager.scoreCount -= 5;
                StartCoroutine(Invulnerability());
            }

            if (collision.gameObject.CompareTag("Strawberry"))
            {
                AddHealth(5);
                //ScoreManager.scoreValue += 5;
                scoreManager.scoreCount += 5;
                Debug.Log("Strawberry hits!");
            }
        }
    }

    public IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            sprite.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            sprite.color = originalColor;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}
