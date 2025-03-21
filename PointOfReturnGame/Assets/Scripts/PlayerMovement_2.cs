using UnityEngine;

public class PlayerMovement_2 : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    Rigidbody2D rb;
    Vector2 movementDirection;
    Animator anim;
    public Transform Aim; // Assign this in the inspector

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Read movement input
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Update animations
        MovementAnimation();

        // Rotate Aim object based on movement direction
        if (movementDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            Aim.rotation = Quaternion.AngleAxis(angle+90f, Vector3.forward);
        }
    }

    void FixedUpdate()
    {
        // Use rb.velocity (not linearVelocity) to move the player
        rb.linearVelocity = movementDirection.normalized * movementSpeed;
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
}
