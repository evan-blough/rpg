using UnityEngine;
using UnityEngine.UI;

public class BattleInventoryDisplay : MonoBehaviour
{
    public Image portrait;
    public GameObject battleInventory;
    public GameObject battleInventoryButtonPrefab;
    public PlayerCharacterData pcd;

    public void CreateCharacterDisplay(PlayerCharacterData data)
    {
        pcd = data;
        portrait.sprite = data.portrait;
        PopulateBattleInventoryDisplay();
    }

    public void PopulateBattleInventoryDisplay()
    {
        GameObject temp;
        foreach (Items item in pcd.charInventory.items)
        {
            temp = Instantiate(battleInventoryButtonPrefab, battleInventory.transform);
            temp.GetComponentInChildren<BattleItemButton>().PopulateButton(item);
        }
    }

    public void PopulateCharacterDisplay()
    {
        return;
    }
}
