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
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private GameObject deathScreenPanel;


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
        if (currentHealth <= 0)
        {
            ShowDeathScreen();
        }
    }
    public void ShowDeathScreen()
    {
        if (deathScreenPanel != null)
        {
            deathScreenPanel.SetActive(true);
        }
    }

    public void ReduceHealth(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        playerHealth.initialHealth = currentHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
        StartCoroutine("ChangeColorRed");
    }

    IEnumerator ChangeColorRed()
    {
        hitSound.Play();
        DontDestroyOnLoad(this.hitSound);
        GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);
    }

    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        playerHealth.initialHealth = currentHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
        StartCoroutine("ChangeColorGreen");
    }

    IEnumerator ChangeColorGreen()
    {
        GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.Lerp(Color.green, Color.white, 0.33f));
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);
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
