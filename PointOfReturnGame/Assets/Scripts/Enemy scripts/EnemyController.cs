using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed;

    private Rigidbody2D rb;
    private PlayerAwarenessController playerAwareness;
    private Vector2 targetDirection;
    private SpriteRenderer renderer;
    private Animator anim;
    [SerializeField] private AudioSource hitSound;


    [SerializeField] float maxHealth = 10f;
    [SerializeField] float health = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAwareness = GetComponent<PlayerAwarenessController>();
        health = maxHealth;
        renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        UpdateTargetDirection();
        SetVelocity();
        MovementAnimation();
    }


    private void UpdateTargetDirection()
    {
        if (playerAwareness.AwareOfPlayer)
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
        if (targetDirection == Vector2.zero)
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
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            hitSound.Play();
            DontDestroyOnLoad(this.hitSound);
            rb.AddForce(targetDirection.normalized * -100f * damage);
            StartCoroutine("ChangeColor");
        }
    }

    IEnumerator ChangeColor()
    {
        renderer.material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(0.3f);
        renderer.material.SetColor("_Color", Color.white);
    }

    void MovementAnimation()
    {
        if (rb.linearVelocity.magnitude != 0)
        {
            if (Mathf.Abs(rb.linearVelocity.x) > Mathf.Abs(rb.linearVelocity.y))
            {
                anim.SetFloat("MoveX", 1 * Mathf.Sign(rb.linearVelocity.x));
                anim.SetFloat("MoveY", 0);
            }
            else
            {
                anim.SetFloat("MoveY", 1 * Mathf.Sign(rb.linearVelocity.y));
                anim.SetFloat("MoveX", 0);
            }
            anim.SetBool("Walking", true);
        }
        else
            anim.SetBool("Walking", false);
    }

    public void AttackAnimation()
    {
        anim.SetTrigger("Attack");
    }
}
