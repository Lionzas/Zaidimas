using UnityEngine;
using System.Collections;

public class PlayerProfile : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] DisplayHealth healthbar;
    private SpriteRenderer renderer;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
        renderer = GetComponent<SpriteRenderer>();
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
        StartCoroutine("ChangeColor");
    }

    IEnumerator ChangeColor()
    {
        renderer.material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(0.3f);
        renderer.material.SetColor("_Color", Color.white);
    }

    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        healthbar.SetHealth(currentHealth, maxHealth);
    }
}
