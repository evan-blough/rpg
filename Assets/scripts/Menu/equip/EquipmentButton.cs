using UnityEngine;
using UnityEngine.UI;
public class EquipmentButton : MonoBehaviour
{
    public Text quantity;
    public Text itemName;
    public InventorySlots currSlot;
    public Equipment item;

    public void CreateButton(InventorySlots slot)
    {
        currSlot = slot;
        item = (Equipment)slot.item;
        quantity.text = currSlot.itemCount.ToString();
        itemName.text = item.itemName;
    }

    public void UpdateButtonData()
    {
        quantity.text = currSlot.itemCount.ToString();
        if (currSlot.itemCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
