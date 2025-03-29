using UnityEngine;

public class DoorExample : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {

        Debug.Log("Opening door");
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
}
