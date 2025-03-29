using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private AudioSource movementSound;

    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private Animator anim;
    public Transform Aim;
    public VectorValue startingPosition;
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        transform.position = startingPosition.initialValue;

        if (movementSound != null)
        {
            movementSound.loop = true; // Ensure sound loops
            DontDestroyOnLoad(movementSound.gameObject); // Prevents stopping on scene change
        }
    }

    void Update()
    {
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        MovementAnimation();

        if (movementDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            Aim.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);

            if (!isMoving)
            {
                isMoving = true;
                if (!movementSound.isPlaying)
                {
                    movementSound.Play(); // Play sound only if it's not already playing
                }
            }
        }
        else
        {
            if (isMoving) // Stop sound only when movement fully stops
            {
                isMoving = false;
                movementSound.Stop();
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movementDirection.normalized * movementSpeed; // Fixed incorrect property
    }

    void MovementAnimation()
    {
        if (movementDirection != Vector2.zero)
        {
            anim.SetFloat("MoveX", movementDirection.x);
            anim.SetFloat("MoveY", movementDirection.y);
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

    public void UseAnimation()
    {
        //anim.SetFloat("MoveX", movementDirection.x);
        //anim.SetFloat("MoveY", movementDirection.y);
        //anim.SetBool("Using", true);
        //anim.Play("Use Tree", 0, 0.0f);
        anim.SetTrigger("Using");
    }
}
