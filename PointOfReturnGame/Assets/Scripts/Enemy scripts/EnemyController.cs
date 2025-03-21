using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed;

    private Rigidbody2D rb;
    private PlayerAwarenessController playerAwareness;
    private Vector2 targetDirection;

    
    [SerializeField] float maxHealth = 10f;
    [SerializeField] float health = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAwareness = GetComponent<PlayerAwarenessController>();
        health = maxHealth;
    }


    private void FixedUpdate()
    {
        UpdateTargetDirection();
        SetVelocity();
    }


    private void UpdateTargetDirection()
    {
        if(playerAwareness.AwareOfPlayer)
        {
            targetDirection = playerAwareness.DirectionToPlayer;
        }
        else
        {
            targetDirection = Vector2.zero;
        }
    }


    private void SetVelocity()
    {
        if(targetDirection == Vector2.zero)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            rb.linearVelocity = targetDirection * speed;
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <=0)
        {
            Destroy(gameObject);
        }
    }
}
