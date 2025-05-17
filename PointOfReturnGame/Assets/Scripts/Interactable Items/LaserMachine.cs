using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LaserMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private GameObject laserBeam;
    public GameObject popUpPanel;
    public TMP_Text popUpText;
    [SerializeField] private string message;
    private bool activated;

    public string InteractionPrompt => prompt;

    void Start()
    {
        laserBeam.SetActive(false);
        activated = false;
    }

    public bool Interact(Interactor interactor)
    {
        //if (!activated)
        if (!activated && InventoryManager.instance.SelectedHasItemId("device"))
            {
                InventoryManager.instance.RemoveItemByIdInSlot("device");
                PlayAnimations();
                activated = true;
                prompt = "";
                Invoke("End", 5f);
            }
        return true;
    }

    private void PlayAnimations()
    {
        GetComponent<Animator>().Play("LaserStart");
    }

    private void PlayBeamAnimation()
    {
        laserBeam.SetActive(true);
        laserBeam.GetComponent<Animator>().Play("LaserBeamLoop");
    }

    private void End()
    {
        GetComponent<EndGame>().StartGameEnding();
    }
}
