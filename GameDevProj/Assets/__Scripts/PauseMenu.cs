using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject deathMenu;
    [SerializeField] GameObject gameMusic;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject levelEnd;
    [SerializeField] playerControl player;

    public static bool GameIsPaused = false;
    public float setVolume;
    public TMP_Dropdown resolution;
    public TMP_Dropdown screenType;
    public TMP_Text health;
    public string dropdownName;
    protected int width;
    protected int height;
    public Timer time;


    public Slider sliderVal1;


    private void Awake()
    {
        Time.timeScale = 1;

        /* if (!mainMenu)
         {
             settingsMenu.SetActive(false);
         }
         else settingsMenu.SetActive(true);
        */
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

        if (levelEnd.GetComponent<LevelChange>().passPortal && levelEnd.GetComponent<LevelChange>().gameEnder)
        {
            Time.timeScale = 0f;
            deathMenu.SetActive(true);

        }

        health.text = "HP: " + player.health;
        if (time.totalTime <= 0)
        {
            Death();
        }
        if (player.health <= 0)
        {
            Death();
        }
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(2);
    }

    public void Death()
    {
        //gameMusic.SetActive(false);
        PlayerPrefs.DeleteAll();
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

    public void ChangeVol()
    {
        setVolume = sliderVal1.value;
        setVolume /= 100;
        AudioListener.volume = setVolume;
    }

    public void ResolutionOptions()
    {
        if (resolution.value == 0)
        {

        }
        else
        {
            //see = int.Parse(resolution.captionText.text);
            dropdownName = resolution.captionText.text;
            string[] splitTitle = dropdownName.Split(new string[] { "x" }, System.StringSplitOptions.None);
            int.TryParse(splitTitle[0], out width);
            int.TryParse(splitTitle[1], out height);
            Screen.SetResolution(width, height, true);
            //test = resolution.captionText;
        }
    }

    public void ScreenType()
    {
        if (screenType.value == 0)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else if (screenType.value == 1)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else if (screenType.value == 2)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        else if (screenType.value == 3)
        {
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
        }
        else
        {
            Debug.Log("Not a valid screen type");
        }
    }
}
