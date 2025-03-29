using System.Collections.Generic;
using UnityEngine;

public class SkillUseModal : MonoBehaviour
{
    public List<PlayerCharacterData> currParty;
    public GameObject charDataPrefab;

    public void PopulateData(Skill skillToUse)
    {
        currParty = GameManager.instance.partyManager.partyData;
    }
}
