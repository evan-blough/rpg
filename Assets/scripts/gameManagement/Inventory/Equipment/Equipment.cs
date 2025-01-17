public enum EquipmentWeight { MAGIC = 0, LIGHT = 1, MEDIUM = 2, HEAVY = 3, VERY_HEAVY = 4 }
public class Equipment : Items
{
    public int attackBuff;
    public int defenseBuff;
    public int magicAttackBuff;
    public int magicDefenseBuff;
    public int agilityBuff;
    public EquipmentWeight weight;
    public int accuracyMod;

    public virtual string PrintStats()
    {
        string returnString = string.Empty;
        if (attackBuff > 0) returnString += $"ATK +{attackBuff} ";
        else if (attackBuff < 0) returnString += $"ATK {attackBuff} ";

        if (defenseBuff > 0) returnString += $"DEF +{defenseBuff} ";
        else if (defenseBuff < 0) returnString += $"DEF {defenseBuff} ";

        if (magicAttackBuff > 0) returnString += $"MGATK +{magicAttackBuff} ";
        else if (magicAttackBuff < 0) returnString += $"MGATK {magicAttackBuff} ";

        if (magicDefenseBuff > 0) returnString += $"MGDEF +{magicDefenseBuff} ";
        else if (magicDefenseBuff < 0) returnString += $"MGDEF {magicDefenseBuff} ";

        if (agilityBuff > 0) returnString += $"AGI +{agilityBuff} ";
        else if (agilityBuff < 0) returnString += $"AGI {agilityBuff} ";

        if (accuracyMod > 0) returnString += $"DODGE +{accuracyMod}%";
        else if (accuracyMod < 0) returnString += $"DODGE {accuracyMod}%";

        return returnString;
    }
}
