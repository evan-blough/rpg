using UnityEngine;
using UnityEngine.UI;

public class StatDiffBlock : MonoBehaviour
{
    public PlayerCharacterData data;
    public Text attackText;
    public Text defenseText;
    public Text magAtkText;
    public Text magDefText;
    public Text agilityText;
    public Text weaponTypeText;

    public void PopulateBlock(PlayerCharacterData pcd)
    {
        data = pcd;
        attackText.color = Color.white;
        defenseText.color = Color.white;
        magAtkText.color = Color.white;
        magDefText.color = Color.white;
        agilityText.color = Color.white;
        weaponTypeText.color = Color.white;
        attackText.text = $"{pcd.attack}";
        defenseText.text = $"{pcd.defense}";
        magAtkText.text = $"{pcd.magAtk}";
        magDefText.text = $"{pcd.magDef}";
        agilityText.text = $"{pcd.agility}";
        weaponTypeText.text = "";
    }

    public void PopulateBlock(Weapon newWeapon)
    {
        var attackBuff = data.attack - (data.weapon is null ? 0 : data.weapon.attackBuff) + newWeapon.attackBuff;
        var defenseBuff = data.defense - (data.weapon is null ? 0 : data.weapon.defenseBuff) + newWeapon.defenseBuff;
        var magAtkBuff = data.magAtk - (data.weapon is null ? 0 : data.weapon.magicAttackBuff) + newWeapon.magicAttackBuff;
        var magDefBuff = data.magDef - (data.weapon is null ? 0 : data.weapon.magicDefenseBuff) + newWeapon.magicDefenseBuff;
        var agilityBuff = data.agility - (data.weapon is null ? 0 : data.weapon.agilityBuff) + newWeapon.agilityBuff;

        attackText.text = $"{data.attack} => {attackBuff}";
        if (data.attack != attackBuff)
            attackText.color = data.attack > attackBuff ? Color.red : Color.green;
        else
            attackText.color = Color.white;

        defenseText.text = $"{data.defense} => {defenseBuff}";
        if (data.defense != defenseBuff)
            defenseText.color = data.defense > defenseBuff ? Color.red : Color.green;
        else
            defenseText.color = Color.white;

        magAtkText.text = $"{data.magAtk} => {magAtkBuff}";
        if (data.magAtk != magAtkBuff)
            magAtkText.color = data.magAtk > magAtkBuff ? Color.red : Color.green;
        else
            magAtkText.color = Color.white;

        magDefText.text = $"{data.magDef} => {magDefBuff}";
        if (data.magDef != magDefBuff)
            magDefText.color = data.magDef > magDefBuff ? Color.red : Color.green;
        else
            magDefText.color = Color.white;

        agilityText.text = $"{data.agility} => {agilityBuff}";
        if (data.agility != agilityBuff)
            agilityText.color = data.agility > agilityBuff ? Color.red : Color.green;
        else
            agilityText.color = Color.white;

        weaponTypeText.text = $"{(data.weapon is not null ? data.weapon.weight : "None")} => {newWeapon.weight}";
        if (data.weapon is not null && data.weapon.weight != newWeapon.weight)
            weaponTypeText.color = data.weapon.weight < newWeapon.weight ? Color.red : Color.green;
        else
            weaponTypeText.color = Color.white;
    }

    public void PopulateBlock(Armor newArmor)
    {
        var attackBuff = data.attack - (data.armor is null ? 0 : data.armor.attackBuff) + newArmor.attackBuff;
        var defenseBuff = data.defense - (data.armor is null ? 0 : data.armor.defenseBuff) + newArmor.defenseBuff;
        var magAtkBuff = data.magAtk - (data.armor is null ? 0 : data.armor.magicAttackBuff) + newArmor.magicAttackBuff;
        var magDefBuff = data.magDef - (data.armor is null ? 0 : data.armor.magicDefenseBuff) + newArmor.magicDefenseBuff;
        var agilityBuff = data.agility - (data.armor is null ? 0 : data.armor.agilityBuff) + newArmor.agilityBuff;

        attackText.text = $"{data.attack} => {attackBuff}";
        if (data.attack != attackBuff)
            attackText.color = data.attack > attackBuff ? Color.red : Color.green;
        else
            attackText.color = Color.white;

        defenseText.text = $"{data.defense} => {defenseBuff}";
        if (data.defense != defenseBuff)
            defenseText.color = data.defense > defenseBuff ? Color.red : Color.green;
        else
            defenseText.color = Color.white;

        magAtkText.text = $"{data.magAtk} => {magAtkBuff}";
        if (data.magAtk != magAtkBuff)
            magAtkText.color = data.magAtk > magAtkBuff ? Color.red : Color.green;
        else
            magAtkText.color = Color.white;

        magDefText.text = $"{data.magDef} => {magDefBuff}";
        if (data.magDef != magDefBuff)
            magDefText.color = data.magDef > magDefBuff ? Color.red : Color.green;
        else
            magDefText.color = Color.white;

        agilityText.text = $"{data.agility} => {agilityBuff}";
        if (data.agility != agilityBuff)
            agilityText.color = data.agility > agilityBuff ? Color.red : Color.green;
        else
            agilityText.color = Color.white;

        weaponTypeText.text = $"{(data.armor is not null ? data.armor.weight : "None")} => {newArmor.weight}";
        if (data.armor is not null && data.armor.weight != newArmor.weight)
            weaponTypeText.color = data.armor.weight < newArmor.weight ? Color.red : Color.green;
        else
            weaponTypeText.color = Color.white;
    }
    public void PopulateAccessory1Block(Accessory newAccessory)
    {
        var attackBuff = data.attack - (data.accessory1 is null ? 0 : data.accessory1.attackBuff) + newAccessory.attackBuff;
        var defenseBuff = data.defense - (data.accessory1 is null ? 0 : data.accessory1.defenseBuff) + newAccessory.defenseBuff;
        var magAtkBuff = data.magAtk - (data.accessory1 is null ? 0 : data.accessory1.magicAttackBuff) + newAccessory.magicAttackBuff;
        var magDefBuff = data.magDef - (data.accessory1 is null ? 0 : data.accessory1.magicDefenseBuff) + newAccessory.magicDefenseBuff;
        var agilityBuff = data.agility - (data.accessory1 is null ? 0 : data.accessory1.agilityBuff) + newAccessory.agilityBuff;

        attackText.text = $"{data.attack} => {attackBuff}";
        if (data.attack != attackBuff)
            attackText.color = data.attack > attackBuff ? Color.red : Color.green;
        else
            attackText.color = Color.white;

        defenseText.text = $"{data.defense} => {defenseBuff}";
        if (data.defense != defenseBuff)
            defenseText.color = data.defense > defenseBuff ? Color.red : Color.green;
        else
            defenseText.color = Color.white;

        magAtkText.text = $"{data.magAtk} => {magAtkBuff}";
        if (data.magAtk != magAtkBuff)
            magAtkText.color = data.magAtk > magAtkBuff ? Color.red : Color.green;
        else
            magAtkText.color = Color.white;

        magDefText.text = $"{data.magDef} => {magDefBuff}";
        if (data.magDef != magDefBuff)
            magDefText.color = data.magDef > magDefBuff ? Color.red : Color.green;
        else
            magDefText.color = Color.white;

        agilityText.text = $"{data.agility} => {agilityBuff}";
        if (data.agility != agilityBuff)
            agilityText.color = data.agility > agilityBuff ? Color.red : Color.green;
        else
            agilityText.color = Color.white;

        weaponTypeText.text = $"{(data.accessory1 is not null ? data.accessory1.weight : "None")} => {newAccessory.weight}";
        if (data.accessory1 is not null && data.accessory1.weight != newAccessory.weight)
            weaponTypeText.color = data.accessory1.weight < newAccessory.weight ? Color.red : Color.green;
        else
            weaponTypeText.color = Color.white;
    }

    public void PopulateAccessory2Block(Accessory newAccessory)
    {
        var attackBuff = data.attack - (data.accessory2 is null ? 0 : data.accessory2.attackBuff) + newAccessory.attackBuff;
        var defenseBuff = data.defense - (data.accessory2 is null ? 0 : data.accessory2.defenseBuff) + newAccessory.defenseBuff;
        var magAtkBuff = data.magAtk - (data.accessory2 is null ? 0 : data.accessory2.magicAttackBuff) + newAccessory.magicAttackBuff;
        var magDefBuff = data.magDef - (data.accessory2 is null ? 0 : data.accessory2.magicDefenseBuff) + newAccessory.magicDefenseBuff;
        var agilityBuff = data.agility - (data.accessory2 is null ? 0 : data.accessory2.agilityBuff) + newAccessory.agilityBuff;

        attackText.text = $"{data.attack} => {attackBuff}";
        if (data.attack != attackBuff)
            attackText.color = data.attack > attackBuff ? Color.red : Color.green;
        else
            attackText.color = Color.white;

        defenseText.text = $"{data.defense} => {defenseBuff}";
        if (data.defense != defenseBuff)
            defenseText.color = data.defense > defenseBuff ? Color.red : Color.green;
        else
            defenseText.color = Color.white;

        magAtkText.text = $"{data.magAtk} => {magAtkBuff}";
        if (data.magAtk != magAtkBuff)
            magAtkText.color = data.magAtk > magAtkBuff ? Color.red : Color.green;
        else
            magAtkText.color = Color.white;

        magDefText.text = $"{data.magDef} => {magDefBuff}";
        if (data.magDef != magDefBuff)
            magDefText.color = data.magDef > magDefBuff ? Color.red : Color.green;
        else
            magDefText.color = Color.white;

        agilityText.text = $"{data.agility} => {agilityBuff}";
        if (data.agility != agilityBuff)
            agilityText.color = data.agility > agilityBuff ? Color.red : Color.green;
        else
            agilityText.color = Color.white;

        weaponTypeText.text = $"{(data.accessory2 is not null ? data.accessory2.weight : "None")} => {newAccessory.weight}";
        if (data.accessory2 is not null && data.accessory2.weight != newAccessory.weight)
            weaponTypeText.color = data.accessory2.weight < newAccessory.weight ? Color.red : Color.green;
        else
            weaponTypeText.color = Color.white;
    }
}
