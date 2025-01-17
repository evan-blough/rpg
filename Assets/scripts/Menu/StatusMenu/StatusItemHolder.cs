using UnityEngine;
using UnityEngine.UI;
public class StatusItemHolder : MonoBehaviour
{
    public GameObject textPrefab;
    public void FillData(BattleInventory inventory)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (BattleItem item in inventory.items)
        {
            var temp = Instantiate(textPrefab, transform);
            temp.GetComponent<Text>().text = item.name;
        }
    }
}
