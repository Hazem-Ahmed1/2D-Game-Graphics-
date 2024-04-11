using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject Select_Level;
    private bool isPaused = false;
    private bool isBack = false;

    private void Start()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeLevel();
                Select_Level.SetActive(false);
            }
            else
            {
                PauseLevel();
            }
        }
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeLevel()
    {
        Time.timeScale = 1f;
        isPaused = false;

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }
    public void SelectLevel()
    {
        // Time.timeScale = 0f;
        isBack = !isBack;

        if (pauseMenuUI != null && !isBack)
        {
            pauseMenuUI.SetActive(false);
            Select_Level.SetActive(true);
        }
        else if(pauseMenuUI != null && isBack)
        {
            pauseMenuUI.SetActive(true);
            Select_Level.SetActive(false);
        }
    }


    public void PauseLevel()
    {
        Time.timeScale = 0f;
        isPaused = true;

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
    }
    public void QuitLevel()
    {
        Debug.Log("Game Over");
        Application.Quit();
    }
}
