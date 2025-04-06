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
    private bool isDying = false;

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
        if (targetDirection == Vector2.zero || isDying)
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
        if (health <= 0 && !isDying)
        {
            StartCoroutine("DeathEffect");
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

    IEnumerator DeathEffect()
    {
        isDying = true;
        GetComponent<EnemyAttack>().enabled = false;
        float currentTime = 0;
        while (currentTime < 0.5f)
        {
            renderer.material.SetColor("_Color", Color.red);
            transform.rotation = Quaternion.Euler(currentTime / 0.5f * -90, transform.rotation.y, transform.rotation.z);
            //transform.rotation *= Quaternion.AngleAxis(-45 * Time.deltaTime, transform.right);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
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
