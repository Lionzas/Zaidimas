using UnityEngine;
using TMPro;
using System.Collections;

public class ItemExample : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private AudioSource m_clicksound;
    public ItemData itemData;

    public string InteractionPrompt => prompt;


    public GameObject popUpPanel;
    public TMP_Text popUpText;
    public string text;

    private bool wasPickedUp = false;

    private void Start()
    {
        // Check if this item was already picked up
        if (InventoryManager.instance != null &&
            InventoryManager.instance.HasItem(itemData))
        {
            wasPickedUp = true;
            gameObject.SetActive(false);
        }
    }


    public bool Interact(Interactor interactor)
    {
        if (m_clicksound != null)
        {
            AudioSource.PlayClipAtPoint(m_clicksound.clip, transform.position);
        }

        if (popUpPanel != null)
        {
            popUpPanel.SetActive(true);
            popUpText.text = text;
        }

        wasPickedUp = InventoryManager.instance.AddItem(itemData);

        if (wasPickedUp)
        {
            gameObject.SetActive(false);
            Invoke("DisablePopUp", 1f);
            Invoke("DestroyObject", 1.2f);
        }

        return true;
    }


    public void DisablePopUp()
    {
        popUpPanel.SetActive(false);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
