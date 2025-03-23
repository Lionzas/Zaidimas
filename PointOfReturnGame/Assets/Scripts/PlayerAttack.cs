using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject KnifeAnimation;
    public GameObject SpearAnimation;

    public GameObject KnifeHitbox;
    public GameObject SpearHitbox;


    private enum WeaponType { None, Knife, Spear }
    private WeaponType currentWeapon = WeaponType.None;


    bool isAttacking = false;
    float attackTimer = 0f;


    [SerializeField] float knifeAttackDuration = 0.3f;
    [SerializeField] float spearAttackDuration = 0.5f;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            currentWeapon = WeaponType.None;
            DisableAllObjects();
            Debug.Log("Unequipped weapon");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = WeaponType.Knife;
            Debug.Log("Equipped Knife");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = WeaponType.Spear;
            Debug.Log("Equipped Spear");
        }

        if (Input.GetKeyDown(KeyCode.E) && !isAttacking && currentWeapon != WeaponType.None)
        {
            StartAttack();
        }

        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            float currentDuration = (currentWeapon == WeaponType.Knife) ? knifeAttackDuration : spearAttackDuration;
            if (attackTimer >= currentDuration)
            {
                EndAttack();
            }
        }
    }


    void StartAttack()
    {
        isAttacking = true;
        attackTimer = 0f;

        DisableAllObjects();

        if (currentWeapon == WeaponType.Knife)
        {
            if (KnifeAnimation != null)
                KnifeAnimation.SetActive(true);
            if (KnifeHitbox != null)
                KnifeHitbox.SetActive(true);
        }
        else if (currentWeapon == WeaponType.Spear)
        {
            if (SpearAnimation != null)
                SpearAnimation.SetActive(true);
            if (SpearHitbox != null)
                SpearHitbox.SetActive(true);
        }
    }


    void EndAttack()
    {
        isAttacking = false;
        attackTimer = 0f;
        DisableAllObjects();
    }


    void DisableAllObjects()
    {
        if (KnifeAnimation != null)
        {
            KnifeAnimation.SetActive(false);
        }
        if (SpearAnimation != null)
        {
            SpearAnimation.SetActive(false);         
        }
        if (KnifeHitbox != null)
        {
            KnifeHitbox.SetActive(false);
        }

        if (SpearHitbox != null)
        {
            SpearHitbox.SetActive(false);
        }
    }
}