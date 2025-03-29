using UnityEngine;
public class MenuData : MonoBehaviour
{
    public GameObject menuSlotPrefab;

    private void Start()
    {
        FillSlotData();
    }
    public void FillSlotData()
    {
        GameObject temp;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (PlayerCharacterData data in GameManager.instance.partyManager.partyData)
        {
            if (!data.isInParty) continue;

            temp = Instantiate(menuSlotPrefab, transform);
            CharacterDisplay display = temp.GetComponent<CharacterDisplay>();
            display.pcd = data;
            display.SetDisplayData();
        }
    }
}
