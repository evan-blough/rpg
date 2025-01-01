using UnityEngine;
using UnityEngine.UI;

public class MenuItemButton : MonoBehaviour
{
    public Text quantity;
    public Text itemName;
    public InventorySlots itemDetails;


    public void CreateButton(InventorySlots itemToDisplay)
    {
        itemDetails = itemToDisplay;
        itemName.text = itemDetails.item.itemName;
        quantity.text = itemDetails.itemCount.ToString();
    }
}
