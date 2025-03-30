using System.Collections.Generic;
using UnityEngine;

public class SkillUseModal : MonoBehaviour
{
    public List<PlayerCharacterData> currParty;
    public GameObject charDataPrefab;
    public GameObject characterList;

    public void PopulateData(Skill skillToUse)
    {
        currParty = GameManager.instance.partyManager.partyData;

        GameObject temp;
        foreach (PlayerCharacterData pcd in currParty)
        {
            temp = Instantiate(charDataPrefab, characterList.transform);
            temp.GetComponent<SkillUseCharacterDisplay>().CreateCharacterDisplay(pcd);
        }
    }
}
