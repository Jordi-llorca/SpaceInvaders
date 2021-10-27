using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private AudioSource sfx;
    internal void PlaySfx(AudioClip clip) => sfx.PlayOneShot(clip);
    [SerializeField]
    private AudioClip introClip;


    internal static LevelLoader Instance;
    public Animator transition;
    public float transitionTime = 2;
    public void LoadNextLevel()
    {
        SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
        PlayerPrefs.SetInt("Score", GameManager.Instance.score);
        PlayerPrefs.SetInt("Lives", GameManager.Instance.lives);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void loadLevel(int index)
    {
        SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
        StartCoroutine(LoadLevel(index));
    }

    public void ReloadLevel()
    {
        PlayerPrefs.SetInt("Score", GameManager.Instance.score);
        PlayerPrefs.SetInt("Lives", GameManager.Instance.lives);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    public void PlayGame()
    {
        PlaySfx(introClip);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Lives", 3);
        loadLevel(1);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
