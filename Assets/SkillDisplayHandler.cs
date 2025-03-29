using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillDisplayHandler : MonoBehaviour
{
    public PlayerCharacterData data;
    public GameObject skillButtonPrefab;
    public GameObject sortOptions;
    public Button sort;
    public SkillsCharacterHolder characterHolder;

    public void PopulateUseMenu(PlayerCharacterData pcd)
    {
        data = pcd;
        PopulateUseMenu();
    }

    public void PopulateUseMenu()
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
            if (newButton.skill is HealingSkill || newButton.skill is RevivalSkill || newButton.skill is StatusSkill)
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
        Debug.Log("Program Me!");
    }

    public void OnSkillAddClick(Skill skill)
    {
        if (data.equippedSkills.Any(s => s == skill) || data.equippedSkills.Count >= data.skillCount)
            return;

        data.equippedSkills.Add(skill);
        characterHolder.PopulateBattleSkillsDisplay();
    }
}
