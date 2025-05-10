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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        laserBeam.SetActive(false);
        activated = false;
    }

    public bool Interact(Interactor interactor)
    {
        //if (InventoryManager.instance.SelectedHasItemId("device"))
        //{
        //    PlayAnimations();
        //}
        if (!activated)
        {
            PlayAnimations();
            activated = true;
            prompt = "";
        }
        return true;
    }

    private void PlayAnimations()
    {
        GetComponent<Animator>().Play("LaserStart");
        Invoke("PlayBeamAnimation", 1);
        //laserBeam.SetActive(true);
        //laserBeam.GetComponent<Animator>().Play("LaserBeamEmpty");
    }

    private void PlayBeamAnimation()
    {
        laserBeam.SetActive(true);
        laserBeam.GetComponent<Animator>().Play("LaserBeamLoop");
    }
}
