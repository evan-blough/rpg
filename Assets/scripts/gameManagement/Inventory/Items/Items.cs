using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Items : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int itemPrice;

    public virtual bool UseItemInField(List<PlayerCharacterData> target)
    {
        return false;
    }
}


