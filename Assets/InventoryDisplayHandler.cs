using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum CurrentMenu { KEYITEM, BATTLEINVENTORY, USEITEM };
public class InventoryDisplayHandler : MonoBehaviour
{
    Inventory inventory;
    public CharacterItemDisplay characters;
    public GameObject slotPrefab;
    public Text inventoryType;
    InventorySlots currSlot;
    CurrentMenu currentMenu;

    public void PopulateFieldMenu()
    {
        CheckForInventory();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (InventorySlots slot in inventory.items)
        {
            var temp = Instantiate(slotPrefab, transform);
            var newButton = temp.GetComponent<MenuItemButton>();
            newButton.CreateButton(slot);

            var button = newButton.GetComponent<Button>();
            if (newButton.item is HealItem || newButton.item is FieldItem || newButton.item is ReviveItem)
            {
                button.onClick.AddListener(() => OnFieldItemClick(slot));
            }
            else
            {
                button.interactable = false;
            }
        }

        inventoryType.text = "Use Items";
        currentMenu = CurrentMenu.USEITEM;
    }

    public void PopulateBattleItems()
    {
        CheckForInventory();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (InventorySlots slot in inventory.items)
        {
            var temp = Instantiate(slotPrefab, transform);
            var newButton = temp.GetComponent<MenuItemButton>();
            newButton.CreateButton(slot);

            var button = newButton.GetComponent<Button>();
            if (newButton.item is BattleItem)
            {
                button.onClick.AddListener(() => OnBattleItemClick(slot));
            }
            else
            {
                button.interactable = false;
            }
        }

        inventoryType.text = "Move To Battle Inventory";
        currentMenu = CurrentMenu.BATTLEINVENTORY;
    }

    public void PopulateKeyItems()
    {
        CheckForInventory();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (KeyItems keyItem in inventory.keyItems)
        {
            var temp = Instantiate(slotPrefab, transform);
            var newButton = temp.GetComponent<MenuItemButton>();
            newButton.CreateButton(keyItem);
        }

        inventoryType.text = "Key Items";
        currentMenu = CurrentMenu.KEYITEM;
    }

    public void SortInventory()
    {
        if (currentMenu == CurrentMenu.KEYITEM)
            return;

        RefreshInventoryView();
    }

    public void OnFieldItemClick(InventorySlots slot)
    {
        currSlot = slot;
        characters.ActivateFieldTargets(slot.item);
    }

    public void OnFieldItemUse(List<PlayerCharacterData> dataList)
    {
        if (currSlot.item.UseItemInField(dataList))
        {
            currSlot.itemCount -= 1;
            characters.RefreshCharView();
            RefreshInventoryView();
        }
        if (currSlot.itemCount < 1)
        {
            inventory.items.Remove(currSlot);
            currSlot = null;
            characters.DeactivateButtons();
        }
    }

    public void OnBattleItemClick(InventorySlots slot)
    {
        currSlot = slot;
        characters.ActivateBattleTargets(slot.item);
    }

    public void OnBattleItemUse(PlayerCharacterData charToAddTo)
    {
        if (charToAddTo.charInventory.AddItem((BattleItem)currSlot.item))
        {
            currSlot.itemCount -= 1;
            characters.RefreshBattleInventoryView();
            RefreshInventoryView();
        }
        if (currSlot.itemCount < 1)
        {
            inventory.items.Remove(currSlot);
            currSlot = null;
            characters.DeactivateButtons();
        }
    }

    public void RemoveItemFromChar(PlayerCharacterData pcd, Items item)
    {
        pcd.charInventory.items.Remove((BattleItem)item);
        if (inventory.items.Where(i => i.item == item).Any())
        {
            inventory.items.First(i => i.item == item).itemCount++;
        }
        else
        {
            inventory.items.Add(new InventorySlots(item));
        }
        characters.RefreshBattleInventoryView();
        RefreshInventoryView();
    }

    private void RefreshInventoryView()
    {
        var buttons = GetComponentsInChildren<MenuItemButton>().ToList();

        foreach (InventorySlots slot in inventory.items)
        {
            if (buttons.Any(b => b.item == slot.item))
            {
                buttons.First(b => b.item == slot.item).UpdateButtonData();
            }
            else
            {
                var temp = Instantiate(slotPrefab, transform);
                var newButton = temp.GetComponent<MenuItemButton>();
                newButton.CreateButton(slot);

                var button = newButton.GetComponent<Button>();
                if (currentMenu == CurrentMenu.USEITEM)
                {
                    if (newButton.item is HealItem || newButton.item is FieldItem || newButton.item is ReviveItem)
                    {
                        button.onClick.AddListener(() => OnFieldItemClick(slot));
                    }
                    else
                    {
                        button.interactable = false;
                    }
                }
                else
                {
                    if (newButton.item is BattleItem)
                    {
                        button.onClick.AddListener(() => OnBattleItemClick(slot));
                    }
                    else
                    {
                        button.interactable = false;
                    }
                }
            }
        }
    }

    private void CheckForInventory()
    {
        if (inventory is null)
        {
            inventory = BattlePartyHandler.instance.inventory;
        }
    }

}
