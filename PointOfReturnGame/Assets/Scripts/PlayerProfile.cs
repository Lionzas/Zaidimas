using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerProfile : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] DisplayHealth healthbar;
    //private SpriteRenderer renderer;
    public PlayerHealth playerHealth;
    public Transform respawnPoint;


    void Start()
    {
        /*currentHealth = maxHealth;
        playerHealth.initialHealth = currentHealth;*/
        if(playerHealth.initialHealth < maxHealth && playerHealth.initialHealth > 0)
        {
            currentHealth = (int)playerHealth.initialHealth;
        }
        else
        {
            currentHealth = maxHealth;
            playerHealth.initialHealth = currentHealth;
        }
        healthbar.SetHealth(currentHealth, maxHealth);
        //renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            ReduceHealth(15);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            RestoreHealth(15);
        }
        if(currentHealth <= 0)
        {
            RespawnPoint();
            //ReloadScene();
        }
    }

    public void ReduceHealth(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        playerHealth.initialHealth = currentHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
        StartCoroutine("ChangeColor");
    }

    IEnumerator ChangeColor()
    {
        GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);
    }

    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        playerHealth.initialHealth = currentHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
    }


    public void RespawnPoint()
    {
        gameObject.transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        playerHealth.initialHealth = maxHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
    }


    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
