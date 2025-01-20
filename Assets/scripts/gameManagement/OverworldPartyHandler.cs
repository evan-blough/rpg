using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverworldPartyHandler : MonoBehaviour
{
    public GameObject heroPrefab;
    public GameObject wizardPrefab;
    public GameObject senatorPrefab;

    public List<PlayerCharacterHandler> currParty;
    PlayerCharacterHandler heroController;
    PlayerCharacterHandler wizardController;
    PlayerCharacterHandler senatorController;

    public PlayerCharacterHandler leadCharacter { get { return currParty.FirstOrDefault(); } }

    void Start()
    {
        GameObject temp;
        currParty.Clear();
        int index = 0;

        foreach (PlayerCharacterData pcd in BattlePartyHandler.instance.partyData)
        {
            if (pcd is HeroData && pcd.isInParty)
            {
                temp = Instantiate(heroPrefab, transform);

                heroController = temp.GetComponent<PlayerCharacterHandler>();
                heroController.partyHandler = this;
                if (!currParty.Any(pc => pc == heroController)) currParty.Add(heroController);
            }
            else if (pcd is WizardData)
            {
                temp = Instantiate(wizardPrefab, transform);

                wizardController = temp.GetComponent<PlayerCharacterHandler>();
                wizardController.partyHandler = this;
                if (!currParty.Any(pc => pc == wizardController)) currParty.Add(wizardController);
            }
            else if (pcd is SenatorData)
            {
                temp = Instantiate(senatorPrefab, transform);

                senatorController = temp.GetComponent<PlayerCharacterHandler>();
                senatorController.partyHandler = this;
                if (!currParty.Any(pc => pc == senatorController)) currParty.Add(senatorController);
            }
            index++;
        }
        SetLeadCharacterCam();
    }

    public void FillPartyData()
    {
        if (currParty.Any())
            currParty.Clear();

        foreach (PlayerCharacterData data in BattlePartyHandler.instance.partyData)
        {
            if (data is HeroData)
            {
                if (data.isInParty && heroController is not null)
                    currParty.Add(heroController);
            }
            else if (data is WizardData)
            {
                if (data.isInParty && wizardController is not null)
                    currParty.Add(wizardController);
            }
            else if (data is SenatorData)
            {
                if (data.isInParty && senatorController is not null)
                    currParty.Add(senatorController);
            }
        }
        SetLeadCharacterCam();
    }

    void Update()
    {
        if (leadCharacter != SceneManager.instance.cam.cam.LookAt)
        {
            SetLeadCharacterCam();
        }
    }
    public void SetLeadCharacterCam()
    {
        SceneManager.instance.cam.cam.Follow = leadCharacter.transform;
        SceneManager.instance.cam.cam.LookAt = leadCharacter.transform;
    }
    public void SwapLeader()
    {
        // need to map input to swapping lead character
    }

    private void OnEnable()
    {
        FillPartyData();
    }
}
