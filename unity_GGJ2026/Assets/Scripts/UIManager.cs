using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject gameOverUI;

    private void Start()
    {
        instance = this;
    }
    public void QuitGame()
   {
       Application.Quit();
    }
    public void OpenGameOver()
    {
        DeliveriesManager.instance.stopAll();
        FindAnyObjectByType<FirstPersonController>().enabled = false;
        FadingManager.instance.Fade(0.8f, 0.5f, () =>
        {
            gameOverUI.SetActive(true);
        });
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
