using UnityEngine;
using UnityEngine.UI;

public class MenuItemButton : MonoBehaviour
{
    public Text quantity;
    public Text itemName;
    public Items item;

    public void CreateButton(InventorySlots slot)
    {
        quantity.text = slot.itemCount.ToString();
        itemName.text = slot.item.itemName;
        item = slot.item;
    }

    public void CreateButton(KeyItems keyItem)
    {
        quantity.text = "1";
        itemName.text = keyItem.itemName;
        item = keyItem;
    }
}
