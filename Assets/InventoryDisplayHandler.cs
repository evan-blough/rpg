using UnityEngine;

public class InventoryDisplayHandler : MonoBehaviour
{
    public Inventory inventory;
    public GameObject slotPrefab;

    public void PopulateMenu()
    {
        inventory = BattlePartyHandler.instance.inventory;

        foreach (InventorySlots slot in inventory.items)
        {
            var temp = Instantiate(slotPrefab, this.transform);
            var newButton = temp.GetComponent<MenuItemButton>();
            newButton.CreateButton(slot);
        }
    }
}
