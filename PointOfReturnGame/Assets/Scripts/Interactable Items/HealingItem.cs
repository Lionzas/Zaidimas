using TMPro;
using UnityEngine;

public class HealingItem : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private int restoreAmount;

    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        interactor.GetComponent<PlayerProfile>().RestoreHealth(restoreAmount);
        return true;
    }
}
