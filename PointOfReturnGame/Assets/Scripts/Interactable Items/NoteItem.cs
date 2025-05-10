using TMPro;
using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private AudioSource m_clicksound;

    public string InteractionPrompt => prompt;


    public GameObject popUpPanel;
    public TMP_Text popUpText;
    public string text;
    public float textDuration;

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
            Invoke("DisablePopUp", textDuration);
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

