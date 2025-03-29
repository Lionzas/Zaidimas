using UnityEngine;

public class ItemExample : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private AudioSource m_clicksound;

    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        m_clicksound.Play();
        Debug.Log("Picking up item");
        Destroy(gameObject);
        return true;
    }
}
