using TMPro;
using UnityEngine;

public class TimeMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private AudioSource m_clicksound;
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject clock;
    public GameObject popUpPanel;
    public TMP_Text popUpText;
    [SerializeField] private string textFailed;
    [SerializeField] private string textPipe;
    [SerializeField] private string textCog;
    [SerializeField] private string textCanister;
    [SerializeField] private string promptCompleted;
    public int portalState;
    public bool pipes;
    public bool fuel;
    [SerializeField] private MachineManager machineManager;
    
    public string InteractionPrompt => prompt;


    void Start()
    {
        portalState = 0;
        pipes = false;
        machineManager.LoadData();
        PlayAnimations();
        if (portalState >= 3)
        {
            prompt = promptCompleted;
        }
    }

    public bool Interact(Interactor interactor)
    {
        string text = "";
        bool IsSuccessful = false;
        if (portalState >= 3)
        {
            // put travelling to past here
        }
        else
        {
            if (InventoryManager.instance.SelectedHasItemId("pipe"))
            {
                InventoryManager.instance.RemoveItemByIdInSlot("pipe");
                pipes = true;
                IsSuccessful = Repair();
                text = textPipe;
            }
            if (InventoryManager.instance.SelectedHasItemId("cog"))
            {
                InventoryManager.instance.RemoveItemByIdInSlot("cog");
                IsSuccessful = Repair();
                text = textCog;
            }
            if (InventoryManager.instance.SelectedHasItemId("fuel_canister"))
            {
                InventoryManager.instance.RemoveItemByIdInSlot("fuel_canister");
                fuel = true;
                IsSuccessful = Repair();
                text = textCanister;
            }
            if (portalState >= 3)
                prompt = promptCompleted;
            
            if (popUpPanel != null)
            {
                popUpPanel.SetActive(true);
                if (IsSuccessful)
                    popUpText.text = text;
                else
                    popUpText.text = textFailed;
                Invoke("DisablePopUp", 2f);
            }
        }
        
        //for easier testing
        machineManager.SaveData();
        PlayAnimations();

        return true;
    }

    private bool Repair()
    {
        portalState++;
        machineManager.SaveData();
        PlayAnimations();
        return true;
    }

    private void PlayAnimations()
    {
        if (pipes && fuel)
            GetComponent<Animator>().Play("MachinePipes");
        else if (pipes && !fuel)
            GetComponent<Animator>().Play("MachinePipesStatic");
        else if (!pipes)
            GetComponent<Animator>().Play("MachineStatic");

        if (pipes)
        {
            clock.SetActive(true);
            clock.GetComponent<Animator>().Play("Clock");
        }
        else
            clock.SetActive(false);

        switch (portalState)
        {
            case 0:
                portal.SetActive(false);
                break;
            case 1:
                portal.SetActive(true);
                portal.GetComponent<Animator>().Play("First");
                break;
            case 2:
                portal.SetActive(true);
                portal.GetComponent<Animator>().Play("Second");
                break;
            case 3:
                portal.SetActive(true);
                portal.GetComponent<Animator>().Play("Third");
                break;
            default:
                portal.SetActive(true);
                portal.GetComponent<Animator>().Play("Third");
                break;
        }
    }

    public void DisablePopUp()
    {
        popUpPanel.SetActive(false);
    }
}
