using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    //public PlayerWeaponController weaponController;

    public Transform hotbarParent;
    public InventorySlot[] slots;
    public int selectedSlot = 0;

    public List<ItemData> inventoryItems = new List<ItemData>();
     public List<string> pickedUpItemIds = new List<string>();
    public List<string> consumedItemIds = new List<string>();
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeSlots();
        RefreshInventory();
    }
    
    
    private void InitializeSlots()
    {
        if (hotbarParent == null)
        {
            var hotbarObj = GameObject.FindGameObjectWithTag("Inventory");
            if (hotbarObj != null) hotbarParent = hotbarObj.transform;
        }

        if (hotbarParent == null) return;

        slots = hotbarParent.GetComponentsInChildren<InventorySlot>(true);

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotIndex = i;
            slots[i].inventoryIcon.transform.localScale = Vector3.one;
            slots[i].ClearSlot();
        }

        if (slots.Length > 0)
        {
            SelectSlot(selectedSlot);
        }
    }
    
    
    private void RefreshInventory()
    {
        if (slots == null) return;

        foreach (var slot in slots)
        {
            if (slot != null) slot.ClearSlot();
        }

        for (int i = 0; i < inventoryItems.Count && i < slots.Length; i++)
        {
            if (slots[i] != null && inventoryItems[i] != null)
            {
                slots[i].AddItem(inventoryItems[i]);
            }
        }
    }
    

    public bool HasItem(ItemData item)
    {
        return inventoryItems.Contains(item);
    }


    public bool HasItemId(string itemId)
    {
        return inventoryItems.Exists(item => item.itemId == itemId);
    }


    public bool SelectedHasItemId(string itemId)
    {
        return slots[selectedSlot].item?.itemId == itemId;
    }


    public bool RemoveItem(string itemId)
    {
        ItemData item = inventoryItems.Find(item => item.itemId == itemId);
        if (item == null) return false;

        inventoryItems.Remove(item);

        RefreshInventory();
        return true;
    }


    public bool RemoveItemByIdInSlot(string itemId)
    {
        InventorySlot currentSlot = slots[selectedSlot];

        if (currentSlot.item != null && currentSlot.item.itemId == itemId)
        {
            bool removedFromList = inventoryItems.Remove(currentSlot.item);

            if (removedFromList)
            {
                consumedItemIds.Add(itemId);
                currentSlot.ClearSlot();
                return true;
            }
        }
        return false;
    }


    public bool IsItemActiveInWorld(string itemId)
    {
        return !pickedUpItemIds.Contains(itemId) && 
               !consumedItemIds.Contains(itemId);
    }

    
    private void Update()
    {
        if (slots == null || slots.Length == 0) return;

        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectSlot(i);
            }
        }
    }


    public ItemData GetCurrentWeapon()
    {
        if (slots == null || slots.Length <= selectedSlot) return null;
        var slotItem = slots[selectedSlot].item;
        return (slotItem != null && slotItem.itemType == ItemData.ItemType.Weapon) ? slotItem : null;
    }


    public void SelectSlot(int index)
    {
        if (slots == null || index < 0 || index >= slots.Length) return;

        if (selectedSlot < slots.Length && slots[selectedSlot] != null)
        {
            slots[selectedSlot].SetHighlight(false);
        }

        selectedSlot = index;
        slots[selectedSlot].SetHighlight(true);

        var currentItem = slots[selectedSlot].item;
        if (currentItem != null && currentItem.itemType == ItemData.ItemType.Weapon)
        {
            PlayerAttack.instance.EquipWeapon(currentItem);
        }
        else
        {
            PlayerAttack.instance.DisableAllObjects();
        }
    
    }
    
    
    public bool AddItem(ItemData item)
    {
        if (item == null || slots == null) return false;

        if (inventoryItems.Contains(item))
        {
            return false;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(item);
                inventoryItems.Add(item);
                return true;
            }
        }

        return false;
    }
}
