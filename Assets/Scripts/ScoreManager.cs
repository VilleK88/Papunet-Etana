using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int scoreCount;
    public TextMeshProUGUI scoreText;

    public void Start()
    {

    }

    public void Update()
    {
        scoreText.text = scoreCount.ToString();
    }
}
