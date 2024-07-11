using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class EndingScript : MonoBehaviour
{
    public Etana etana;
    bool dead;
    [SerializeField] Image endingBgImg;
    [SerializeField] ButtonB playAgainButton;
    [SerializeField] ButtonB backToMainMenuButton;
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject gameWonText;
    [SerializeField] CursorController cursor;
    public void GameOverScreen()
    {
        StartCoroutine(EndingScreen(false));
    }
    public void GameWonScreen()
    {
        StartCoroutine(EndingScreen(true));
    }
    IEnumerator EndingScreen(bool gameWon)
    {
        cursor.gameover = true;
        yield return new WaitForSeconds(2);
        endingBgImg.enabled = true;
        if (gameWon)
            gameWonText.SetActive(true);
        else
            gameOverText.SetActive(true);
        playAgainButton.gameObject.SetActive(true);
        backToMainMenuButton.gameObject.SetActive(true);
        InputManager.Instance.isEndingMenuOpen = true;
    }
}