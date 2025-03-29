using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    Rigidbody2D rb;
    Vector2 movementDirection;
    Animator anim;
    public Transform Aim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    

    void Update()
    {
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        MovementAnimation();

        if (movementDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            Aim.rotation = Quaternion.AngleAxis(angle+90f, Vector3.forward);
        }
    }


    void FixedUpdate()
    {
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

    public void UseAnimation()
    {
        //anim.SetFloat("MoveX", movementDirection.x);
        //anim.SetFloat("MoveY", movementDirection.y);
        //anim.SetBool("Using", true);
        //anim.Play("Use Tree", 0, 0.0f);
        anim.SetTrigger("Using");
    }
}
