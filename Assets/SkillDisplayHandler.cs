using UnityEngine;
using UnityEngine.UI;

public class SkillDisplayHandler : MonoBehaviour
{
    public PlayerCharacterData data;
    public GameObject skillButtonPrefab;
    public GameObject sortOptions;
    public Button sort;
    InventorySlots currSlot;

    public void PopulateUseMenu(PlayerCharacterData data)
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

    public void OnSkillUseClick(Skill skill)
    {
        Debug.Log(skill);
    }
}
