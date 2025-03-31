using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillsCharacterHolder : MonoBehaviour
{
    public Image portrait;
    public GameObject battleSkills;
    public GameObject charDataView;
    public Text hpView;
    public Text spView;
    public Text unitName;
    public Text level;
    public Text status;
    public GameObject battleSkillButtonPrefab;
    public PlayerCharacterData pcd;

    public void CreateCharacterDisplay(PlayerCharacterData data)
    {
        pcd = data;
        portrait.sprite = data.portrait;
        battleSkills.gameObject.SetActive(false);
        PopulateCharacterDisplay();
    }

    public void PopulateBattleSkillsDisplay()
    {
        GameObject temp;
        foreach (Transform child in battleSkills.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Skill skill in pcd.equippedSkills)
        {
            temp = Instantiate(battleSkillButtonPrefab, battleSkills.transform);
            temp.GetComponentInChildren<SkillsMenuButton>().PopulateButton(skill);
            temp.GetComponentInChildren<SkillsMenuButton>()
                .GetComponent<Button>()
                .onClick.AddListener(
                () => RemoveSkillFromChar(skill));
        }

        battleSkills.SetActive(true);
        charDataView.SetActive(false);
    }

    public void PopulateCharacterDisplay()
    {
        level.text = $"Level {pcd.level}";
        unitName.text = pcd.unitName;
        string statusText = pcd.currStatuses.Any() ? pcd.currStatuses.First().status.ToString() : string.Empty;
        status.text = statusText;
        hpView.text = $"HP: {pcd.currHP} / {pcd.maxHP}";
        spView.text = $"SP: {pcd.currSP} / {pcd.maxSP}";
        charDataView.SetActive(true);
        battleSkills.SetActive(false);
    }

    public void RemoveSkillFromChar(Skill skill)
    {
        pcd.equippedSkills.Remove(skill);
        PopulateBattleSkillsDisplay();
    }
}
