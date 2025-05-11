using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource m_clicksound;
    [SerializeField] public Animator transition;
    [SerializeField] private GameObject goalPanel;

    private bool goalShown = false;

    public void PlayGame()
    {
        m_clicksound.Play();
        DontDestroyOnLoad(this.m_clicksound);

        if (!goalShown)
        {
            goalPanel.SetActive(true);
            goalShown = true;
            return; // Wait for user to click "Continue"
        }

        StartCoroutine(LoadLevel("GameScreen"));
    }
    public void OnContinueAfterGoal()
    {
        StartCoroutine(LoadLevel("GameScreen"));
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
        StartCoroutine(LoadLevel("MainMenu"));
    }
    public void GoToSettingsMenu()
    {
        m_clicksound.Play();
        DontDestroyOnLoad(this.m_clicksound);
        StartCoroutine (LoadLevel("SettingsMenu"));
    }
    private IEnumerator LoadLevel(string name)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(name);
    }
}
