using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    private bool isPaused;

    [SerializeField] private AudioSource m_clicksound;

    private void Start()
    {
        isPaused = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                Pause();
            else
                Unpause();
        }
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Unpause()
    {
        m_clicksound.Play();
        DontDestroyOnLoad(this.m_clicksound);
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Exit()
    {
        m_clicksound.Play();
        DontDestroyOnLoad(this.m_clicksound);
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        isPaused = false;
    }
}
