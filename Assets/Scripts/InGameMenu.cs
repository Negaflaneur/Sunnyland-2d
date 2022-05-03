using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class InGameMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
     
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Music.musicInstance.music.Play();
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        Music.musicInstance.music.Stop();
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Advertisement.Initialize("3684789", true);

        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
