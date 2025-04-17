using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Equipment/Weapon")]
public class Weapon : Equipment
{
    public bool isMagic;
    public Elements element;
    public List<Statuses> statuses;

    public override string PrintStats()
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

        if (accuracyMod > 0) returnString += $"ACC +{accuracyMod}%";
        else if (accuracyMod < 0) returnString += $"ACC {accuracyMod}%";

        return returnString;
    }
}
