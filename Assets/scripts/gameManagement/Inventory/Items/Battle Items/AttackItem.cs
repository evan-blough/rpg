using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Item", menuName = "Items/BattleItem/Attack Item")]
public class AttackItem : BattleItem
{
    public override List<string> UseItem(List<Character> targets, int turnCounter)
    {
        List<string> result = new List<string>();
        int newEffectVal;

        foreach (Character target in targets)
        {
            if (!target.isActive)
            {
                result.Add("");
                continue;
            }

            else
            {
                List<Elements> element = new List<Elements>();

                Attack attack = new Attack(effectValue, false, false, statuses, element, turnCounter);

                newEffectVal = target.TakeDamage(attack);

                result.Add(newEffectVal.ToString());
            }
        }

        return result;
    }
}
