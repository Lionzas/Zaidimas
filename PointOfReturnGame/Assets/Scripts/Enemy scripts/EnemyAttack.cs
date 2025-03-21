using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackDelay = 0.5f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private int attackDamage = 10;


    private Transform player;
    private EnemyController enemyController;
    private Rigidbody2D rb;

    private enum AttackState { Ready, AttackDelay, Cooldown }
    private AttackState state = AttackState.Ready;
    private float timer = 0f;


    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        enemyController = GetComponent<EnemyController>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        switch (state)
        {
            case AttackState.Ready:
                if (distance <= attackRange)
                {
                    state = AttackState.AttackDelay;
                    timer = attackDelay;
                    
                    if (enemyController != null)
                    {
                        enemyController.enabled = false;
                    }
                    rb.linearVelocity = Vector2.zero;
                }
                break;

            case AttackState.AttackDelay:
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    if (distance <= attackRange)
                    {
                        PlayerProfile playerProfile = player.GetComponent<PlayerProfile>();
                        if (playerProfile != null)
                        {
                            playerProfile.ReduceHealth(attackDamage);
                        }
                    }
                    
                    state = AttackState.Cooldown;
                    timer = attackCooldown;
                    
                    if (enemyController != null)
                        enemyController.enabled = true;
                }
                break;

            case AttackState.Cooldown:
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    state = AttackState.Ready;
                }
                break;
        }
    }
}
