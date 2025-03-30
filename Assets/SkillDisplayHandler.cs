using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillDisplayHandler : MonoBehaviour
{
    public PlayerCharacterData data;
    public GameObject skillButtonPrefab;
    public GameObject sortOptions;
    public SkillUseModal skillUseModal;
    public Button sort;
    public SkillsCharacterHolder characterHolder;
    bool isUse = false;

    public void PopulateUseMenu(PlayerCharacterData pcd)
    {
        data = pcd;
        PopulateUseMenu();
    }

    public void PopulateUseMenu()
    {
        if (isUse) return;

        sortOptions.gameObject.SetActive(false);
        sort.interactable = true;
        isUse = true;

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
            if (newButton.skill is HealingSkill || newButton.skill is RevivalSkill || newButton.skill is StatusRemovalSkill)
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
        if (!isUse) return;

        sortOptions.gameObject.SetActive(false);
        sort.interactable = true;
        isUse = false;

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
        isUse = true;
        characterHolder.PopulateBattleSkillsDisplay();
    }

    private void OnDisable()
    {
        isUse = false;
        skillUseModal.gameObject.SetActive(false);
    }
}
