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
        StartCoroutine(LoadLevel("Level_1"));
    }
    public void MainMenuButton()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(LoadLevel("MainMenu"));
    }

    IEnumerator LoadLevel(string levelName)
    {
        transition.SetTrigger("start");
        
        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(levelName);
    }
}
