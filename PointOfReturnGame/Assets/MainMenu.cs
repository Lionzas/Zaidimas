using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource m_clicksound;
    public void PlayGame()
    {
        m_clicksound.Play();
        DontDestroyOnLoad(this.m_clicksound);
        SceneManager.LoadScene("GameScreen");
        
    }
    public void QuitGame()
    {
        m_clicksound.Play();
        DontDestroyOnLoad(this.m_clicksound);
        Application.Quit();
        Debug.Log("Game is exiting");
    }
    public void GoToMainMenu()
    {
        m_clicksound.Play();
        DontDestroyOnLoad(this.m_clicksound);
        SceneManager.LoadScene("MainMenu");
    }
    public void GoToSettingsMenu()
    {
        m_clicksound.Play();
        DontDestroyOnLoad(this.m_clicksound);
        SceneManager.LoadScene("SettingsMenu");
    }
}
