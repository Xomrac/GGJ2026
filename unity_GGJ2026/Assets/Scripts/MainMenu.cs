using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OpenSettingsPanel()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void OpenCreditsPanel()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
    public void CloseCreditsPanel()
    {
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

        public void PlayMenuPlaySFX()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.menuPlay);
    }

    public void PlayMenuBackSFX()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.menuBack);
    }

    public void PlayButtonSFX()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.menuButton);
    }
}
