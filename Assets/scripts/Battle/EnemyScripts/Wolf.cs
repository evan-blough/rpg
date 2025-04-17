using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wolf : Enemy
{
    public override IEnumerator EnemyTurn(List<PlayerCharacter> playerCharacters, List<Enemy> enemies, int turnCounter, BattleStationManager bsm, EnemySkillUI esu)
    {
        List<string> damages = new List<string>();
        List<Character> targets = new List<Character>();
        int selection = Random.Range(1, 100) % 4;
        Character targetUnit = this.FindTarget(playerCharacters.Where(c => c.isActive && c.isActive).ToList());

        if (selection == 0)
        {
            esu.SetText("Maul");
            esu.gameObject.SetActive(true);
            targets.Add(targetUnit);
            damages.Add(Maul(targetUnit, turnCounter).ToString());
            yield return new WaitForSeconds(.35f);

        }
        else
        {
            targets.Add(targetUnit);
            damages.Add(Attack(targetUnit, turnCounter).ToString());
            yield return null;
        }

        for (int i = 0; i < damages.Count && i < targets.Count; i++) bsm.SetText(damages[i], targets[i]);

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < targets.Count; i++) bsm.SetText(string.Empty, targets[i]);
        esu.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.75f);

        foreach (Character target in targets)
        {
            if (target.currHP <= 0)
            {
                target.isActive = false;
            }
        }
    }

    public int Maul(Character enemy, int turnCounter)
    {
        int damage;
        double hitChance = hitPercent - enemy.dodgePercent;

        if (hitChance >= UnityEngine.Random.Range(0, 100))
        {
            bool isCritical = UnityEngine.Random.Range(1, 20) >= 18 ? true : false;
            damage = (int)(((attack * 1.5) * FindPhysicalAttackStatusModifier() * UnityEngine.Random.Range(1f, 1.25f)));

            List<Statuses> statuses = new List<Statuses>();
            statuses.Add(new Statuses(Status.Bleeding, 3, true, .5f));
            List<Elements> element = new List<Elements>();

            Attack currAttack = new Attack(damage, isCritical, false, statuses, element, turnCounter);

            damage = enemy.TakeDamage(currAttack);

            return damage;
        }
        return 0;
    }
}
