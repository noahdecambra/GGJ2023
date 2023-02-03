using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public bool isOptions;
    public GameObject optionsMenuUI;

    void Update()
    {
        if (Input.anyKeyDown && Input.GetKeyDown(KeyCode.Escape) == false && Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            SceneManager.LoadScene("Levels");
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
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

    public void Options()
    {
        optionsMenuUI.SetActive(true);
        isOptions = true;
    }

    public void MainMenu()
    {
        optionsMenuUI.SetActive(false);
        isOptions = false;
    }

    public void Levels()
    {
        SceneManager.LoadScene("Levels");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}