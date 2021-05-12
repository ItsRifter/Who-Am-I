using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pausePanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseGame();
        } else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            ResumeGame();
        }
    }

    //Pause the game and enable the pause menu
    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        isPaused = true;
    }

    //Resumes the game and disable the pause menu
    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        isPaused = false;
    }

    //Returns to the main menu
    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    //Quits the game
    public void Quit()
    {
        Debug.Log("Player has quit the game");
        Application.Quit();
    }
}
