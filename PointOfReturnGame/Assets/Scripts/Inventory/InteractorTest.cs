using UnityEngine;

public class InteractorTest : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string itemId;

    public string InteractionPrompt => prompt;


    public bool Interact(Interactor interactor)
    {
        if (InventoryManager.instance.HasItemId(itemId))
        {
            Debug.Log("player has item");
        }
        else
        {
            Debug.Log("player does not have item");
        }
        return true;
    }
}
