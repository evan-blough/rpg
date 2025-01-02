using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplayHandler : MonoBehaviour
{
    Inventory inventory;
    public GameObject slotPrefab;
    public Text inventoryType;

    public void PopulateMenu()
    {
        CheckForInventory();

        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (InventorySlots slot in inventory.items)
        {
            var temp = Instantiate(slotPrefab, this.transform);
            var newButton = temp.GetComponent<MenuItemButton>();
            newButton.CreateButton(slot);
        }

        inventoryType.text = "Inventory";
    }

    public void PopulateKeyItems()
    {
        CheckForInventory();

        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (KeyItems keyItem in inventory.keyItems)
        {
            var temp = Instantiate(slotPrefab, this.transform);
            var newButton = temp.GetComponent<MenuItemButton>();
            newButton.CreateButton(keyItem);
        }

        inventoryType.text = "Key Items";
    }

    public void SortInventory()
    {
        return;
    }

    private void CheckForInventory()
    {
        if (inventory is null)
        {
            inventory = BattlePartyHandler.instance.inventory;
        }
    }
}
