using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EndingScript : MonoBehaviour
{
    public Etana etana;
    bool dead;
    [SerializeField] Image endingBgImg;
    [SerializeField] ButtonB playAgainButton;
    [SerializeField] ButtonB backToMainMenuButton;
    [SerializeField] GameObject endingText;
    private void Update()
    {
        dead = etana.GetComponent<Etana>().dead;
        if(dead)
            StartCoroutine(EndingScreen());
    }
    IEnumerator EndingScreen()
    {
        yield return new WaitForSeconds(2);
        endingBgImg.enabled = true;
        endingText.SetActive(true);
        playAgainButton.gameObject.SetActive(true);
        backToMainMenuButton.gameObject.SetActive(true);
        InputManager.Instance.isEndingMenuOpen = true;
    }
}