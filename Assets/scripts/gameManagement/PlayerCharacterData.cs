using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerCharacterData : CharacterData
{
    public Sprite portrait;
    public ExperienceHandler expHandler;
    public ClassExpHandler classExpHandler;
    public EquipmentWeight weightClass;
    public List<Skill> skills;
    public List<Skill> equippedSkills;
    public List<Class> classes;
    [SerializeReference]
    public Class equippedClass;
    public int excessClassPoints = 0;
    public BattleInventory charInventory;
    public Weapon weapon;
    public Armor armor;
    public Accessory accessory1;
    public Accessory accessory2;
    public bool isInParty;
    public int exp;
    public int skillCount = 12;
    public virtual int attack => strength + (weapon is null ? 0 : weapon.attackBuff) + (armor is null ? 0 : armor.attackBuff)
                + (accessory1 is null ? 0 : accessory1.attackBuff) + (accessory2 is null ? 0 : accessory2.attackBuff)
                + (equippedClass is null ? 0 : equippedClass.classAtkMod);

    public virtual int defense => constitution + (weapon is null ? 0 : weapon.defenseBuff) + (armor is null ? 0 : armor.defenseBuff)
                + (accessory1 is null ? 0 : accessory1.defenseBuff) + (accessory2 is null ? 0 : accessory2.defenseBuff)
                + (equippedClass is null ? 0 : equippedClass.classDefMod);

    public virtual int magAtk => intelligence + (weapon is null ? 0 : weapon.magicAttackBuff) + (armor is null ? 0 : armor.magicAttackBuff)
                + (accessory1 is null ? 0 : accessory1.magicAttackBuff) + (accessory2 is null ? 0 : accessory2.magicAttackBuff)
                + (equippedClass is null ? 0 : equippedClass.classMgAtkMod);

    public virtual int magDef => spirit + (weapon is null ? 0 : weapon.magicDefenseBuff) + (armor is null ? 0 : armor.magicDefenseBuff)
                + (accessory1 is null ? 0 : accessory1.magicDefenseBuff) + (accessory2 is null ? 0 : accessory2.magicDefenseBuff)
                + (equippedClass is null ? 0 : equippedClass.classMgDefMod);
    public virtual int agility => speed + (weapon is null ? 0 : weapon.agilityBuff) + (armor is null ? 0 : armor.agilityBuff)
                + (accessory1 is null ? 0 : accessory1.agilityBuff) + (accessory2 is null ? 0 : accessory2.agilityBuff)
                + (equippedClass is null ? 0 : equippedClass.classAgilityMod);

    public virtual int hitPercent => 100 + (weapon is null ? 0 : weapon.accuracyMod);
    public virtual int dodgePercent => 0 + (armor is null ? 0 : armor.accuracyMod) + (accessory1 is null ? 0 : accessory1.accuracyMod)
        + (accessory2 is null ? 0 : accessory2.accuracyMod);

    public override List<Status> resistances
    {
        get
        {
            var statusList = new List<Status>();
            if (accessory1 is not null) statusList.AddRange(accessory1.statusResistances);
            if (accessory2 is not null) statusList.AddRange(accessory2.statusResistances);
            return statusList;
        }
    }
    public override List<Status> immunities
    {
        get
        {
            var statusList = new List<Status>();
            if (accessory1 is not null) statusList.AddRange(accessory1.statusImmunities);
            if (accessory2 is not null) statusList.AddRange(accessory2.statusImmunities);
            return statusList;
        }
    }
    public override List<Elements> elemWeaknesses
    {
        get
        {
            var elemList = new List<Elements>();
            if (armor is not null) elemList.AddRange(armor.elemWeaknesses);
            return elemList;
        }
    }
    public override List<Elements> elemResistances
    {
        get
        {
            var elemList = new List<Elements>();
            if (armor is not null) elemList.AddRange(armor.elemResists);
            return elemList;
        }
    }
    public override List<Elements> elemAbsorptions
    {
        get
        {
            var elemList = new List<Elements>();
            if (armor is not null) elemList.AddRange(armor.elemAbsorption);
            return elemList;
        }
    }

    public override void DeepDataCopy(Character c)
    {
        PlayerCharacter pc;
        if (c is PlayerCharacter) pc = c as PlayerCharacter;
        else return;

        base.DeepDataCopy(c);
        expHandler = pc.expHandler;
        classExpHandler = pc.classExpHandler;
        weightClass = pc.weightClass;
        skills = pc.skills;
        equippedSkills = pc.equippedSkills;
        classes = pc.classes;
        equippedClass = pc.currClass;
        charInventory = pc.charInventory;
        weapon = pc.weapon;
        armor = pc.armor;
        accessory1 = pc.accessory1;
        accessory2 = pc.accessory2;
        isInParty = pc.isInParty;
        exp = pc.exp;
        skillCount = pc.skillCount;
        excessClassPoints = pc.excessClassPoints;
    }
}

[System.Serializable]
public class HeroData : PlayerCharacterData
{
    public override int attack
    {
        get
        {
            if (weapon is null) return strength + level * 2;

            return strength + (weapon is null ? 0 : weapon.attackBuff) + (armor is null ? 0 : armor.attackBuff)
                + (accessory1 is null ? 0 : accessory1.attackBuff) + (accessory2 is null ? 0 : accessory2.attackBuff)
                + (equippedClass is null ? 0 : equippedClass.classAtkMod);
        }
    }
    public override int agility
    {
        get
        {
            if (armor is null && accessory1 is null && accessory2 is null) return speed + 20 + (level * 2)
                    + (equippedClass is null ? 0 : equippedClass.classAgilityMod);

            return speed + (weapon is null ? 0 : weapon.agilityBuff) + (armor is null ? 0 : armor.agilityBuff)
                + (accessory1 is null ? 0 : accessory1.agilityBuff) + (accessory2 is null ? 0 : accessory2.agilityBuff)
                + (equippedClass is null ? 0 : equippedClass.classAgilityMod);
        }
    }
}

[System.Serializable]
public class WizardData : PlayerCharacterData { }

[System.Serializable]
public class SenatorData : PlayerCharacterData { }
