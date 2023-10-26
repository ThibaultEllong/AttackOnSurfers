using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ReplayMenu : MonoBehaviour
{

    public GameObject Canvas;

    public GameObject MainMenu;
    public GameObject CreditsMenu;

    // Start is called before the first frame update
    void Start()
    {
        Canvas.SetActive(false);
    }

    public void Display()
    {
        Canvas.SetActive(true);
    }

    public void ReplayNowButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }



    public void MainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menus");
    }

    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }
}

