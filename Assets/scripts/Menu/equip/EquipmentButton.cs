using UnityEngine;
using UnityEngine.UI;
public class EquipmentButton : MonoBehaviour
{
    public Text quantity;
    public Text itemName;
    public InventorySlots currSlot;
    public Equipment item;
    StatDiffBlock sdb;
    ElemDiffBlock edb;
    bool isFirst;

    public void CreateButton(InventorySlots slot, StatDiffBlock sDiffBlock, ElemDiffBlock eDiffBlock, bool isFirstAccessory = false)
    {
        currSlot = slot;
        item = (Equipment)slot.item;
        quantity.text = currSlot.itemCount.ToString();
        itemName.text = item.itemName;
        sdb = sDiffBlock;
        edb = eDiffBlock;
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

    public void AddElements()
    {
        if (item is Weapon)
            edb.PopulateWeaponBlock((Weapon)item);
        else if (item is Armor)
            edb.PopulateArmorBlock((Armor)item);
        else if (item is Accessory)
        {
            if (isFirst)
                edb.PopulateAccessoryBlock((Accessory)item);
            else
                edb.PopulateAccessoryBlock((Accessory)item, true);
        }
    }

    public void RemoveStats()
    {
        sdb.PopulateBlock(sdb.data);
    }
}
