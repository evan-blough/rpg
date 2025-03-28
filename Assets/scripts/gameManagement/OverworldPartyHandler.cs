using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverworldPartyHandler : MonoBehaviour
{
    public GameObject playerCharacterPrefab;
    PlayerControls controls;

    public List<RuntimeAnimatorController> currParty;
    [SerializeField]
    RuntimeAnimatorController heroAnimation;
    [SerializeField]
    RuntimeAnimatorController wizardAnimation;
    [SerializeField]
    RuntimeAnimatorController senatorAnimation;

    public PlayerCharacterHandler playerCharacter;
    public RuntimeAnimatorController currCharacter { get { return currParty.FirstOrDefault(); } }
    bool created = false;

    void Start()
    {
        created = false;
        controls = ControlsHandler.instance.playerControls;
        controls.overworld.SwapLead.performed += ctx => QuickSwapLead();
        GameObject temp;
        currParty.Clear();
        int index = 0;
        temp = Instantiate(playerCharacterPrefab, transform);
        playerCharacter = temp.GetComponent<PlayerCharacterHandler>();

        foreach (PlayerCharacterData pcd in BattlePartyHandler.instance.partyData)
        {
            if (pcd is HeroData && pcd.isInParty)
            {
                if (!currParty.Any(pc => pc == heroAnimation)) currParty.Add(heroAnimation);
            }
            else if (pcd is WizardData)
            {
                if (!currParty.Any(pc => pc == wizardAnimation)) currParty.Add(wizardAnimation);
            }
            else if (pcd is SenatorData)
            {
                if (!currParty.Any(pc => pc == senatorAnimation)) currParty.Add(senatorAnimation);
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
                if (data.isInParty && heroAnimation is not null)
                    currParty.Add(heroAnimation);
            }
            else if (data is WizardData && wizardAnimation is not null)
            {
                if (data.isInParty)
                    currParty.Add(wizardAnimation);
            }
            else if (data is SenatorData && senatorAnimation is not null)
            {
                if (data.isInParty)
                    currParty.Add(senatorAnimation);
            }
        }
        SetLeadCharacterCam();
    }

    void Update()
    {
        if (playerCharacter != SceneManager.instance.cam.cam.LookAt)
        {
            SetLeadCharacterCam();
        }
    }
    public void SetLeadCharacterCam()
    {
        playerCharacter.animator.runtimeAnimatorController = currCharacter;
        SceneManager.instance.cam.cam.Follow = playerCharacter.transform;
        SceneManager.instance.cam.cam.LookAt = playerCharacter.transform;
    }
    public void SwapLeader()
    {
        // need to map input to swapping lead character
    }

    public void QuickSwapLead()
    {
        BattlePartyHandler.instance.QuickSwapLead();

        var temp = currParty[0];
        currParty[0] = currParty[1];
        currParty[1] = currParty[2];
        currParty[2] = temp;
    }

    private void OnEnable()
    {
        if (created)
            FillPartyData();
    }

}
