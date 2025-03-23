using UnityEngine;

public class PlayerAttack2 : MonoBehaviour
{
    public GameObject Knife;
    public GameObject Spear;

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
            Debug.Log("Unequipped weapon");
            Knife.SetActive(false);
            Spear.SetActive(false);
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
            float currentDuration = 0f;
            if (currentWeapon == WeaponType.Knife)
            {
                currentDuration = knifeAttackDuration;
            }
            else if (currentWeapon == WeaponType.Spear)
            {
                currentDuration = spearAttackDuration;
            }

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
        Knife.SetActive(false);
        Spear.SetActive(false);
        if (currentWeapon == WeaponType.Knife)
        {
            Knife.SetActive(true);
        }
        else if (currentWeapon == WeaponType.Spear)
        {
            Spear.SetActive(true);
        }
    }


    void EndAttack()
    {
        isAttacking = false;
        attackTimer = 0f;
        Knife.SetActive(false);
        Spear.SetActive(false);
    }
}
