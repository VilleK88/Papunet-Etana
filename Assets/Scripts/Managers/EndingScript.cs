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
    [SerializeField] MoveLeft moveLeftBG;
    [SerializeField] MoveLeft moveLeftGroundEtanaGoesFront;
    [SerializeField] MoveLeft moveLeftGroundEtanaGoesBack;
    [SerializeField] GameObject transparentBG;
    public void GameOverScreen()
    {
        StartCoroutine(EndingScreen(false));
        Debug.Log("GameOver");
    }
    public void GameWonScreen()
    {
        StartCoroutine(EndingScreen(true));
        Debug.Log("GameWon");
    }
    IEnumerator EndingScreen(bool gameWon)
    {
        cursor.gameover = true;
        yield return new WaitForSeconds(2f);
        DestroyRocksAndStrawberries();
        moveLeftBG.stopMovingBG = true;
        moveLeftGroundEtanaGoesFront.stopMovingBG = true;
        moveLeftGroundEtanaGoesBack.stopMovingBG = true;
        endingBgImg.enabled = true;
        if (gameWon)
            gameWonText.SetActive(true);
        else
            gameOverText.SetActive(true);
        playAgainButton.gameObject.SetActive(true);
        backToMainMenuButton.gameObject.SetActive(true);
        InputManager.Instance.SelectFirstButton();
        InputManager.Instance.isEndingMenuOpen = true;
        transparentBG.SetActive(true);
    }
    void DestroyRocksAndStrawberries()
    {
        GameObject[] rocks = GameObject.FindGameObjectsWithTag("Rock");
        for (int i = 0; i < rocks.Length; i++)
            Destroy(rocks[i]);
        GameObject[] strawberries = GameObject.FindGameObjectsWithTag("Strawberry");
        for (int i = 0; i < strawberries.Length; i++)
            Destroy(strawberries[i]);
    }
}