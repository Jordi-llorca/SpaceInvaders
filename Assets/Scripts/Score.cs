using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float score = 0;
    private float maxScore = 0;
    public Text scoreText;
    public Text MaxscoreText;

    void Start()
    {
        score = PlayerPrefs.GetInt("Score");
        maxScore = PlayerPrefs.GetInt("MaxScore");
        if(maxScore < score)
        {
            maxScore = score;
            PlayerPrefs.SetInt("MaxScore", (int)maxScore);
        }

        scoreText.text = score.ToString();
        MaxscoreText.text = maxScore.ToString();
    }

}
