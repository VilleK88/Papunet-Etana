using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingScript : MonoBehaviour
{
    public Etana etana;
    bool dead;

    [SerializeField] GameObject whiteScreen;
    [SerializeField] Button playAgainButton;

    private void Update()
    {
        dead = etana.GetComponent<Etana>().dead;

        if(dead)
        {
            StartCoroutine(EndingScreen());
        }
    }

    IEnumerator EndingScreen()
    {
        yield return new WaitForSeconds(2);
        whiteScreen.SetActive(true);
        playAgainButton.gameObject.SetActive(true);

    }

    public void LoadGameAgain()
    {
        SceneManager.LoadScene("Game");
    }
}
