using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public void LoadNextLevel()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void MainMenuButton()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(LoadLevel(0));
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(true);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }
    public void SelectLevel(int index)
    {
        this.gameObject.SetActive(true);
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel(index));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("start");
        
        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(levelIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Game Over");
        Application.Quit();
    }
}
