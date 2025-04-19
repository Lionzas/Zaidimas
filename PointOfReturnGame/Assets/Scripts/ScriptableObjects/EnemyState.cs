using UnityEngine;

[CreateAssetMenu(fileName = "EnemyState", menuName = "Scriptable Objects/EnemyState")]
public class EnemyState : ScriptableObject
{
    public float enemyHealth;
    public bool isDead;
}
