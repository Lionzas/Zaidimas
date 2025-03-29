using UnityEngine;

public class PlayerAwarenessController : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField] private float playerAwarenessDistance;
    [SerializeField] private float chaseTimeout = 2.0f;
    [SerializeField] private AudioSource movementSound; // Looping movement sound
    [SerializeField] private AudioSource aggroSound; // Plays when detecting player

    private Transform player;
    private float chaseTimer = 0f;
    private bool isMoving = false;
    private bool hasPlayedAggroSound = false;

    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;

        if (movementSound != null)
        {
            movementSound.loop = true; // Ensure movement sound loops
        }
    }

    void Update()
    {
        if (player == null) return; // Prevent errors if player is missing

        Vector2 enemyToPlayerVector = player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;
        bool playerDetected = enemyToPlayerVector.magnitude <= playerAwarenessDistance;

        if (playerDetected)
        {
            if (!AwareOfPlayer)
            {
                if (aggroSound != null && !hasPlayedAggroSound)
                {
                    aggroSound.Play(); // Play aggro sound once
                    hasPlayedAggroSound = true;
                }
            }

            AwareOfPlayer = true;
            chaseTimer = 0f;
        }
        else
        {
            chaseTimer += Time.deltaTime;

            if (chaseTimer >= chaseTimeout)
            {
                AwareOfPlayer = false;
                hasPlayedAggroSound = false; // Reset aggro sound trigger
            }
        }

        HandleMovementSound();
    }

    void HandleMovementSound()
    {
        if (AwareOfPlayer) // If chasing player, play movement sound
        {
            if (!isMoving)
            {
                isMoving = true;
                if (movementSound != null && !movementSound.isPlaying)
                {
                    movementSound.Play();
                }
            }
        }
        else // Stop movement sound when enemy stops moving
        {
            if (isMoving)
            {
                isMoving = false;
                if (movementSound != null)
                {
                    movementSound.Stop();
                }
            }
        }
    }
    public void StopAllSounds() // Call this when the enemy dies
    {
        if (movementSound != null) movementSound.Stop();
        if (aggroSound != null) aggroSound.Stop();
    }

    private void OnDisable() // Stops sounds if the object is disabled
    {
        StopAllSounds();
    }

    private void OnDestroy() // Stops sounds if the object is destroyed
    {
        StopAllSounds();
    }
}
