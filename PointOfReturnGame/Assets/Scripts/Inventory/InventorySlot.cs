using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image inventoryIcon;
    public ItemData item;
    public int slotIndex;

    public Image slotImage;
    
    [Range(0,1)] public float highlightAlpha = 0.7f;
    public Color highlightTint = Color.yellow;
    
    private Color originalColor;
    private Sprite originalSprite;
    

    void Awake()
    {
        originalColor = slotImage.color;
        originalSprite = slotImage.sprite;
    }

    public void SetHighlight(bool isActive)
    {
        if (isActive)
        {
            slotImage.color = highlightTint;
            slotImage.color = new Color(
                highlightTint.r,
                highlightTint.g,
                highlightTint.b,
                highlightAlpha
            );
        }
        else
        {
            slotImage.color = originalColor;
            slotImage.sprite = originalSprite;
        }
    }

    public void AddItem(ItemData newItem)
    {
        item = newItem;
        inventoryIcon.sprite = newItem.icon;
        inventoryIcon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        inventoryIcon.sprite = null;
        inventoryIcon.enabled = false;
    }
}
