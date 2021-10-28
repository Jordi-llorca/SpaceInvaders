using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pausemenu;
    public void pause()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }
    public void Resume()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }
    private void Start()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }
}
