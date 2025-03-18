using UnityEngine;

public class PlayerAwarenessController : MonoBehaviour
{
    public bool AwareOfPlayer {get; private set;}
    public Vector2 DirectionToPlayer {get; private set;}

    [SerializeField] private float playerAwarenessDistance;
    [SerializeField] private float chaseTimeout = 2.0f;

    private Transform player;
    private float chaseTimer = 0f;


    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
    }


    void Update()
    {
        Vector2 enemyToPlayerVector = player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        if(enemyToPlayerVector.magnitude <= playerAwarenessDistance)
        {
            AwareOfPlayer = true;
            chaseTimer = 0f;
            //Debug.Log("Player detected. Resetting chase timer.");
        }
        else
        {
            chaseTimer += Time.deltaTime;
            //Debug.Log($"Player out of range. Chase timer: {chaseTimer}/{chaseTimeout}");

            if(chaseTimer >= chaseTimeout)
            {
                AwareOfPlayer = false;
                //Debug.Log("Player lost. Enemy stops chasing.");
            }
        }
    }
}
