using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] DisplayHealth healthbar;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
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
    }

    public void ReduceHealth(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        healthbar.SetHealth(currentHealth, maxHealth);
    }

    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        healthbar.SetHealth(currentHealth, maxHealth);
    }
}
