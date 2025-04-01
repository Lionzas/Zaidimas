using System.Collections;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PopUpBox : MonoBehaviour
{
    public GameObject popUpPanel;
    public TMP_Text popUpText;
    public string[] text;
    private int index;


    public float wordSpeed;
    public bool playerIsClose;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if(popUpPanel.activeInHierarchy)
            {
                ZeroText();
            }
            else
            {
                popUpPanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) && popUpPanel.activeInHierarchy)
        {
            ZeroText();
        }
    }


    public void ZeroText()
    {
        popUpText.text = "";
        index = 0;
        popUpPanel.SetActive(false);
    }


    IEnumerator Typing()
    {
        foreach(char letter in text[index].ToCharArray())
        {
            popUpText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerIsClose = false;
            ZeroText();
        }
    }
}
