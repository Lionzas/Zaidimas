using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExample : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private AudioSource doorSound;


    public string InteractionPrompt => prompt;

    public int sceneBuildIndex;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    Animator anim;


    public bool Interact(Interactor interactor)
    {

        PlayDoorSound();
        Debug.Log("Opening door");
        if (anim = GetComponent<Animator>())
        {
            anim.SetTrigger("Opened");
        }
        LoadScene();
        return true;
        /*
                // Key check example
                var inventory = interactor.GetComponent<Inventory>();
                if (inventory == null)
                    return false;
                if (inventory.HasKey())
                    Debug.Log("Opening door");
                else
                    Debug.Log("Missing key");
                return true;
        */
    }

    public void LoadScene()
    {
        playerStorage.initialValue = playerPosition;
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
    private void PlayDoorSound()
    {
        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource tempSource = tempAudio.AddComponent<AudioSource>();

        tempSource.clip = doorSound.clip;
        tempSource.outputAudioMixerGroup = doorSound.outputAudioMixerGroup;
        tempSource.volume = doorSound.volume;
        tempSource.pitch = doorSound.pitch;
        tempSource.spatialBlend = doorSound.spatialBlend; // Keep 3D effects if needed
        tempSource.Play();

        DontDestroyOnLoad(tempAudio);
        Destroy(tempAudio, tempSource.clip.length); // Destroy after audio finishes
    }
}
