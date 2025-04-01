using UnityEngine;
using TMPro;
using System.Collections;

public class ItemExample : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private AudioSource m_clicksound;

    public string InteractionPrompt => prompt;


    public GameObject popUpPanel;
    public TMP_Text popUpText;
    public string text;


    public bool Interact(Interactor interactor)
    {
        m_clicksound.Play();
        popUpPanel.SetActive(true);
        popUpText.text = text;
        /*if(popUpPanel.activeInHierarchy)
        {
            Invoke("DisablePopUp", 1f);
        }
        Destroy(gameObject);*/
        //StartCoroutine(HidePopUp());
        gameObject.SetActive(false);
        Invoke("DisablePopUp", 1f);
        Invoke("DestroyObject", 1.2f);
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
