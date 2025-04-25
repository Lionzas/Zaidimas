using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    public GameObject KnifeAnimation;
    public GameObject SpearAnimation;

    public GameObject KnifeHitbox;
    public GameObject SpearHitbox;


    public enum WeaponType { None, Knife, Spear }
    private WeaponType currentWeapon = WeaponType.None;
    [SerializeField] private AudioSource m_knifeSound;
    [SerializeField] private AudioSource m_spearSound;


    bool isAttacking = false;
    float attackTimer = 0f;


    [SerializeField] float knifeAttackDuration = 0.3f;
    [SerializeField] float spearAttackDuration = 0.5f;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha0))
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
        }*/

        if (Input.GetKeyDown(KeyCode.E) && !isAttacking && currentWeapon != WeaponType.None)
        {
            this.GetComponent<PlayerMovement>().UseAnimation();
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


    public void EquipWeapon(ItemData weaponItem)
    {
        if (weaponItem == null || weaponItem.itemType != ItemData.ItemType.Weapon)
        {
            currentWeapon = WeaponType.None;
            DisableAllObjects();
            return;
        }

        currentWeapon = (WeaponType)weaponItem.weaponType;

        DisableAllObjects();
        switch (currentWeapon)
        {
            case WeaponType.Knife:
                if (KnifeAnimation != null) KnifeAnimation.SetActive(true);
                break;
            case WeaponType.Spear:
                if (SpearAnimation != null) SpearAnimation.SetActive(true);
                break;
        }
    }


    public void StartAttack()
    {
        isAttacking = true;
        attackTimer = 0f;

        DisableAllObjects();
        switch (currentWeapon)
        {
            case WeaponType.Knife:
                m_knifeSound.Play();
                if (KnifeAnimation != null) KnifeAnimation.SetActive(true);
                if (KnifeHitbox != null) KnifeHitbox.SetActive(true);
                break;
            case WeaponType.Spear:
                m_spearSound.Play();
                if (SpearAnimation != null) SpearAnimation.SetActive(true);
                if (SpearHitbox != null) SpearHitbox.SetActive(true);
                break;
        }
        /*if (currentWeapon == WeaponType.Knife)
        {
            if (KnifeAnimation != null)
            {
                m_knifeSound.Play();
                KnifeAnimation.SetActive(true);
            }
            if (KnifeHitbox != null)
                KnifeHitbox.SetActive(true);
        }
        else if (currentWeapon == WeaponType.Spear)
        {
            m_spearSound.Play();
            if (SpearAnimation != null)
                SpearAnimation.SetActive(true);
            if (SpearHitbox != null)
                SpearHitbox.SetActive(true);
        }*/
    }


    public void EndAttack()
    {
        isAttacking = false;
        attackTimer = 0f;
        DisableAllObjects();
        switch (currentWeapon)
        {
            case WeaponType.Knife:
                if (KnifeAnimation != null) KnifeAnimation.SetActive(true);
                break;
            case WeaponType.Spear:
                if (SpearAnimation != null) SpearAnimation.SetActive(true);
                break;
        }
    }


    public void DisableAllObjects()
    {
        if (KnifeAnimation != null) KnifeAnimation.SetActive(false);
        if (SpearAnimation != null) SpearAnimation.SetActive(false);
        if (KnifeHitbox != null) KnifeHitbox.SetActive(false);
        if (SpearHitbox != null) SpearHitbox.SetActive(false);
        /*if (KnifeAnimation != null)
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
        }*/
    }
}