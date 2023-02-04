using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public bool isOptions;
    public bool isIntro;
    public Animator anim;
    public GameObject introUI;
    public GameObject optionsMenuUI;
    public GameObject optionsButton;
    public GameObject startText;
    public GameObject quitButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Intro();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isIntro == false)
        {
            if(isOptions)
            {
                MainMenu();
            }
            else
            {
                Options();
            }
        }
    }

    public void Intro()
    {
        introUI.SetActive(true);
        isIntro = true;
        if (isIntro)
        {
            optionsButton.SetActive(false);
            quitButton.SetActive(false);
        }
    }
    public void Options()
    {
        optionsMenuUI.SetActive(true);
        isOptions = true;
        optionsButton.SetActive(false);
        startText.SetActive(false);
    }

    public void MainMenu()
    {
        optionsMenuUI.SetActive(false);
        isOptions = false;
        optionsButton.SetActive(true);
        startText.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}