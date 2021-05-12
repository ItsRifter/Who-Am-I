using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject helpPnl;
    public GameObject mainPnl;
    public GameObject optionPnl;

    //Starts a new game to the first level
    public void BeginGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Level_01");
    }

    public void BackButtonHelp()
    {
        helpPnl.SetActive(false);
        mainPnl.SetActive(true);
    }

    public void HelpMenu()
    {
        helpPnl.SetActive(true);
        mainPnl.SetActive(false);
    }

    public void OptionsMenu()
    {
        optionPnl.SetActive(true);
        mainPnl.SetActive(false);
    }

    public void BackButtonOptions()
    {
        optionPnl.SetActive(false);
        mainPnl.SetActive(true);
    }

    //Quits the game
    public void QuitGame()
    {
        Debug.Log("Player has quit the game");
        Application.Quit();
    }
}
