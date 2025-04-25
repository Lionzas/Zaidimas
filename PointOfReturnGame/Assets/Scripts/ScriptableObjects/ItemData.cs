using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemId;
    public Sprite icon;
    public ItemType itemType;
    //public WeaponType weaponType;

    public PlayerAttack.WeaponType weaponType;


    public enum ItemType { General, Weapon }
    //public enum WeaponType { Knife, Spear }
}
