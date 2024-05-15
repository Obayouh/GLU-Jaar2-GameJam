using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    Scene currentScene;

    [SerializeField] private GameObject pausePanel;

    public string selectedScene;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Pause();
            }
            else Continue();
        }
    }

    //public void Settings()
    //{
    //    Debug.Log("settings");
    //}

    public void Quit()
    {
        //PlayerPrefs.Save();
        Application.Quit();
        Debug.Log("quit");
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void TitleScreen()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(selectedScene);
    }
}
