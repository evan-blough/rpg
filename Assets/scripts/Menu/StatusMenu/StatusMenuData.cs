using UnityEngine;
using UnityEngine.UI;
public class StatusMenuData : MonoBehaviour
{
    PlayerCharacterData data;
    public Image portrait;
    public Text nameText;
    public Text levelText;
    public Text hpText;
    public Text spText;
    public Text currClassText;
    public Text currExpText;
    public Text nextLevelExpText;
    public Text classLevelText;
    public Text nextClassLevelText;
    public Text strengthText;
    public Text constitutionText;
    public Text intelligenceText;
    public Text spiritText;
    public Text speedText;
    public Text luckText;
    public Text attackText;
    public Text defenseText;
    public Text magAtkText;
    public Text magDefText;
    public Text agilityText;
    public Text accuracyText;
    public Text evasionText;

    public void PopulateData(PlayerCharacterData pcd)
    {
        data = pcd;
        portrait.sprite = data.portrait;
        nameText.text = data.unitName;
        levelText.text = $"Level {data.level}";
        hpText.text = $"HP: {data.currHP} / {data.maxHP}";
        spText.text = $"SP: {data.currSP} / {data.maxSP}";
        currClassText.text = data.equippedClass.classSlot.name;
        currExpText.text = $"Curr EXP: {data.exp}";
        nextLevelExpText.text = $"To Next Level: {data.expHandler.nextLevelExp - data.exp}";
        classLevelText.text = $"Class Level: {(data.equippedClass.isMaxed ? "MAX" : data.equippedClass.classLevel)}";
        nextClassLevelText.text = $"Next Class Level: {(data.equippedClass.isMaxed ? "MAXED" : data.equippedClass.currLevel.expNeeded - data.equippedClass.classXp)}";
        strengthText.text = $"Strength: {data.strength}";
        constitutionText.text = $"Constitution: {data.constitution}";
        intelligenceText.text = $"Intelligence: {data.intelligence}";
        spiritText.text = $"Spirit: {data.spirit}";
        speedText.text = $"Speed: {data.speed}";
        luckText.text = $"Luck: {data.luck}";
        attackText.text = $"Attack: {data.attack}";
        defenseText.text = $"Defense: {data.defense}";
        magAtkText.text = $"Magic Atk: {data.magAtk}";
        magDefText.text = $"Magic Def: {data.magDef}";
        agilityText.text = $"Agility: {data.agility}";
        accuracyText.text = $"Accuracy: {data.hitPercent}%";
        evasionText.text = $"Evasion: {data.dodgePercent}%";
    }
}
