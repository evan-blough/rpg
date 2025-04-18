using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillUseModal : MonoBehaviour
{
    public List<PlayerCharacterData> currParty;
    public GameObject charDataPrefab;
    public GameObject characterList;
    public Button allCharsButton;
    public SkillDisplayHandler skillDisplayHandler;
    public OptionsList options;
    public Text title;
    public Text description;
    public Text skillPointCost;
    public Text caster;
    public Text isMagic;
    public Text isRanged;
    public Text isMultiTargeted;
    public Text whatElement;

    public void PopulateData(Skill skillToUse, PlayerCharacterData charUsing)
    {
        currParty = GameManager.instance.partyManager.partyData;
        options.NestedSkillSubmenuOpen();
        foreach (Transform child in characterList.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject temp;
        allCharsButton.onClick.RemoveAllListeners();
        allCharsButton.interactable = false;
        foreach (PlayerCharacterData pcd in currParty)
        {
            temp = Instantiate(charDataPrefab, characterList.transform);
            temp.GetComponent<SkillUseCharacterDisplay>().CreateCharacterDisplay(pcd);

            Button button = temp.GetComponent<Button>();
            if ((skillToUse is HealingSkill && pcd.currHP == pcd.maxHP)
                || (skillToUse is StatusRemovalSkill && !pcd.currStatuses.Any())
                || (skillToUse is RevivalSkill && pcd.isActive))
            {
                button.interactable = false;
            }
            else if (!skillToUse.isMultiTargeted)
            {
                button.onClick.RemoveAllListeners();
                List<PlayerCharacterData> target = new List<PlayerCharacterData>();
                target.Add(pcd);
                button.onClick.AddListener(() => UseSkill(skillToUse, charUsing, target));
            }
            else
                button.interactable = false;
        }
        if (skillToUse.isMultiTargeted)
        {
            if ((skillToUse is HealingSkill && currParty.Any(pcd => pcd.currHP != pcd.maxHP))
                || (skillToUse is StatusRemovalSkill && currParty.Any(pcd => pcd.currStatuses.Any()))
                || (skillToUse is RevivalSkill && !currParty.Any(pcd => pcd.isActive)))
            {
                allCharsButton.onClick.AddListener(() => UseSkill(skillToUse, charUsing, currParty));
                allCharsButton.interactable = true;
            }
        }
        PopulateSkillData(skillToUse, charUsing);
    }

    public void UseSkill(Skill skill, PlayerCharacterData currCharacter, List<PlayerCharacterData> targets)
    {
        if (currCharacter.currSP < skill.skillPointCost) return;

        currCharacter.currSP -= skill.skillPointCost;
        skill.UseFieldSkill(currCharacter, targets);

        if (currCharacter.currSP < skill.skillPointCost)
        {
            skillDisplayHandler.PopulateUseMenu();
            gameObject.SetActive(false);
        }

        RepopulateData(skill, currCharacter, targets);

        if ((skill is HealingSkill && !currParty.Any(t => t.currHP != t.maxHP))
            || (skill is StatusRemovalSkill && !currParty.Any(t => t.currStatuses.Any()))
            || (skill is RevivalSkill && !currParty.Any(t => !t.isActive)))
        {
            skillDisplayHandler.PopulateUseMenu();
            gameObject.SetActive(false);
        }
    }

    public void RepopulateData(Skill skillToUse, PlayerCharacterData currChar, List<PlayerCharacterData> targets)
    {
        List<SkillUseCharacterDisplay> displays = GetComponentsInChildren<SkillUseCharacterDisplay>().ToList();
        displays.First(x => x.pcd == currChar).PopulateCharacterDisplay();
        foreach (PlayerCharacterData target in targets)
        {
            if (displays.Any(x => x.pcd == target) && currChar != target)
            {
                displays.First(x => x.pcd == target).PopulateCharacterDisplay();
            }

            if ((skillToUse is HealingSkill && target.currHP == target.maxHP)
                || (skillToUse is StatusRemovalSkill && !target.currStatuses.Any())
                || (skillToUse is RevivalSkill && target.isActive))
            {
                Button button = displays.First(x => x.pcd == target).GetComponentInChildren<Button>();
                button.interactable = false;
            }
        }
    }

    public void PopulateSkillData(Skill skill, PlayerCharacterData currChar)
    {
        title.text = skill.name;
        description.text = skill.skillDescription;
        skillPointCost.text = $"SP Cost: {skill.skillPointCost.ToString()}";
        caster.text = $"Caster: {currChar.unitName}";
        isMagic.text = skill.isMagic ? "Magical" : "Physical";
        isRanged.text = skill.isRanged ? "Ranged" : "Close Up";
        isMultiTargeted.text = skill.isMultiTargeted ? "Multiple targets" : "Single target";
        whatElement.text = $"Element: {skill.elemAttribute.ToString()}";
    }
}
