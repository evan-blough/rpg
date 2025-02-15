using UnityEngine;
using UnityEngine.UI;

public class StatDiffBlock : MonoBehaviour
{
    public Text attackText;
    public Text defenseText;
    public Text magAtkText;
    public Text magDefText;
    public Text agilityText;
    public Text weaponTypeText;

    public void PopulateBlock(PlayerCharacterData pcd)
    {
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

    public void PopulateBlock(PlayerCharacterData pcd, Weapon newWeapon)
    {
        var attackBuff = pcd.attack - (pcd.weapon is null ? 0 : pcd.weapon.attackBuff) + newWeapon.attackBuff;
        var defenseBuff = pcd.defense - (pcd.weapon is null ? 0 : pcd.weapon.defenseBuff) + newWeapon.defenseBuff;
        var magAtkBuff = pcd.magAtk - (pcd.weapon is null ? 0 : pcd.weapon.magicAttackBuff) + newWeapon.magicAttackBuff;
        var magDefBuff = pcd.magDef - (pcd.weapon is null ? 0 : pcd.weapon.magicDefenseBuff) + newWeapon.magicDefenseBuff;
        var agilityBuff = pcd.agility - (pcd.weapon is null ? 0 : pcd.weapon.agilityBuff) + newWeapon.agilityBuff;

        attackText.text = $"{pcd.attack} => {attackBuff}";
        if (pcd.attack != attackBuff)
            attackText.color = pcd.attack > attackBuff ? Color.red : Color.green;
        else
            attackText.color = Color.white;

        defenseText.text = $"{pcd.defense} => {defenseBuff}";
        if (pcd.defense != defenseBuff)
            defenseText.color = pcd.defense > defenseBuff ? Color.red : Color.green;
        else
            defenseText.color = Color.white;

        magAtkText.text = $"{pcd.magAtk} => {magAtkBuff}";
        if (pcd.magAtk != magAtkBuff)
            magAtkText.color = pcd.magAtk > magAtkBuff ? Color.red : Color.green;
        else
            magAtkText.color = Color.white;

        magDefText.text = $"{pcd.magDef} => {magDefBuff}";
        if (pcd.magDef != magDefBuff)
            magDefText.color = pcd.magDef > magDefBuff ? Color.red : Color.green;
        else
            magDefText.color = Color.white;

        agilityText.text = $"{pcd.agility} => {agilityBuff}";
        if (pcd.agility != agilityBuff)
            agilityText.color = pcd.agility > agilityBuff ? Color.red : Color.green;
        else
            agilityText.color = Color.white;

        weaponTypeText.text = $"{pcd.weapon.weight} => {newWeapon.weight}";
        if (pcd.weapon.weight != newWeapon.weight)
            weaponTypeText.color = pcd.weapon.weight < newWeapon.weight ? Color.red : Color.green;
        else
            weaponTypeText.color = Color.white;
    }

    public void PopulateBlock(PlayerCharacterData pcd, Armor newArmor)
    {
        var attackBuff = pcd.attack - (pcd.armor is null ? 0 : pcd.armor.attackBuff) + newArmor.attackBuff;
        var defenseBuff = pcd.defense - (pcd.armor is null ? 0 : pcd.armor.defenseBuff) + newArmor.defenseBuff;
        var magAtkBuff = pcd.magAtk - (pcd.armor is null ? 0 : pcd.armor.magicAttackBuff) + newArmor.magicAttackBuff;
        var magDefBuff = pcd.magDef - (pcd.armor is null ? 0 : pcd.armor.magicDefenseBuff) + newArmor.magicDefenseBuff;
        var agilityBuff = pcd.agility - (pcd.armor is null ? 0 : pcd.armor.agilityBuff) + newArmor.agilityBuff;

        attackText.text = $"{pcd.attack} => {attackBuff}";
        if (pcd.attack != attackBuff)
            attackText.color = pcd.attack > attackBuff ? Color.red : Color.green;
        else
            attackText.color = Color.white;

        defenseText.text = $"{pcd.defense} => {defenseBuff}";
        if (pcd.defense != defenseBuff)
            defenseText.color = pcd.defense > defenseBuff ? Color.red : Color.green;
        else
            defenseText.color = Color.white;

        magAtkText.text = $"{pcd.magAtk} => {magAtkBuff}";
        if (pcd.magAtk != magAtkBuff)
            magAtkText.color = pcd.magAtk > magAtkBuff ? Color.red : Color.green;
        else
            magAtkText.color = Color.white;

        magDefText.text = $"{pcd.magDef} => {magDefBuff}";
        if (pcd.magDef != magDefBuff)
            magDefText.color = pcd.magDef > magDefBuff ? Color.red : Color.green;
        else
            magDefText.color = Color.white;

        agilityText.text = $"{pcd.agility} => {agilityBuff}";
        if (pcd.agility != agilityBuff)
            agilityText.color = pcd.agility > agilityBuff ? Color.red : Color.green;
        else
            agilityText.color = Color.white;

        weaponTypeText.text = $"{pcd.armor.weight} => {newArmor.weight}";
        if (pcd.armor.weight != newArmor.weight)
            weaponTypeText.color = pcd.armor.weight < newArmor.weight ? Color.red : Color.green;
        else
            weaponTypeText.color = Color.white;
    }
    public void PopulateAccessory1Block(PlayerCharacterData pcd, Accessory newAccessory)
    {
        var attackBuff = pcd.attack - (pcd.accessory1 is null ? 0 : pcd.accessory1.attackBuff) + newAccessory.attackBuff;
        var defenseBuff = pcd.defense - (pcd.accessory1 is null ? 0 : pcd.accessory1.defenseBuff) + newAccessory.defenseBuff;
        var magAtkBuff = pcd.magAtk - (pcd.accessory1 is null ? 0 : pcd.accessory1.magicAttackBuff) + newAccessory.magicAttackBuff;
        var magDefBuff = pcd.magDef - (pcd.accessory1 is null ? 0 : pcd.accessory1.magicDefenseBuff) + newAccessory.magicDefenseBuff;
        var agilityBuff = pcd.agility - (pcd.accessory1 is null ? 0 : pcd.accessory1.agilityBuff) + newAccessory.agilityBuff;

        attackText.text = $"{pcd.attack} => {attackBuff}";
        if (pcd.attack != attackBuff)
            attackText.color = pcd.attack > attackBuff ? Color.red : Color.green;
        else
            attackText.color = Color.white;

        defenseText.text = $"{pcd.defense} => {defenseBuff}";
        if (pcd.defense != defenseBuff)
            defenseText.color = pcd.defense > defenseBuff ? Color.red : Color.green;
        else
            defenseText.color = Color.white;

        magAtkText.text = $"{pcd.magAtk} => {magAtkBuff}";
        if (pcd.magAtk != magAtkBuff)
            magAtkText.color = pcd.magAtk > magAtkBuff ? Color.red : Color.green;
        else
            magAtkText.color = Color.white;

        magDefText.text = $"{pcd.magDef} => {magDefBuff}";
        if (pcd.magDef != magDefBuff)
            magDefText.color = pcd.magDef > magDefBuff ? Color.red : Color.green;
        else
            magDefText.color = Color.white;

        agilityText.text = $"{pcd.agility} => {agilityBuff}";
        if (pcd.agility != agilityBuff)
            agilityText.color = pcd.agility > agilityBuff ? Color.red : Color.green;
        else
            agilityText.color = Color.white;

        weaponTypeText.text = $"{pcd.accessory1.weight} => {newAccessory.weight}";
        if (pcd.accessory1.weight != newAccessory.weight)
            weaponTypeText.color = pcd.accessory1.weight < newAccessory.weight ? Color.red : Color.green;
        else
            weaponTypeText.color = Color.white;
    }

    public void PopulateAccessory2Block(PlayerCharacterData pcd, Accessory newAccessory)
    {
        var attackBuff = pcd.attack - (pcd.accessory2 is null ? 0 : pcd.accessory2.attackBuff) + newAccessory.attackBuff;
        var defenseBuff = pcd.defense - (pcd.accessory2 is null ? 0 : pcd.accessory2.defenseBuff) + newAccessory.defenseBuff;
        var magAtkBuff = pcd.magAtk - (pcd.accessory2 is null ? 0 : pcd.accessory2.magicAttackBuff) + newAccessory.magicAttackBuff;
        var magDefBuff = pcd.magDef - (pcd.accessory2 is null ? 0 : pcd.accessory2.magicDefenseBuff) + newAccessory.magicDefenseBuff;
        var agilityBuff = pcd.agility - (pcd.accessory2 is null ? 0 : pcd.accessory2.agilityBuff) + newAccessory.agilityBuff;

        attackText.text = $"{pcd.attack} => {attackBuff}";
        if (pcd.attack != attackBuff)
            attackText.color = pcd.attack > attackBuff ? Color.red : Color.green;
        else
            attackText.color = Color.white;

        defenseText.text = $"{pcd.defense} => {defenseBuff}";
        if (pcd.defense != defenseBuff)
            defenseText.color = pcd.defense > defenseBuff ? Color.red : Color.green;
        else
            defenseText.color = Color.white;

        magAtkText.text = $"{pcd.magAtk} => {magAtkBuff}";
        if (pcd.magAtk != magAtkBuff)
            magAtkText.color = pcd.magAtk > magAtkBuff ? Color.red : Color.green;
        else
            magAtkText.color = Color.white;

        magDefText.text = $"{pcd.magDef} => {magDefBuff}";
        if (pcd.magDef != magDefBuff)
            magDefText.color = pcd.magDef > magDefBuff ? Color.red : Color.green;
        else
            magDefText.color = Color.white;

        agilityText.text = $"{pcd.agility} => {agilityBuff}";
        if (pcd.agility != agilityBuff)
            agilityText.color = pcd.agility > agilityBuff ? Color.red : Color.green;
        else
            agilityText.color = Color.white;

        weaponTypeText.text = $"{pcd.accessory2.weight} => {newAccessory.weight}";
        if (pcd.accessory2.weight != newAccessory.weight)
            weaponTypeText.color = pcd.accessory2.weight < newAccessory.weight ? Color.red : Color.green;
        else
            weaponTypeText.color = Color.white;
    }
}
