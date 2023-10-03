using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SnailHead : MonoBehaviour
{
    [SerializeField] float startingEnergy = 100;
    public float currentEnergy;

    public EnergyBar energyBar;

    bool ifHiding;

    public GameObject cursorController;
    public float energyLossPerSecond = 1;

    float shieldCounterMaxTime = 2;
    public float shieldCounter = 0;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    Color originalColor;

    public ScoreManager scoreManager;

    public void Awake()
    {
        currentEnergy = startingEnergy;
        energyBar.SetStartingEnergy(startingEnergy);
        spriteRend = GetComponent<SpriteRenderer>();
        originalColor = spriteRend.color;
    }

    public void Update()
    {
        ifHiding = cursorController.GetComponent<CursorController>().hideHead;

        if (ifHiding)
        {
            if(shieldCounterMaxTime > shieldCounter)
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
        if (collision.gameObject.CompareTag("Rock"))
        {
            TakeDamage(20);
            Debug.Log("Rock hits!");
            //ScoreManager.scoreValue -= 5;
            scoreManager.scoreCount -= 5;
            StartCoroutine(Invulnerability());
        }

        if(collision.gameObject.CompareTag("Strawberry"))
        {
            AddHealth(5);
            //ScoreManager.scoreValue += 5;
            scoreManager.scoreCount += 5;
            Debug.Log("Strawberry hits!");
        }
    }

    public IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = originalColor;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}
