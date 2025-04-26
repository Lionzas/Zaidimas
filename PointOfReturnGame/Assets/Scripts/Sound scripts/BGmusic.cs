using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGmusic : MonoBehaviour
{
    public static BGmusic instance;

    public AudioSource audioSource;

    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName;
        public AudioClip musicClip;
    }

    public List<SceneMusic> sceneMusicList; // Assign in Inspector

    private Dictionary<string, AudioClip> sceneMusicDict = new Dictionary<string, AudioClip>();
    private string currentSceneName = "";

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;

            foreach (var sceneMusic in sceneMusicList)
            {
                sceneMusicDict.Add(sceneMusic.sceneName, sceneMusic.musicClip);
            }
        }
    }

    void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName != currentSceneName)
        {
            currentSceneName = sceneName;
            HandleSceneMusic(sceneName);
        }
    }


    void HandleSceneMusic(string sceneName)
    {
        if (sceneMusicDict.TryGetValue(sceneName, out AudioClip clip))
        {
            if (audioSource.clip != clip)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
            // If clip is already the same, don't restart it!
        }
        else
        {
            Debug.LogWarning($"No music assigned for scene {sceneName}!");
            // Optional: continue playing whatever is already playing
        }
    }

}
