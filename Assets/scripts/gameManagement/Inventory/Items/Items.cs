using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Items : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int itemPrice;
}


