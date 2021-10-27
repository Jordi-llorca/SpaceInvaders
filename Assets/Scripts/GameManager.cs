using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    internal static GameManager Instance;

    [SerializeField]
    private int maxLives = 3;

    [SerializeField]
    private Sprite [] livesLabel;

    [SerializeField]
    private GameObject img;

    public int score=0;
    public int lives;
    public Text scoreText;

    internal void UpdateLives()
    {
        lives = Mathf.Clamp(lives - 1, 0, maxLives);
        if (lives == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        img.GetComponent<ChangeImageLives>().UpdateImage(livesLabel[lives - 1]);
        
        
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        lives = maxLives;
        img.GetComponent<ChangeImageLives>().UpdateImage(livesLabel[lives - 1]);
    }
    public void UpdateScore()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = score + "pts";
    }
}
