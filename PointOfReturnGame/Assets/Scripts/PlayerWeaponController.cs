using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public static PlayerWeaponController  instance;

    public GameObject KnifeAnimation;
    public GameObject SpearAnimation;

    public GameObject KnifeHitbox;
    public GameObject SpearHitbox;


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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isAttacking && InventoryManager.instance.GetCurrentWeapon() != null)
        {
            this.GetComponent<PlayerMovement>().UseAnimation();
            StartAttack();
        }

        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            float currentDuration = (InventoryManager.instance.GetCurrentWeapon().weaponType == PlayerAttack.WeaponType.Knife)
                ? knifeAttackDuration : spearAttackDuration;

            if (attackTimer >= currentDuration)
            {
                EndAttack();
            }
        }
    }


    public void EquipWeapon(ItemData weaponItem)
    {
        DisableAllObjects();

        if (weaponItem.weaponType == PlayerAttack.WeaponType.Knife)
        {
            if (KnifeAnimation != null) KnifeAnimation.SetActive(true);
        }
        else if (weaponItem.weaponType == PlayerAttack.WeaponType.Spear)
        {
            if (SpearAnimation != null) SpearAnimation.SetActive(true);
        }
    }


    void StartAttack()
    {
        var currentWeapon = InventoryManager.instance.GetCurrentWeapon();
        if (currentWeapon == null) return;

        isAttacking = true;
        attackTimer = 0f;

        DisableAllObjects();
        if (currentWeapon.weaponType == PlayerAttack.WeaponType.Knife)
        {
            m_knifeSound.Play();
            if (KnifeAnimation != null) KnifeAnimation.SetActive(true);
            if (KnifeHitbox != null) KnifeHitbox.SetActive(true);
        }
        else if (currentWeapon.weaponType == PlayerAttack.WeaponType.Spear)
        {
            m_spearSound.Play();
            if (SpearAnimation != null) SpearAnimation.SetActive(true);
            if (SpearHitbox != null) SpearHitbox.SetActive(true);
        }
    }


    void EndAttack()
    {
        isAttacking = false;
        attackTimer = 0f;
        DisableAllObjects();
        var currentWeapon = InventoryManager.instance.GetCurrentWeapon();
        if (currentWeapon != null)
        {
            if (currentWeapon.weaponType == PlayerAttack.WeaponType.Knife && KnifeAnimation != null)
                KnifeAnimation.SetActive(true);
            else if (currentWeapon.weaponType == PlayerAttack.WeaponType.Spear && SpearAnimation != null)
                SpearAnimation.SetActive(true);
        }
    }
    

    void DisableAllObjects()
    {
        if (KnifeAnimation != null) KnifeAnimation.SetActive(false);
        if (SpearAnimation != null) SpearAnimation.SetActive(false);
        if (KnifeHitbox != null) KnifeHitbox.SetActive(false);
        if (SpearHitbox != null) SpearHitbox.SetActive(false);
    }
}
