using UnityEngine;
using TMPro;

public class LevelDoorUnlocking : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private AudioSource doorSound;
    public string doorId;
    public string keyId;
    public GameObject popUpPanel;
    public TMP_Text popUpText;

    public string InteractionPrompt => prompt;

    Animator anim;
    Collider2D coll;


    private void Start()
    {
        if (DoorStateManager.instance.IsDoorUnlocked(doorId))
        {
            gameObject.tag = "Unlocked";
        }
        else
        {
            gameObject.tag = "Locked";
        }
    }


    public bool Interact(Interactor interactor)
    {
        var selectedItem = InventoryManager.instance.slots[InventoryManager.instance.selectedSlot].item;

        if (gameObject.tag == "Locked")
        {
            if (selectedItem != null)
            {
                if (selectedItem.itemId == keyId)
                {
                    gameObject.tag = "Unlocked";
                    InventoryManager.instance.RemoveItemByIdInSlot(keyId);
                    ShowPopUp("Door has been unlocked");
                    DoorStateManager.instance.SetDoorState(doorId, true);
                    DoorStateManager.instance.SaveDoors();
                }
                else
                {
                    ShowPopUp("This won't unlock the door");
                }
            }
            else
            {
                ShowPopUp("The door is locked");
            }
        }
        else if (gameObject.tag == "Unlocked")
        {
            PlayDoorSound();
            Debug.Log("Opening door");
            if (anim = GetComponent<Animator>())
            {
                coll = GetComponent<Collider2D>();
                anim.SetTrigger("Opened");
                coll.isTrigger = !coll.isTrigger;
            }
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


    private void ShowPopUp(string message)
    {
        if (popUpPanel != null && popUpText != null)
        {
            popUpPanel.SetActive(true);
            popUpText.text = message;
            Invoke("DisablePopUp", 1f);
        }
    }


    private void DisablePopUp()
    {
        if (popUpPanel != null)
            popUpPanel.SetActive(false);
    }


    private void OnApplicationQuit()
    {
        DoorStateManager.instance.SaveDoors();
    }
}
