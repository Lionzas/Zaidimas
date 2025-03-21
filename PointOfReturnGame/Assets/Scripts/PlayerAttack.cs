using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Melee;
    bool isAttacking = false;
    [SerializeField] float attackDuration = 0.3f;
    [SerializeField] float attackTimer = 0f;

    void Update()
    {
        CheckMeleeTimer();
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            OnAttack();
        }
    }

    void OnAttack()
    {
        if(!isAttacking)
        {
            Melee.SetActive(true);
            isAttacking = true;
        }
    }

    void CheckMeleeTimer()
    {
        if(isAttacking)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer >= attackDuration)
            {
                attackTimer = 0;
                isAttacking = false;
                Melee.SetActive(false);
            }
        }
    }
}
