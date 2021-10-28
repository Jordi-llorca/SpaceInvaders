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
    private AudioSource sfx;

    internal void PlaySfx(AudioClip clip) => sfx.PlayOneShot(clip);

    [SerializeField]
    private int maxLives = 3;

    [SerializeField]
    private Sprite [] livesLabel;

    [SerializeField]
    private GameObject img;

    public int score=0;
    public int lives = 3;
    public Text scoreText;
    

    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private float explosionTime = 1f;

    [SerializeField]
    private AudioClip explosionClip;

    public int round = 0;

    internal void CreateExplosion(Vector2 position)
    {
        PlaySfx(explosionClip);

        var explosion = Instantiate(explosionPrefab, position,
            Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f)));
        Destroy(explosion, explosionTime);
    }

    internal void UpdateLives()
    {
        lives = Mathf.Clamp(lives - 1, 0, maxLives);
        if (lives == 0)
        {
            LevelLoader.Instance.LoadNextLevel();
        }

        img.GetComponent<ChangeImageLives>().UpdateImage(livesLabel[lives - 1]);
    }

    public void Setup()
    {
        score = 0;
        lives = maxLives;
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
        

        lives = PlayerPrefs.GetInt("Lives");
        score = PlayerPrefs.GetInt("Score");

        img.GetComponent<ChangeImageLives>().UpdateImage(livesLabel[lives - 1]);

        UpdateScore(0);
    }

    public void UpdateScore(int add)
    {
        score += add;
        scoreText.text = score.ToString();
    }
}
