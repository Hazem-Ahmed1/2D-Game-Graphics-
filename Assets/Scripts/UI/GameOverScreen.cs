using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    // [SerializeField]
    // private Text pointsText;
    // public void Setup(int score)
    // {
    //     gameObject.SetActive(true);
    //     pointsText.text = score.ToString() + "Score";
    // }
    public void RestartButton()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
