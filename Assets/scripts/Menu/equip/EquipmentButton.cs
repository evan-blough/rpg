using UnityEngine;
using UnityEngine.UI;
public class EquipmentButton : MonoBehaviour
{
    public Text quantity;
    public Text itemName;
    public InventorySlots currSlot;
    public Equipment item;
    StatDiffBlock sdb;
    bool isFirst;

    public void CreateButton(InventorySlots slot, StatDiffBlock diffBlock, bool isFirstAccessory = false)
    {
        currSlot = slot;
        item = (Equipment)slot.item;
        quantity.text = currSlot.itemCount.ToString();
        itemName.text = item.itemName;
        sdb = diffBlock;
        isFirst = isFirstAccessory;
    }

    public void UpdateButtonData()
    {
        quantity.text = currSlot.itemCount.ToString();
        if (currSlot.itemCount == 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddStats()
    {
        if (item is Weapon)
            sdb.PopulateBlock((Weapon)item);
        else if (item is Armor)
            sdb.PopulateBlock((Armor)item);
        else if (item is Accessory)
        {
            if (isFirst)
                sdb.PopulateAccessory1Block((Accessory)item);
            else
                sdb.PopulateAccessory2Block((Accessory)item);
        }
    }

    public void RemoveStats()
    {
        sdb.PopulateBlock(sdb.data);
    }
}
