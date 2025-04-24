using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private AudioSource doorSound;
    public string InteractionPrompt => prompt;

    Animator anim;
    Collider2D coll;


    public bool Interact(Interactor interactor)
    {
        PlayDoorSound();
        Debug.Log("Opening door");
        if (anim = GetComponent<Animator>())
        {
            coll = GetComponent<Collider2D>();
            anim.SetTrigger("Opened"); 
            coll.isTrigger = !coll.isTrigger;
        }
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
