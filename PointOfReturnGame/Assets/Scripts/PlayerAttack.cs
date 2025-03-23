using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Knife;
    public GameObject Spear;
    bool isAttacking = false;
    [SerializeField] float knifeAttackDuration = 0.3f;
    [SerializeField] float knifeAttackTimer = 0f;

    [SerializeField] float spearAttackDuration = 0.5f;
    [SerializeField] float spearAttackTimer = 1f;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            CheckKnifeTimer();
        
            if(Input.GetKeyDown(KeyCode.E))
            {
                KnifeAttack();
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            CheckSpearTimer();
        
            if(Input.GetKeyDown(KeyCode.E))
            {
                SpearAttack();
            }
        }
    }

    void KnifeAttack()
    {
        if(!isAttacking)
        {
            Knife.SetActive(true);
            isAttacking = true;
        }
    }

    void CheckKnifeTimer()
    {
        if(isAttacking)
        {
            knifeAttackTimer += Time.deltaTime;
            if(knifeAttackTimer >= knifeAttackDuration)
            {
                knifeAttackTimer = 0;
                isAttacking = false;
                Knife.SetActive(false);
            }
        }
    }

    void SpearAttack()
    {
        if(!isAttacking)
        {
            Spear.SetActive(true);
            isAttacking = true;
        }
    }

    void CheckSpearTimer()
    {
        if(isAttacking)
        {
            spearAttackTimer += Time.deltaTime;
            if(spearAttackTimer >= spearAttackDuration)
            {
                spearAttackTimer = 0;
                isAttacking = false;
                Spear.SetActive(false);
            }
        }
    }
}
