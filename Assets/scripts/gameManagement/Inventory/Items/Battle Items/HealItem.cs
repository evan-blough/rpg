using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Heal Item", menuName = "Items/BattleItem/Heal Item")]
public class HealItem : BattleItem
{
    public override bool UseItemInField(List<PlayerCharacterData> targets)
    {
        List<string> result = new List<string>();

        foreach (PlayerCharacterData target in targets)
        {
            if (!target.isActive || target.currHP == target.maxHP)
            {
                continue;
            }

            target.currHP += effectValue;

            if (target.currHP > target.maxHP) target.currHP = target.maxHP;

            result.Add(effectValue.ToString());
        }

        return result.Any();
    }

    public override List<string> UseItem(List<Character> targets, int turnCounter)
    {
        List<string> result = new List<string>();

        foreach (Character target in targets)
        {
            if (!target.isActive)
            {
                result.Add("");
                continue;
            }

            target.currHP += effectValue;

            if (target.currHP > target.maxHP) target.currHP = target.maxHP;

            result.Add(effectValue.ToString());
        }

        return result;
    }
}
