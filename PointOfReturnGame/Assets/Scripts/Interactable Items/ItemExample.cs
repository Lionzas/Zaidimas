using UnityEngine;

public class ItemExample : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        Debug.Log("Picking up item");
        Destroy(gameObject);
        return true;
    }
}
