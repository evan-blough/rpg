using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public List<PlayerCharacterData> partyData;
    public HeroData heroData;
    public WizardData wizardData;
    public SenatorData senatorData;
    public Inventory inventory;
    public int gold;

    private void Awake()
    {
        SetCurrentPartyData();
    }

    private void Start()
    {
        if (heroData.equippedClass is null)
            heroData.equippedClass = heroData.classes[0];
        if (wizardData.equippedClass is null)
            wizardData.equippedClass = wizardData.classes[0];
        if (senatorData.equippedClass is null)
            senatorData.equippedClass = senatorData.classes[0];
    }

    public void SetCurrentPartyData()
    {
        SetCharacterPartyStatus(heroData);
        SetCharacterPartyStatus(wizardData);
        SetCharacterPartyStatus(senatorData);
    }

    public void SetPartyData(List<PlayerCharacter> characters)
    {
        foreach (PlayerCharacter character in characters)
        {
            if (character is Hero) heroData.DeepDataCopy(character);
            else if (character is Wizard) wizardData.DeepDataCopy(character);
            else if (character is Senator) senatorData.DeepDataCopy(character);
        }
    }

    public void QuickSwapLead()
    {
        var temp = partyData[0];
        partyData[0] = partyData[1];
        partyData[1] = partyData[2];
        partyData[2] = temp;
    }

    public void SetCharacterPartyStatus(PlayerCharacterData data)
    {
        if (data is not null && data.isInParty && !partyData.Any(c => c == data))
            partyData.Add(data);

        else if (data is null || (data is not null && !data.isInParty && partyData.Any(d => d == data)))
            partyData.Remove(data);
    }
}
