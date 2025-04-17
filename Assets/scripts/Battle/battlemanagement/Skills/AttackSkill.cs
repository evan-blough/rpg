using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Attacking Skill", menuName = "Skills/Attack Skill")]
public class AttackSkill : Skill
{
    public override List<string> UseSkill(Character character, List<Character> targets, int turnCounter)
    {
        List<string> returnDamages = new List<string>();
        double hitCheck;

        foreach (var target in targets)
        {
            if (!target.isActive)
            {
                returnDamages.Add("");
                continue;
            }

            hitCheck = character.hitPercent - target.dodgePercent;
            if (hitCheck >= Random.Range(0, 100))
            {
                bool isCritical = Random.Range(1, 20) * criticalModifier >= Random.Range(1, 20) ? true : false;
                int damage = (int)((isMagic ? character.magAtk : character.attack * character.FindPhysicalAttackStatusModifier())
                    * Random.Range(1f, 1.25f) * powerModifier / targets.Count);

                List<Elements> element = new List<Elements>();
                element.Add(elemAttribute);

                Attack attack = new Attack(damage, isCritical, isMagic, applyTargetStatuses, element, removeTargetStatuses, turnCounter);
                damage = target.TakeDamage(attack);

                returnDamages.Add(damage.ToString());
            }
            else returnDamages.Add("Miss");
        }

        return returnDamages;
    }
}

