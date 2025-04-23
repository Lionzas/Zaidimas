using UnityEngine;


[System.Serializable]
public class EnemyData
{
    public string enemyId;
    public float enemyHealth;
    public bool isDead;


    public EnemyData(string id, float health)
    {
        enemyId = id;
        enemyHealth = health;
        isDead = false;
    }
}
