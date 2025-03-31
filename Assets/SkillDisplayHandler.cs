using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class SkillDisplayHandler : MonoBehaviour
{
    public PlayerCharacterData data;
    public List<PlayerCharacterData> currParty;
    public GameObject skillButtonPrefab;
    public GameObject sortOptions;
    public SkillUseModal skillUseModal;
    public Button sort;
    public SkillsCharacterHolder characterHolder;

    public void PopulateUseMenu(PlayerCharacterData pcd)
    {
        data = pcd;
        currParty = GameManager.instance.partyManager.partyData;
        PopulateUseMenu();
    }

    public void PopulateUseMenu()
    {
        sortOptions.gameObject.SetActive(false);
        sort.interactable = true;
        characterHolder.PopulateCharacterDisplay();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Skill skill in data.skills)
        {
            var temp = Instantiate(skillButtonPrefab, transform);
            var newButton = temp.GetComponent<SkillsMenuButton>();
            newButton.PopulateButton(skill);

            var button = newButton.GetComponent<Button>();
            if (((newButton.skill is HealingSkill && currParty.Any(p => p.currHP < p.maxHP)
                || (newButton.skill is RevivalSkill && currParty.Any(p => !p.isActive)))
                || (newButton.skill is StatusRemovalSkill && currParty.Any(p => p.currStatuses.Any())))
                && skill.skillPointCost <= data.currSP)
            {
                button.onClick.AddListener(() => OnSkillUseClick(skill));
            }
            else
            {
                button.interactable = false;
            }
        }
    }

    public void PopulateAddMenu()
    {
        sortOptions.gameObject.SetActive(false);
        sort.interactable = true;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Skill skill in data.skills)
        {
            var temp = Instantiate(skillButtonPrefab, transform);
            var newButton = temp.GetComponent<SkillsMenuButton>();
            newButton.PopulateButton(skill);

            var button = newButton.GetComponent<Button>();
            button.onClick.AddListener(() => OnSkillAddClick(skill));
        }
    }

    public void OnSkillUseClick(Skill skill)
    {
        skillUseModal.PopulateData(skill, data);
        skillUseModal.gameObject.SetActive(true);
    }

    public void OnSkillAddClick(Skill skill)
    {
        if (data.equippedSkills.Any(s => s == skill) || data.equippedSkills.Count >= data.skillCount)
            return;

        data.equippedSkills.Add(skill);
        characterHolder.PopulateBattleSkillsDisplay();
    }

    private void OnDisable()
    {
        skillUseModal.gameObject.SetActive(false);
    }
}
