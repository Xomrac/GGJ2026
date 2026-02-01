using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject gameOverUI;
    public GameObject cheatsheetMini;
    public GameObject cheatsheetMax;

    private void Start()
    {
        instance = this;
    }
    public void QuitGame()
   {
       Application.Quit();
    }
    [Button]
    public void OpenGameOver()
    {
        DeliveriesManager.instance.stopAll();
        FindAnyObjectByType<FirstPersonController>().enabled = false;
        FadingManager.instance.Fade(0.8f, 0.5f, () =>
        {
            gameOverUI.SetActive(true);
        });
    }
    public void MaximaxeOrMinimze()
    {
        if (cheatsheetMax.activeSelf) { 
            cheatsheetMax.SetActive(false);
            cheatsheetMini.SetActive(true);
            FindAnyObjectByType<FirstPersonController>().enabled = true;
        }
        else {          
            cheatsheetMini.SetActive(false);
            cheatsheetMax.SetActive(true);
            FindAnyObjectByType<FirstPersonController>().enabled = false;
        }
    }
    public void Restart()
    {
        FadingManager.instance.Fade(1, 1f, () =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }
    public void OpenEndGameUI()
    {

    }
    public void gameOver()
    {
            OpenGameOver();
        
    }
}
