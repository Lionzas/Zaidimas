using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu1 : MonoBehaviour
{
    public static PauseMenu1 instance;

    public GameObject PausePanel;
    private bool isPaused;

    [SerializeField] private AudioSource m_clicksound;

    void Awake()
    {
        // Singleton pattern to persist across scenes
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    void OnEnable()
    {
        // Listen for scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        isPaused = false;
        if (PausePanel != null)
            PausePanel.SetActive(false);
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
        if (PausePanel != null)
            PausePanel.SetActive(true);

        Time.timeScale = 0;
        isPaused = true;
    }

    public void Unpause()
    {
        if (m_clicksound != null)
            m_clicksound.Play();

        if (PausePanel != null)
            PausePanel.SetActive(false);

        Time.timeScale = 1;
        isPaused = false;
    }

    public void Exit()
    {
        if (m_clicksound != null)
            m_clicksound.Play();

        SceneManager.LoadScene("MainMenu");
        // Don't need to unpause here — OnSceneLoaded will handle it
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ensure pause menu is hidden after any scene load
        if (PausePanel != null)
            PausePanel.SetActive(false);

        Time.timeScale = 1;
        isPaused = false;

        // Optional: destroy in main menu
        if (scene.name == "MainMenu")
        {
            Destroy(gameObject);
        }
    }
}
