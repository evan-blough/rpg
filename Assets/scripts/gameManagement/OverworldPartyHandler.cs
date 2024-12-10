using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverworldPartyHandler : MonoBehaviour
{
    public GameObject heroPrefab;
    public GameObject wizardPrefab;
    public GameObject senatorPrefab;

    public List<CharacterHandler> currParty;
    CharacterHandler heroController;
    CharacterHandler wizardController;
    CharacterHandler senatorController;

    public CharacterHandler leadCharacter { get { return currParty.FirstOrDefault(); } }

    void Start()
    {
        GameObject temp;
        int index = 0;

        foreach (PlayerCharacterData pcd in BattlePartyHandler.instance.partyData)
        {
            if (pcd is HeroData && pcd.isInParty)
            {
                temp = Instantiate(heroPrefab, transform);

                heroController = temp.GetComponent<CharacterHandler>();
                heroController.partyHandler = this;
                if (!currParty.Any(pc => pc == heroController)) currParty.Add(heroController);
            }
            else if (pcd is WizardData)
            {
                temp = Instantiate(wizardPrefab, transform);

                wizardController = temp.GetComponent<CharacterHandler>();
                wizardController.partyHandler = this;
                if (!currParty.Any(pc => pc == wizardController)) currParty.Add(wizardController);
            }
            else if (pcd is SenatorData)
            {
                temp = Instantiate(senatorPrefab, transform);
                //var senator = temp.GetComponent<Senator>();

                //if (senatorData is null)
                //    senatorData.DeepDataCopy(senator);
                //else
                //    senator.DeepCopyFrom(pcd);

                senatorController = temp.GetComponent<CharacterHandler>();
                senatorController.partyHandler = this;
                if (!currParty.Any(pc => pc == senatorController)) currParty.Add(senatorController);
            }
            index++;
        }
        SetLeadCharacterCam();
    }

    void Update()
    {
        if (leadCharacter != GameManager.instance.cam.cam.LookAt)
        {
            SetLeadCharacterCam();
        }
    }
    public void SetLeadCharacterCam()
    {
        GameManager.instance.cam.cam.Follow = leadCharacter.transform;
        GameManager.instance.cam.cam.LookAt = leadCharacter.transform;
    }
    public void SwapLeader()
    {
        // need to map input to swapping lead character
    }
}
