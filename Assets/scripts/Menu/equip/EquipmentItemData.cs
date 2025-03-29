using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItemData : MonoBehaviour
{
    public GameObject slotPrefab;
    public GameObject removePrefab;
    EquipMenuData equipMenuData;
    PlayerCharacterData data;
    List<InventorySlots> inventory;

    public void PopulateData(PlayerCharacterData pcd, EquipMenuData emd)
    {
        data = pcd;
        inventory = GameManager.instance.partyManager.inventory.items;
        equipMenuData = emd;
    }

    public void EraseEquipment()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void PopulateWeapons()
    {
        GameObject temp;
        Button button;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        if (data.weapon is not null)
        {
            temp = Instantiate(removePrefab, transform);
            temp.GetComponent<Button>().onClick.AddListener(() => SwapWeapon(null));
        }

        foreach (InventorySlots equipmentSlot in inventory)
        {
            if (equipmentSlot.item is not Weapon) continue;

            temp = Instantiate(slotPrefab, transform);
            temp.GetComponent<EquipmentButton>().CreateButton(equipmentSlot, equipMenuData.statDiffBlock, equipMenuData.elemDiffBlock);
            button = temp.GetComponent<Button>();

            if (((Weapon)equipmentSlot.item).weight > data.weightClass)
                continue;

            button.onClick.AddListener(() => SwapWeapon((Weapon)equipmentSlot.item));
        }
    }

    public void PopulateArmor()
    {
        GameObject temp;
        Button button;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        if (data.armor is not null)
        {
            temp = Instantiate(removePrefab, transform);
            temp.GetComponent<Button>().onClick.AddListener(() => SwapArmor(null));
        }

        foreach (InventorySlots equipmentSlot in inventory)
        {
            if (equipmentSlot.item is not Armor) continue;

            temp = Instantiate(slotPrefab, transform);
            temp.GetComponent<EquipmentButton>().CreateButton(equipmentSlot, equipMenuData.statDiffBlock, equipMenuData.elemDiffBlock);
            button = temp.GetComponent<Button>();

            if (((Armor)equipmentSlot.item).weight > data.weightClass)
                continue;

            button.onClick.AddListener(() => SwapArmor((Armor)equipmentSlot.item));
        }
    }

    public void PopulateAccessories(Accessory accessoryToSwap)
    {
        GameObject temp;
        Button button;
        bool isFirst = data.accessory1 == accessoryToSwap ? true : false;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        if (isFirst && data.accessory1 is not null)
        {
            temp = Instantiate(removePrefab, transform);
            temp.GetComponent<Button>().onClick.AddListener(() => SwapAccessory(data.accessory1, null));
        }
        else if (!isFirst && data.accessory2 is not null)
        {
            temp = Instantiate(removePrefab, transform);
            temp.GetComponent<Button>().onClick.AddListener(() => SwapAccessory(data.accessory2, null));
        }

        foreach (InventorySlots equipmentSlot in inventory)
        {
            if (equipmentSlot.item is not Accessory) continue;

            temp = Instantiate(slotPrefab, transform);
            temp.GetComponent<EquipmentButton>().CreateButton(equipmentSlot, equipMenuData.statDiffBlock, equipMenuData.elemDiffBlock, isFirst);
            button = temp.GetComponent<Button>();

            if (((Equipment)equipmentSlot.item).weight > data.weightClass)
                continue;

            button.onClick.AddListener(() => SwapAccessory(accessoryToSwap, (Accessory)equipmentSlot.item));
        }
    }

    public void SwapWeapon(Weapon newWeapon)
    {
        Weapon toSwap = data.weapon;

        if (newWeapon != null)
        {
            InventorySlots slot = inventory.Where(i => i.item == newWeapon).First();

            if (slot.itemCount <= 1)
                inventory.Remove(slot);
            else
                slot.itemCount -= 1;
        }
        data.weapon = newWeapon;

        if (inventory.Any(i => i.item == toSwap))
            inventory.Where(i => i.item == toSwap).First().itemCount++;
        else
        {
            inventory.Add(new InventorySlots(toSwap));
        }
        equipMenuData.UpdateMenu();
        EraseEquipment();
    }

    public void SwapArmor(Armor newArmor)
    {
        Armor toSwap = data.armor;

        if (newArmor != null)
        {
            InventorySlots slot = inventory.Where(i => i.item == newArmor).First();

            if (slot.itemCount <= 1)
                inventory.Remove(slot);
            else
                slot.itemCount -= 1;
        }
        data.armor = newArmor;

        if (inventory.Any(i => i.item == toSwap))
            inventory.Where(i => i.item == toSwap).First().itemCount++;
        else
        {
            inventory.Add(new InventorySlots(toSwap));
        }
        equipMenuData.UpdateMenu();
        EraseEquipment();
    }

    public void SwapAccessory(Accessory accessoryToSwap, Accessory newAccessory)
    {
        Accessory toSwap = data.accessory1 == accessoryToSwap ? data.accessory1 : data.accessory2;

        if (newAccessory != null)
        {
            InventorySlots slot = inventory.Where(i => i.item == newAccessory).First();

            if (slot.itemCount <= 1)
                inventory.Remove(slot);
            else
                slot.itemCount -= 1;
        }

        if (accessoryToSwap == data.accessory1)
            data.accessory1 = newAccessory;
        else
            data.accessory2 = newAccessory;

        if (inventory.Any(i => i.item == toSwap))
            inventory.Where(i => i.item == toSwap).First().itemCount++;
        else
        {
            inventory.Add(new InventorySlots(toSwap));
        }
        equipMenuData.UpdateMenu();
        EraseEquipment();
    }
}
