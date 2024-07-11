using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public int scoreCount;
    public TextMeshProUGUI scoreText;
    [SerializeField] EndingScript endingScript;
    [SerializeField] SpawnManager spawnManager;
    public void UpdateScore(int newScore)
    {
        scoreCount += newScore;
        scoreText.text = scoreCount.ToString();
        if(scoreCount >= 300)
        {
            endingScript.GameWonScreen();
            spawnManager.GameOverOrWon();
        }
    }
}