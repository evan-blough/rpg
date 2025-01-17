using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Equipment/Weapon")]
public class Weapon : Equipment
{
    public Elements element;
    public List<Statuses> statuses;

    public void ApplyWeaponStatuses(Character enemy, int turnCounter)
    {
        foreach (var status in statuses)
        {
            Statuses newStatus = new Statuses(status);
            if (enemy.immunities.Any(s => s == newStatus.status))
            {
                continue;
            }
            else if (!enemy.resistances.Any(s => s == newStatus.status) || Random.Range(0, 1) == 1)
            {
                if (newStatus.accuracy * 100 >= Random.Range(1, 100))
                {
                    if (enemy.currStatuses.FirstOrDefault(s => s.status == newStatus.status) != null)
                    {
                        enemy.currStatuses.RemoveAll(s => s.status == newStatus.status);
                    }

                    newStatus.expirationTurn += turnCounter;
                    enemy.currStatuses.Add(newStatus);

                    if (newStatus.status == Status.Death)
                    {
                        enemy.currHP = 0;
                        enemy.isActive = false;
                    }
                }
            }
        }
    }
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
