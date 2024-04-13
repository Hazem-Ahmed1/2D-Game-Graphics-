using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject Select_Level;
    private bool isPaused = false;
    private bool isBack = false;
    public PlayerInput playerControls;
    private InputAction pause;

    void Awake()
    {
        playerControls = new PlayerInput();
    }

    void OnEnable()
    {
        pause = playerControls.UI.Pause;
        pause.Enable();
    }

    void OnDisable()
    {
        pause.Disable();
    }

    private void Start()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (pause.IsPressed())
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
