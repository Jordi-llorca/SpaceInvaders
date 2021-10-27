using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float score = 0;
    public Text scoreText;

    void Start()
    {
        score = PlayerPrefs.GetInt("Score");
        scoreText.text = score.ToString();
    }

}
