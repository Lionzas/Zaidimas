using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExample : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;

    public int sceneBuildIndex;
    public Vector2 playerPosition;
    public VectorValue playerStorage;


    public bool Interact(Interactor interactor)
    {

        Debug.Log("Opening door");
        LoadScene();
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

    public void LoadScene()
    {
        playerStorage.initialValue = playerPosition;
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}
