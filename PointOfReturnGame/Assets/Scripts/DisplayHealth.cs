using UnityEngine;

public class DisplayHealth : MonoBehaviour
{
    [SerializeField] Material healthbarMat;

    public void SetHealth(int health, int maxHealth)
    {
        healthbarMat.SetFloat("_Health", health * 1.0f / maxHealth);
    }
}
