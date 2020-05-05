using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Credits;

    public void PlayGame ()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowCredits()
    {
        MainMenu.SetActive(false);
        Credits.SetActive(true);
    }

    public void HideCredits()
    {
        Credits.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
