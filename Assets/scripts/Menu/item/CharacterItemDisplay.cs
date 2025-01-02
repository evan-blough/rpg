using UnityEngine;

public class CharacterItemDisplay : MonoBehaviour
{
    public GameObject characterDisplayPrefab;

    private void Start()
    {
        SetupMenu();
    }

    public void SetupMenu()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        GameObject temp;
        foreach (PlayerCharacterData pcd in BattlePartyHandler.instance.partyData)
        {
            if (!pcd.isInParty) continue;

            temp = Instantiate(characterDisplayPrefab, transform);
            temp.GetComponentInChildren<BattleInventoryDisplay>().CreateCharacterDisplay(pcd);
        }
    }
}
