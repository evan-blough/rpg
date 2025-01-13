using System.Collections.Generic;

[System.Serializable]
public class BattleInventory
{
    public List<BattleItem> items;
    public int maxItems;

    public bool AddItem(BattleItem item)
    {
        if (items.Count == maxItems) return false;

        items.Add(item);

        return true;
    }
}
