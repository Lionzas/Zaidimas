using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenUI : MonoBehaviour
{
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScreen");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        AudioListener.volume = 1f;
    }
    void OnEnable()
    {

        Time.timeScale = 0f;
        AudioListener.volume = 0f;
    }
}