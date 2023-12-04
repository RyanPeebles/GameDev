using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject deathMenu;
    [SerializeField] GameObject gameMusic;
    [SerializeField] GameObject settingsMenu;

    public static bool GameIsPaused = false;
    public bool mainMenu = false;


    private void Awake()
    {

        if (!mainMenu)
        {
            settingsMenu.SetActive(false);
        }
        else settingsMenu.SetActive(true);

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Time.timeScale = 0f;
                Resume();
            }
            else
            {
                Pause();
            }
        }


    }

    public void Tutorial()
    {
        SceneManager.LoadScene(2);
    }

    public void Death()
    {
        gameMusic.SetActive(false);
        deathMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
    }

    public void Apply()
    {
        settingsMenu.SetActive(false);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

    public void Restart(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
        Time.timeScale = 1f;
    }
    public void Quit_Game()
    {
        Application.Quit();
    }
}
