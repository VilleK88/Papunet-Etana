using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public int scoreCount;
    public TextMeshProUGUI scoreText;
    [SerializeField] EndingScript endingScript;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] Etana etana;
    public void UpdateScore(int newScore)
    {
        scoreCount += newScore;
        scoreText.text = scoreCount.ToString();
        if(scoreCount >= 20)
        {
            endingScript.GameWonScreen();
            spawnManager.GameOverOrWon();
            etana.GameWon();
        }
    }
}