using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    Rigidbody2D rb;
    Vector2 movementDirection;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate() 
    {
        rb.linearVelocity = movementDirection.normalized * movementSpeed;
        MovementAnimation();
        
    }

    void MovementAnimation()
    {
        if (rb.linearVelocity.magnitude != 0)
        {
            anim.SetFloat("MoveX", movementDirection.x);
            anim.SetFloat("MoveY", movementDirection.y);
            anim.SetBool("Walking", true);
        }
        else
            anim.SetBool("Walking", false);
    }
}
