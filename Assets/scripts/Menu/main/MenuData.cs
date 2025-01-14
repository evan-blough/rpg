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

        foreach (PlayerCharacterData data in BattlePartyHandler.instance.partyData)
        {
            if (!data.isInParty) continue;

            temp = Instantiate(menuSlotPrefab, transform);
            temp.GetComponent<CharacterDisplay>().SetDisplayData(data);
        }
    }
}
